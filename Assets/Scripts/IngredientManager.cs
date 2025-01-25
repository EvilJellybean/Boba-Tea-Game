using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private static IngredientManager instance;
    public static IngredientManager Instance => instance;

    [SerializeField]
    private Transform ingredientSpawnZone;
    [SerializeField]
    private Vector2 spawnZoneSize;

    private List<GameObject> spawnedIngredients = new List<GameObject>();

    private void Awake()
    {
        instance = this;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(ingredientSpawnZone.position, spawnZoneSize);
    }

    public void SpawnIngredient(Ingredient ingredient)
    {
        StartCoroutine(SpawnIngredients(ingredient));
    }

    private IEnumerator SpawnIngredients(Ingredient ingredient)
    {
        for(int i = 0; i < ingredient.Amount; i++)
        {
            Vector3 spawnPosition = ingredientSpawnZone.position + new Vector3(Random.Range(-spawnZoneSize.x / 2, spawnZoneSize.x / 2), Random.Range(-spawnZoneSize.y / 2, spawnZoneSize.y / 2), 0);
            float scale = 1 + Random.Range(-ingredient.ScaleRandomness, ingredient.ScaleRandomness);
            GameObject newSpawnedIngredient = Instantiate(ingredient.SpawnedIngredient, spawnPosition, Quaternion.Euler(0, 0, Random.Range(-180.0f, 180.0f)));
            newSpawnedIngredient.transform.localScale *= scale;
            spawnedIngredients.Add(newSpawnedIngredient);
            yield return new WaitForSeconds(ingredient.DelayPerSpawn);
        }
    }
}
