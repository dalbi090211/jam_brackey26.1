using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;
using System.Collections;

public class NormalScene : SceneInfo
{
    [SerializeField] private List<GameObject> difficultyObjects;
    [SerializeField] private Light gravityLight;
    private float targetIntensity = 150f;
    [SerializeField] private CinemachineCamera firstCamera;
    [SerializeField] private CinemachineCamera secondCamera;
    private PlayerMovement playerMovement;
    private Coroutine gravityCoroutine;
    private float waitGraivityTime = 2f;
    private float gravityTimerLimit = 3f;
    private float restGraivityTime = 2f;

    private void Awake()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        gravityLight.intensity = 0f;
    }
    public override void initializeGravity()
    {
        Debug.Log("Initialize Gravity");
        gravityCoroutine = StartCoroutine(bloomGravity());
    }
    private IEnumerator bloomGravity()
    {
        float timer = 0f;
        
        while (true)
        {
            timer = 0f;
            while(timer < gravityTimerLimit)
            {
                gravityLight.intensity = Mathf.Lerp(0f, targetIntensity, timer / gravityTimerLimit);
                timer += Time.deltaTime;
                yield return null;
            }
            playerMovement.chnageGravity();
            yield return new WaitForSeconds(waitGraivityTime);
            playerMovement.chnageGravity();
            timer = 0f;
            while(timer < gravityTimerLimit)
            {
                gravityLight.intensity = Mathf.Lerp(targetIntensity, 0f, timer / gravityTimerLimit);
                timer += Time.deltaTime;
                yield return null;
            }
            yield return new WaitForSeconds(restGraivityTime);
        }
    }

    public override void transCamera()
    {
        Debug.Log("Trans Camera");
        firstCamera.Priority = 0;
        secondCamera.Priority = 10;
    }

    public override void lowerDifficulty()
    {
        Debug.Log("Lower Difficulty");
    }
}
