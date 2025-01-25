using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Customer", menuName = "Scriptable Objects/Customer")]
public class Customer : ScriptableObject
{
    [field: SerializeField]
    public Sprite Image;

    [field: SerializeField]
    public string CustomerName;

    [field: SerializeField, TextArea(minLines: 2, maxLines: 20)]
    public List<string> RandomDialogue = new List<string>();

    [field: SerializeField, TextArea(minLines: 2, maxLines: 20)]
    public List<string> CorrectDrinkDialogue = new List<string>();

    [field: SerializeField, TextArea(minLines: 2, maxLines: 20)]
    public List<string> IncorrectDrinkDialogue = new List<string>();
}
