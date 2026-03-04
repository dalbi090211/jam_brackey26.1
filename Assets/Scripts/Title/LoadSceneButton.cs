using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField] private SceneName sceneName;

    public void OnClickLoadScene()
    {
        SceneLoader.Instance.loadScene(sceneName);
    }
}
