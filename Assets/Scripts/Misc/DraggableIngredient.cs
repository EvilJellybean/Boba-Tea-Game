using UnityEngine;

public class DraggableIngredient : MonoBehaviour
{
    private static Collider[] results = new Collider[10];

    [SerializeField]
    private LayerMask draggableAreaLayer;
    [SerializeField]
    private SpriteRenderer packetSprite;
    [SerializeField]
    private SpriteRenderer icon;

    private Camera mainCamera;
    private Vector2 lastMousePosition;

    public Ingredient Ingredient { get; private set; }

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void Configure(Ingredient ingredient, int ingredientNumber)
    {
        Ingredient = ingredient;

        Vector3 newPosition = transform.position;
        newPosition.z = -ingredientNumber * 0.01f;
        transform.position = newPosition;
        packetSprite.sortingOrder = 2 + ingredientNumber * 2;
        icon.sortingOrder = 2 + ingredientNumber * 2 + 1;
        icon.sprite = ingredient.PacketImage;
    }

    private void OnMouseDown()
    {
        lastMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDrag()
    {
        Vector2 currentMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector2 offset = currentMousePosition - lastMousePosition;
        lastMousePosition = currentMousePosition;

        transform.position += (Vector3)offset;
    }

    private void OnMouseUp()
    {
        int hitCount = Physics.OverlapSphereNonAlloc(transform.position, 0.1f, results, draggableAreaLayer, QueryTriggerInteraction.Collide);
        if(hitCount > 0 )
        {
            IngredientManager.Instance.SpawnIngredient(this);
            Destroy(gameObject);
        }
    }
}
