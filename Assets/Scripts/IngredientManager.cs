using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private static IngredientManager instance;
    public static IngredientManager Instance => instance;

    [SerializeField]
    private Transform ingredientSpawnPoint;

    private List<GameObject> spawnedIngredients = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    public void SpawnIngredient(Ingredient ingredient)
    {
        GameObject newSpawnedIngredient = Instantiate(ingredient.SpawnedIngredient, ingredientSpawnPoint.position, Quaternion.Euler(0, 0, Random.Range(-180.0f, 180.0f)));
        spawnedIngredients.Add(newSpawnedIngredient);
    }

    private IEnumerator SpawnIngredients(Ingredient ingredient)
    {
        for(int i = 0; i < ingredient.Amount; i++)
        {
            GameObject newSpawnedIngredient = Instantiate(ingredient.SpawnedIngredient, ingredientSpawnPoint.position, Quaternion.Euler(0, 0, Random.Range(-180.0f, 180.0f)));
            spawnedIngredients.Add(newSpawnedIngredient);
            yield return new WaitForSeconds(ingredient.DelayPerSpawn);
        }
    }
}
