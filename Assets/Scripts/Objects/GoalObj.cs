using UnityEngine;

public class GoalObj : MonoBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneController.Instance.endStage();
        }
    }
}
