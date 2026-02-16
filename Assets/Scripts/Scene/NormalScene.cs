using UnityEngine;
using Unity.Cinemachine;
using System.Collections.Generic;
using System.Collections;

public class NormalScene : SceneInfo
{
    [SerializeField] private List<GameObject> difficultyObjects;
    [SerializeField] private Light gravityLight;
    private float targetIntensity = 40f;
    [SerializeField] private CinemachineCamera firstCamera;
    [SerializeField] private CinemachineCamera secondCamera;
    private PlayerMovement playerMovement;
    private Coroutine gravityCoroutine;
    private float waitGraivityTime = 2f;
    private float gravityTimerLimit = 5f;

    private void Awake()
    {
        playerMovement = FindAnyObjectByType<PlayerMovement>();
        gravityLight.intensity = 0f;
    }
    public override void initializeGravity()
    {
        gravityCoroutine = StartCoroutine(bloomGravity());
    }
    private IEnumerator bloomGravity()
    {
        float timer = 0f;
        
        while (true)
        {
            while(timer < gravityTimerLimit)
            {
                gravityLight.intensity = Mathf.Lerp(0f, targetIntensity, timer / gravityTimerLimit);
                timer += Time.deltaTime;
                yield return null;
            }
            timer = 0f;
            playerMovement.chnageGravity();
            yield return new WaitForSeconds(waitGraivityTime);
            while(timer < gravityTimerLimit)
            {
                gravityLight.intensity = Mathf.Lerp(0f, targetIntensity, timer / gravityTimerLimit);
                timer += Time.deltaTime;
                yield return null;
            }
            timer = 0f;
            playerMovement.chnageGravity();
            yield return new WaitForSeconds(waitGraivityTime);
        }
    }

    public override void transCamera()
    {
        
    }

    public override void lowerDifficulty()
    {
        
    }
}
