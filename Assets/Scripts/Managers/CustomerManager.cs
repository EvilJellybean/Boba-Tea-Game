using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    private static CustomerManager instance;
    public static CustomerManager Instance => instance;

    [SerializeField]
    private List<Customer> allCustomers;
    [SerializeField]
    private int customerCount = 3;
    [SerializeField]
    private FinalPanel finalPanel;

    private int currentCustomerNumber;
    private List<Customer> customersLeft = new List<Customer>();
    private Customer currentCustomer;

    public float FinalScore { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        customersLeft.Clear();
        customersLeft.AddRange(allCustomers);

        // Shuffle
        customersLeft.Sort((x, y) => Random.value < 0.5f ? -1 : 1);

        NextCustomer();
    }

    public void NextCustomer()
    {
        if (currentCustomerNumber >= customerCount)
        {
            FinalScore = IngredientManager.Instance.TotalScore / customerCount;
            finalPanel.Show();
            return;
        }

        int randomIndex = Random.Range(0, customersLeft.Count);
        currentCustomer = customersLeft[randomIndex];
        customersLeft.RemoveAt(randomIndex);

        currentCustomerNumber++;

        IngredientManager.Instance.CreateNewDrinkOrder();
    }

    public void ShowFinalDialogue(bool isCorrectDrink)
    {

    }
}
