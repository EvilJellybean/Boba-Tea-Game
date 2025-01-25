using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private static IngredientManager instance;
    public static IngredientManager Instance => instance;

    [SerializeField]
    private List<Ingredient> allIngredients = new List<Ingredient>();
    [SerializeField]
    private int ingredientCount = 5;
    [SerializeField]
    private float finishDelayBeforeDialogue = 2;
    [SerializeField]
    private float finishDelayAfterDialogue = 5;
    
    [SerializeField]
    private ThoughtBubble thoughtBubble;
    [SerializeField]
    private DraggableIngredient draggableIngredientTemplate;

    [Space]

    [SerializeField]
    private Transform ingredientSpawnZone;
    [SerializeField]
    private Vector2 spawnZoneSize;

    [Space]

    [SerializeField]
    private Transform ingredientDraggableZone;
    [SerializeField]
    private Vector2 draggableZoneSize;

    private List<Ingredient> desiredIngredients = new List<Ingredient>();
    private List<Ingredient> chosenIngredients = new List<Ingredient>();
    private List<DraggableIngredient> draggableIngredients = new List<DraggableIngredient>();
    private List<GameObject> spawnedIngredients = new List<GameObject>();
    public float TotalScore { get; private set; }
    
    

    private void Awake()
    {
        instance = this;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(ingredientSpawnZone.position, spawnZoneSize);
        Gizmos.DrawWireCube(ingredientDraggableZone.position, draggableZoneSize);
    }

    public void CreateNewDrinkOrder()
    {
        ClearIngredients();
        ClearDraggableIngredients();

        desiredIngredients.Clear();
        chosenIngredients.Clear();

        for(int i = 0; i < ingredientCount; i++)
        {
            int categorhyNumber = i + 1;
            List<Ingredient> availableIngredients = allIngredients.FindAll(x => x.CategoryNumber == categorhyNumber);
            Ingredient ingredient = availableIngredients[Random.Range(0, availableIngredients.Count)];
            desiredIngredients.Add(ingredient);
        }

        thoughtBubble.ShowDesiredIngredients(desiredIngredients);

        SpawnDraggableIngredients();
    }

    public void SpawnIngredient(DraggableIngredient draggableIngredient)
    {
        draggableIngredients.Remove(draggableIngredient);
        if (chosenIngredients.Count >= desiredIngredients.Count)
        {
            // Already enough ingredients
            return;
        }

        StartCoroutine(SpawnIngredients(draggableIngredient.Ingredient));

        chosenIngredients.Add(draggableIngredient.Ingredient);
        if (chosenIngredients.Count >= desiredIngredients.Count)
        {
            StartCoroutine(ShowResult());
        }
    }

    private void ClearIngredients()
    {
        for (int i = 0; i < spawnedIngredients.Count; i++)
        {
            Destroy(spawnedIngredients[i].gameObject);
        }
        spawnedIngredients.Clear();
    }

    private void ClearDraggableIngredients()
    {
        for (int i = 0; i < draggableIngredients.Count; i++)
        {
            Destroy(draggableIngredients[i].gameObject);
        }
        draggableIngredients.Clear();
    }

    private void SpawnDraggableIngredients()
    {
        for (int i = 0; i < allIngredients.Count; i++)
        {
            Ingredient ingredient = allIngredients[i];
            Vector3 spawnPosition = ingredientDraggableZone.position + new Vector3(Random.Range(-draggableZoneSize.x / 2, draggableZoneSize.x / 2), Random.Range(-draggableZoneSize.y / 2, draggableZoneSize.y / 2), 0);
            DraggableIngredient newDraggableIngredient = Instantiate(draggableIngredientTemplate, spawnPosition, Quaternion.identity);
            newDraggableIngredient.Configure(ingredient, i+1);
            draggableIngredients.Add(newDraggableIngredient);
        }
    }

    private float CalculateCorrectness()
    {
        if(desiredIngredients.Count == 0)
        {
            return 0;
        }

        int correctCount = 0;
        for(int i = 0; i < desiredIngredients.Count; i++)
        {
            Ingredient desiredIngredient = desiredIngredients[i];
            if(chosenIngredients.Contains(desiredIngredient))
            {
                correctCount++;
            }
        }

        return (float)correctCount / (float)desiredIngredients.Count;
    }

    private IEnumerator SpawnIngredients(Ingredient ingredient)
    {
        for (int i = 0; i < ingredient.Amount; i++)
        {
            Vector3 spawnPosition = ingredientSpawnZone.position + new Vector3(Random.Range(-spawnZoneSize.x / 2, spawnZoneSize.x / 2), Random.Range(-spawnZoneSize.y / 2, spawnZoneSize.y / 2), 0);
            float scale = 1 + Random.Range(-ingredient.ScaleRandomness, ingredient.ScaleRandomness);
            Quaternion rotation = ingredient.AllowRotation ? Quaternion.Euler(0, 0, Random.Range(-180.0f, 180.0f)) : Quaternion.identity;
            GameObject newSpawnedIngredient = Instantiate(ingredient.SpawnedIngredient, spawnPosition, rotation);
            newSpawnedIngredient.transform.localScale *= scale;
            spawnedIngredients.Add(newSpawnedIngredient);
            yield return new WaitForSeconds(ingredient.DelayPerSpawn);
        }
    }

    private IEnumerator ShowResult()
    {
        ClearDraggableIngredients();

        float correctness = CalculateCorrectness();
        bool isCorrectDrink = correctness > 0.999f;
        TotalScore += correctness;

        yield return new WaitForSeconds(finishDelayBeforeDialogue);

        CustomerManager.Instance.ShowFinalDialogue(isCorrectDrink);

        yield return new WaitForSeconds(finishDelayAfterDialogue);

        CustomerManager.Instance.NextCustomer();
    }
}
