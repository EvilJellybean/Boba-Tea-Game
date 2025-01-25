using UnityEngine;

[CreateAssetMenu(fileName = "Ingredient", menuName = "Scriptable Objects/Ingredient")]
public class Ingredient : ScriptableObject
{
    [field: SerializeField]
    public Sprite ThoughtBubbleImage;

    [field: SerializeField]
    public GameObject SpawnedIngredient;

    [field: SerializeField]
    public int CategoryNumber = 1;

    [field: SerializeField]
    public int Amount = 3;

    [field: SerializeField]
    public float DelayPerSpawn = 0.25f;

    [field: SerializeField, Range(0.0f, 0.5f)]
    public float ScaleRandomness = 0.25f;
}
