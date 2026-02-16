using System.Collections.Generic;
using UnityEngine;

public class SceneController : Singleton<SceneController>
{
    [SerializeField] List<SceneInfo> sceneInfos = new List<SceneInfo>();
    public int CurStage = 0;
    void Start()
    {
        if(sceneInfos.Count != StrangeManager.STAGE_COUNT)
        {
            Debug.LogErrorFormat("Scene Controller should have {0} Scene Infos.", StrangeManager.STAGE_COUNT);
        }
    }

    public void startStage()
    {
        if(CurStage < StrangeManager.STAGE_COUNT - 1)
        {
            StartCoroutine(StrangeManager.Instance.StrangeInitializer());
        }
        else
        {
            Debug.LogWarning("Already in the last stage.");
        }
    }

    public void endStage()
    {
        CurStage++;
        StrangeManager.Instance.Reset();
    }

    public void initializeGravity()
    {
        sceneInfos[CurStage].initializeGravity();
    }

    public void transCamera()
    {
        sceneInfos[CurStage].transCamera();
    }

    public void lowerDifficulty()
    {
        sceneInfos[CurStage].lowerDifficulty();
    }
}
