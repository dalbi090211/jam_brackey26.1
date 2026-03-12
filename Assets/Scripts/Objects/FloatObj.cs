using UnityEngine;
using System.Collections;

public class FloatObj : MonoBehaviour
{
    float floatTimer = 5f;
    float height = 3.0f;

    private void Start()
    {
        StartCoroutine(floatingObj());
    }

    private IEnumerator floatingObj()
    {
        float timer = 0f;
        float ans = height / floatTimer;
        while (timer <= floatTimer)
        {
            Vector3 tempVec = this.gameObject.transform.position;
            tempVec -= ans;
        }
    }
}
