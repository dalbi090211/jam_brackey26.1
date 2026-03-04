using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System;
using System.Collections.Generic;

using Object = UnityEngine.Object;

public enum SceneName
{
    Title,
    Level1,
    Level2,
    Level3
}

[Serializable]
public class RunRecord
{
    public string runId;          // 고유 ID
    public string startedAtIso;   // 시작 시간(ISO)
    public string endedAtIso;     // 종료 시간(ISO)
    public int score;
    public float playSeconds;
    public int stageReached;
    public bool cleared;
}

[Serializable]
public class SaveData
{
    public int totalPlayCount;
    public float totalPlaySeconds;
    public int maxReachedStage;
    public List<RunRecord> runs = new(); // 플레이 기록들

    public SaveData()
    {
        totalPlayCount = 0;
        totalPlaySeconds = 0f;
        maxReachedStage = 1;
    }
}

public class SceneLoader : Singleton<SceneLoader>
{
    [SerializeField] private List<SceneName> stageScenePairs;
    private string SavePath => Path.Combine(Application.persistentDataPath, "save.json");
    public SaveData Data { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        Load();
    }
    private void Save()
    {
        try
        {
            string json = JsonUtility.ToJson(Data, prettyPrint: true);
            File.WriteAllText(SavePath, json);
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Save Failed: {e.Message}");
        }
    }
    private void Load()
    {
        if (!File.Exists(SavePath))
        {
            Data = new SaveData();
            return;
        }

        try
        {
            string json = File.ReadAllText(SavePath);
            Data = JsonUtility.FromJson<SaveData>(json) ?? new SaveData();
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Save Load Failed: {e.Message}");
            Data = new SaveData();
        }
    }

    public void ResetSaveData()
    {
        Data = new SaveData();
        try
        {
            if (File.Exists(SavePath))
                File.Delete(SavePath);
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Save Delete Failed: {e.Message}");
        }
        Save();
        var all = Object.FindObjectsByType<LevelCanva>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        all[0].GetComponent<LevelCanva>().initializeButton();
        Debug.Log("Save data reset complete.");
        
    }

    public void saveMaxReach(int sceneIdx)
    {
        if(Data.maxReachedStage < sceneIdx)
        {
            Data.maxReachedStage = sceneIdx;
            Save();
        }
    }

    public void loadScene(int sceneIdx)
    {
        SceneManager.LoadScene(stageScenePairs[sceneIdx].ToString());
    }
    public void loadScene(SceneName sceneName)
    {
        SceneManager.LoadScene(sceneName.ToString());
        for(int i = 0; i < stageScenePairs.Count; i++)
        {
            if(stageScenePairs[i] == sceneName)
            {
                SceneController.Instance.CurStage = i;
            }
        }
    }
}
