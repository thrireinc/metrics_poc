using UnityEngine;

public class TestController : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(MetricsSystem.GetAndPostMetrics());
        }
    }
}