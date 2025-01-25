using UnityEngine;

public class FrogHand : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer thisRenderer;
    [SerializeField]
    private Sprite frogHandOpen;
    [SerializeField]
    private Sprite frogHandClosed;
    [SerializeField]
    private float vibrateAmount = 0.02f;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {
        Vector3 newPosition = (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);

        if(Input.GetMouseButton(0))
        {
            newPosition += (Vector3)(Random.insideUnitCircle * vibrateAmount);
        }

        newPosition.z = transform.position.z;
        transform.position = newPosition;

        thisRenderer.sprite = Input.GetMouseButton(0) ? frogHandClosed : frogHandOpen;


    }
}
