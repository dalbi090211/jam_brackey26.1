using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StrangeState
{
    Gravity,
    Enhance,
    TransDim,
    LowerDifficulty
}

public class StrangeManager : Singleton<StrangeManager>
{
    public const int STRANGE_COUNT = 4;
    public const int STAGE_COUNT = 1;
    [SerializeField] List<StrangeState> startState;

    [SerializeField] private Slider strangeSlider;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] private GameObject strangeText;
    [SerializeField] private GameObject gameClearCanvas;
    [SerializeField] private ParticleSystem strangeEffectParticle;
    [SerializeField] private int moveFreq;
    [SerializeField] private float textRotateSpeed;
    [SerializeField] private float targetZ;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        strangeText.SetActive(false);
        gameClearCanvas.SetActive(false);
    }

    public void StageClear()
    {
        Debug.Log("Stage Clear");
        SceneLoader.Instance.saveMaxReach(SceneController.Instance.CurStage+1);
        activeClearCanvas();
        resetAll();
    }

    private void resetAll()
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
            while ((StrangeState)randomState == StrangeState.Gravity || strangeQueue.Contains((StrangeState)randomState))
            {
                randomState = Random.Range(0, STRANGE_COUNT);
            }
            strangeQueue.Enqueue((StrangeState)randomState);
        }
        strangeQueue.Enqueue(StrangeState.Gravity);

        for(int i = 0; i < STRANGE_COUNT; i++)
        {
            yield return Timer(SceneController.Instance.getStrangeTimer(i));
            yield return strangeEffect();
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
            // strangeText.text = string.Format("{0:0.0} / {1:0.0}", timer, time);
            yield return null;
        }
    }

    private IEnumerator strangeEffect()
    {
        strangeEffectParticle.Play();
        strangeText.SetActive(true);
        var rt = strangeText.transform; // UI면 RectTransform으로 받아도 OK

        int count = 0;
        while (count < moveFreq)
        {
            float target = (count % 2 == 0) ? targetZ : -targetZ;

            while (Mathf.Abs(Mathf.DeltaAngle(rt.localEulerAngles.z, target)) > 0.1f)
            {
                float step = textRotateSpeed * Time.deltaTime;
                float newZ = Mathf.MoveTowardsAngle(rt.localEulerAngles.z, target, step);
                var e = rt.localEulerAngles;
                e.z = newZ;
                rt.localEulerAngles = e;

                yield return null;
            }
            count++;
        }
        strangeText.SetActive(false);
    }

    private void startStrange(StrangeState state)
    {
        switch (state)
        {
            case StrangeState.Gravity:
                SceneController.Instance.initializeGravity();
                break;

            case StrangeState.Enhance:
                // Movement.enhance
                break;

            case StrangeState.TransDim:
                playerMovement.ToggleDimension();
                DimensionManager.Instance.TransDim();
                SceneController.Instance.transCamera();
                break;

            case StrangeState.LowerDifficulty:
                SceneController.Instance.lowerDifficulty();
                break;
        }
    }

    private void activeClearCanvas()
    {
        gameClearCanvas.SetActive(true);
    }
}
