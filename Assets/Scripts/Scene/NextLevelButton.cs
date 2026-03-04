using UnityEngine;

public class NextLevelButton : MonoBehaviour
{
    public void OnclickNextLevel()
    {
        SceneLoader.Instance.loadScene(SceneController.Instance.CurStage+1);
    }
}
