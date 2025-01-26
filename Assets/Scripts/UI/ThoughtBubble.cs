using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtBubble : MonoBehaviour
{
    [SerializeField]
    private Transform spawnParent;
    [SerializeField]
    private Image thoughtBubbleItemTemplate;

    private List<Image> spawnedBubbleItems = new List<Image>();

    public void ShowDesiredIngredients(List<Ingredient> desiredIngredients)
    {
        gameObject.SetActive(false);
        gameObject.SetActive(true);

        for(int i = 0; i < spawnedBubbleItems.Count; i++)
        {
            Destroy(spawnedBubbleItems[i].gameObject);
        }
        spawnedBubbleItems.Clear();

        for (int i = 0; i < desiredIngredients.Count; i++)
        {
            Ingredient ingredient = desiredIngredients[i];
            Image newItem = Instantiate(thoughtBubbleItemTemplate, spawnParent);
            newItem.sprite = ingredient.ThoughtBubbleImage;
            spawnedBubbleItems.Add(newItem);
        }
    }
}
