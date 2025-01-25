using UnityEngine;

public class SetRenderTextureSizeToScreen : MonoBehaviour
{
    [SerializeField]
    private RenderTexture renderTexture;
    [SerializeField]
    private Camera camera;

    private void Awake()
    {
        renderTexture.width = Screen.width;
        renderTexture.height = Screen.height;

        Rect newRect = camera.rect;
        newRect.width = Screen.width;
        newRect.width = Screen.height;
        camera.rect = newRect;
    }
}
