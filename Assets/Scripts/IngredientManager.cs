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

    private void Awake()
    {
        instance = this;
    }

    // TODO Probably put customer sequencing into a different script
    private void Start()
    {
        NewCustomer();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(ingredientSpawnZone.position, spawnZoneSize);
        Gizmos.DrawWireCube(ingredientDraggableZone.position, draggableZoneSize);
    }

    public void NewCustomer()
    {
        for (int i = 0; i < spawnedIngredients.Count; i++)
        {
            Destroy(spawnedIngredients[i].gameObject);
        }
        spawnedIngredients.Clear();

        for (int i = 0; i < draggableIngredients.Count; i++)
        {
            Destroy(draggableIngredients[i].gameObject);
        }
        draggableIngredients.Clear();

        desiredIngredients.Clear();
        chosenIngredients.Clear();

        List<Ingredient> ingredientsLeft = new List<Ingredient>(allIngredients);
        int countLeft = ingredientCount;
        while (countLeft > 0 && ingredientsLeft.Count > 0)
        {
            countLeft--;

            int randomIndex = Random.Range(0, ingredientsLeft.Count);
            Ingredient ingredient = ingredientsLeft[randomIndex];
            ingredientsLeft.RemoveAt(randomIndex);
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

    private void SpawnDraggableIngredients()
    {
        for (int i = 0; i < allIngredients.Count; i++)
        {
            Ingredient ingredient = allIngredients[i];
            Vector3 spawnPosition = ingredientDraggableZone.position + new Vector3(Random.Range(-draggableZoneSize.x / 2, draggableZoneSize.x / 2), Random.Range(-draggableZoneSize.y / 2, draggableZoneSize.y / 2), 0);
            DraggableIngredient newDraggableIngredient = Instantiate(draggableIngredientTemplate, spawnPosition, Quaternion.identity);
            newDraggableIngredient.Configure(ingredient);
            draggableIngredients.Add(newDraggableIngredient);
        }
    }

    private IEnumerator SpawnIngredients(Ingredient ingredient)
    {
        for (int i = 0; i < ingredient.Amount; i++)
        {
            Vector3 spawnPosition = ingredientSpawnZone.position + new Vector3(Random.Range(-spawnZoneSize.x / 2, spawnZoneSize.x / 2), Random.Range(-spawnZoneSize.y / 2, spawnZoneSize.y / 2), 0);
            float scale = 1 + Random.Range(-ingredient.ScaleRandomness, ingredient.ScaleRandomness);
            GameObject newSpawnedIngredient = Instantiate(ingredient.SpawnedIngredient, spawnPosition, Quaternion.Euler(0, 0, Random.Range(-180.0f, 180.0f)));
            newSpawnedIngredient.transform.localScale *= scale;
            spawnedIngredients.Add(newSpawnedIngredient);
            yield return new WaitForSeconds(ingredient.DelayPerSpawn);
        }
    }

    private IEnumerator ShowResult()
    {
        // TODO Dialogue here

        yield return new WaitForSeconds(2);

        NewCustomer();
    }
}
