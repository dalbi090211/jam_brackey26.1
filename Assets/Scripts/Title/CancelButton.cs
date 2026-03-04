using UnityEngine;

public class CancelButton : MonoBehaviour
{
    [SerializeField] private GameObject targetCanvas;

    public void OnClickCancel()
    {
        targetCanvas.SetActive(false);
    }
}
