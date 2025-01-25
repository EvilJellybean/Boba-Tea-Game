using UnityEngine;

public class FrogHand : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer thisRenderer;
    [SerializeField]
    private Sprite frogHandOpen;
    [SerializeField]
    private Sprite frogHandClosed;

    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
    }

    void Update()
    {
        transform.position = (Vector2)camera.ScreenToWorldPoint(Input.mousePosition);

        thisRenderer.sprite = Input.GetMouseButton(0) ? frogHandClosed : frogHandOpen;
    }
}
