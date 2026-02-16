using System.Collections.Generic;
using UnityEngine;

public abstract class SceneInfo : MonoBehaviour
{
    public static readonly Color32 blueColor = new Color32(0, 0, 255, 255);
    public static readonly Color32 blackColor = new Color32(0, 0, 0, 255);
    [SerializeField] public StrangeState startState;
    [SerializeField] public List<int> strangeTimers = new List<int>();
    public abstract void initializeGravity();
    public abstract void transCamera();
    public abstract void lowerDifficulty();
}
