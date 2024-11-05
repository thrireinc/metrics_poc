using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MetricsSystem : MonoBehaviour
{
    // Todo:
    // Criar outros tipos de métricas
    // Criar um sistema de autenticação
    // Separar a lógica de GET e POST em métodos diferentes
    // Criar lógica para nomes genéricos de métricas
    
    void Start()
    {
        StartCoroutine(GetAndPostMetrics());
    }

    public void SendDataToDashboard(string metricName, string metricValue)
    {
        
    }

    public static IEnumerator GetAndPostMetrics()
    {
        // Primeiro, vamos pegar a métrica existente
        UnityWebRequest getRequest = UnityWebRequest.Get("http://thrire.com/metricswithname.php?name=metric_" + System.Environment.MachineName);
        yield return getRequest.SendWebRequest();

        IntMetric metric;

        Debug.Log("Resposta do GET: " + getRequest.downloadHandler.text);
        if (getRequest.isNetworkError || getRequest.isHttpError || getRequest.downloadHandler.text == "null")
        {
            metric = new IntMetric($"metric_{System.Environment.MachineName}", 0);
        }
        else
        {
            metric = JsonUtility.FromJson<IntMetric>(getRequest.downloadHandler.text);
        }
        
        metric.value++;
        string json = JsonUtility.ToJson(metric);
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);

        // Fazer a requisição POST
        UnityWebRequest postRequest = new UnityWebRequest("http://thrire.com/metricswithname.php", "POST");
        postRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        postRequest.downloadHandler = new DownloadHandlerBuffer();
        postRequest.SetRequestHeader("Content-Type", "application/json");

        yield return postRequest.SendWebRequest();

        if (postRequest.isNetworkError || postRequest.isHttpError)
        {
            Debug.Log(postRequest.error);
        }
        else
        {
            Debug.Log("Resposta do POST: " + postRequest.downloadHandler.text);
        }
    }

    [System.Serializable]
    public class IntMetric
    {
        public string name;
        public int value;

        public IntMetric(string name, int value)
        {
            this.name = name;
            this.value = value;
        }
    }
}