using UnityEngine;

public class DraggableIngredient : MonoBehaviour
{
    [SerializeField]
    private LayerMask draggableAreaLayer;
    [SerializeField]
    private SpriteRenderer packetSprite;
    [SerializeField]
    private SpriteRenderer icon;
    [SerializeField]
    private AudioClip pickupSfx;
    [SerializeField]
    private AudioClip dropSfx;
    [SerializeField]
    private float pitchRandomness = 0.25f;

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

    private void Update()
    {
        float aspectRatio = (float)Screen.width / (float)Screen.height;

        Vector3 position = transform.position;
        if (position.y < -5)
        {
            position.y = -5;
            transform.position = position;
        }
        if (position.y > 5)
        {
            position.y = 5;
            transform.position = position;
        }

        if (position.x < -5 * aspectRatio)
        {
            position.x = -5 * aspectRatio;
            transform.position = position;
        }
        if (position.x > 5 * aspectRatio)
        {
            position.x = 5 * aspectRatio;
            transform.position = position;
        }
    }

    private void OnMouseDown()
    {
        lastMousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        float pitch = Random.Range(1 - pitchRandomness, 1 + pitchRandomness);
        AudioManager.Instance.Play(pickupSfx, pitch);
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
        Collider2D result = Physics2D.OverlapCircle((Vector2)transform.position, 0.3f, draggableAreaLayer);
        if(result != null)
        {
            IngredientManager.Instance.SpawnIngredient(this);
            Destroy(gameObject);
        }

        float pitch = Random.Range(1 - pitchRandomness, 1 + pitchRandomness);
        AudioManager.Instance.Play(dropSfx, pitch);
    }
}
