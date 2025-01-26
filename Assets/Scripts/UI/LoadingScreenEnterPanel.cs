using System.Collections;
using UnityEngine;

public class LoadingScreenEnterPanel : MonoBehaviour
{
    [SerializeField]
    private float deactivateSeconds = 1;

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(deactivateSeconds);
        gameObject.SetActive(false);
    }
}
