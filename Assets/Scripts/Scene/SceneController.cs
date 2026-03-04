using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : Singleton<SceneController>
{
    public static SceneInfo curSceneInfo;
    public int CurStage = 0;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void Bootstrap()
    {
        SceneManager.activeSceneChanged += OnActiveSceneChanged;
        Refresh(); // 첫 씬도 세팅
    }

    private static void OnActiveSceneChanged(Scene oldScene, Scene newScene)
    {
        Refresh();
    }
    private static void Refresh()
    {
        // 비활성 오브젝트 포함해서 찾고 싶으면 true
        var all = Object.FindObjectsByType<SceneInfo>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        if (all.Length == 0)
        {
            curSceneInfo = null;
            Debug.Log($"[SceneInfoProvider] 현재 씬에 SceneInfo 파생 컴포넌트가 없습니다.");
            return;
        }

        if (all.Length > 1)
        {
            // “반드시 하나” 규칙 위반
            curSceneInfo = all[0];
            Debug.LogError($"[SceneInfoProvider] SceneInfo 파생 컴포넌트가 {all.Length}개입니다. 규칙 위반입니다.");
            return;
        }

        curSceneInfo = all[0];
    }
    void Start()
    {
        gameResume();
        startStage();
    }

    public void gameStop()
    {
        Time.timeScale = 0f;
    }
    public void gameResume()
    {
        Time.timeScale = 1f;
    }

    public void startStage()
    {
        if(CurStage == 0) return;
        StartCoroutine(StrangeManager.Instance.StrangeInitializer());
    }

    public int getStrangeTimer(int idx)
    {
        return curSceneInfo.strangeTimers[idx];
    }

    public void endStage()
    {
        StrangeManager.Instance.StageClear();
        gameStop();
    }

    public void initializeGravity()
    {
        curSceneInfo.initializeGravity();
    }

    public void transCamera()
    {
        curSceneInfo.transCamera();
    }

    public void lowerDifficulty()
    {
        curSceneInfo.lowerDifficulty();
    }
}
