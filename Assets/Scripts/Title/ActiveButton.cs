using UnityEngine;

public class ActiveButton : MonoBehaviour
{
    [SerializeField] private GameObject targetCanvas;

    public void OnClickActive()
    {
        targetCanvas.SetActive(true);
    }
}
