using UnityEngine;

public class HomeButton : MonoBehaviour
{
    public void OnclickNextLevel()
    {
        SceneLoader.Instance.loadScene(0);
    }
}
