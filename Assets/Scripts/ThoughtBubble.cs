using System.Collections.Generic;
using UnityEngine;

public class ThoughtBubble : MonoBehaviour
{
    [SerializeField]
    private Transform spawnParent;
    [SerializeField]
    private SpriteRenderer thoughtBubbleItemTemplate;

    private List<SpriteRenderer> spawnedBubbleItems = new List<SpriteRenderer>();

    public void ShowDesiredIngredients(List<Ingredient> desiredIngredients)
    {
        for(int i = 0; i < spawnedBubbleItems.Count; i++)
        {
            Destroy(spawnedBubbleItems[i].gameObject);
        }
        spawnedBubbleItems.Clear();

        for (int i = 0; i < desiredIngredients.Count; i++)
        {
            Ingredient ingredient = desiredIngredients[i];
            SpriteRenderer newItem = Instantiate(thoughtBubbleItemTemplate, spawnParent);
            newItem.sprite = ingredient.ThoughtBubbleImage;
        }
    }
}
