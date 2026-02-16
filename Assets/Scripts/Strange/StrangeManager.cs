using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StrangeState
{
    Gravity,
    Enhance,
    View,
    LowerDifficulty
}

[System.Serializable]
public class FloatArray
{
    public float[] values;
}

public class StrangeManager : Singleton<StrangeManager>
{
    public const int STRANGE_COUNT = 4;
    public const int STAGE_COUNT = 1;

    [SerializeField] private Slider strangeSlider;
    [SerializeField] private TextMeshProUGUI strangeText;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] List<FloatArray> sceneStrangeTimer;
    [SerializeField] List<StrangeState> startState;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        
    }

    public void Reset()
    {
        resetGravity();
        resetEnhance();
    }

    private void resetGravity()
    {
        
    }

    private void resetEnhance()
    {
        
    }
    

    public IEnumerator StrangeInitializer()
    {
        Queue<StrangeState> strangeQueue = new Queue<StrangeState>();
        strangeQueue.Enqueue(startState[SceneController.Instance.CurStage]);
        for(int i = 1; i < STRANGE_COUNT - 1; i++)
        {
            int randomState = Random.Range(0, STRANGE_COUNT);
            while ((StrangeState)randomState != StrangeState.Gravity && !strangeQueue.Contains((StrangeState)randomState))
            {
                randomState = Random.Range(0, STRANGE_COUNT);
            }
            strangeQueue.Enqueue((StrangeState)randomState);
        }
        strangeQueue.Enqueue(StrangeState.Gravity);

        for(int i = 0; i < STRANGE_COUNT; i++)
        {
            yield return Timer(sceneStrangeTimer[SceneController.Instance.CurStage].values[i]);
            startStrange(strangeQueue.Dequeue());
        }
    }

    private IEnumerator Timer(float time)
    {
        float timer = 0f;
        while(timer < time)
        {
            timer += Time.deltaTime;
            strangeSlider.value = timer / time;
            strangeText.text = string.Format("{0:0.0} / {1:0.0}", timer, time);
            yield return null;
        }
    }

    private void startStrange(StrangeState state)
    {
        switch (state)
        {
            case StrangeState.Gravity:
                SceneController.Instance.initializeGravity();
                break;

            case StrangeState.Enhance:
                playerMovement.enhanceForce();
                break;

            case StrangeState.View:
                playerMovement.chgMove();
                SceneController.Instance.transCamera();
                break;

            case StrangeState.LowerDifficulty:
                SceneController.Instance.lowerDifficulty();
                break;
        }
    }
}
