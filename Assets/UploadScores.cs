using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UploadScores : MonoBehaviour
{
    string pushURL = "http://vgdapi.basmati.org/mods4.php";
    public int enemyKillCount = 100;
    public int grazeCount = 250;
    public int cancelCount = 500;
    public int totalScore = 0;
    public int bombUseCount = 0;
    public int missCount = 0;
    private int bombPointLoss = -1000;
    private int missPointLoss = -5000;
    void Start() 
    {
        StartCoroutine("UploadEnemyScore");
        StartCoroutine("UploadGrazeScore");
        StartCoroutine("UploadCancelScore");
        StartCoroutine("UploadTotalScore");
    }
    void Update() 
    {
        totalScore = (enemyKillCount+grazeCount+cancelCount)-((bombUseCount*bombPointLoss)+(missCount*missPointLoss));
    }

    IEnumerator UploadEnemyScore()
    {
        WWWForm scoreForm = new WWWForm();
        scoreForm.AddField("groupid", "am30");
        scoreForm.AddField("grouppw", "rBcj8PJPke");
        scoreForm.AddField("row", 0);
        scoreForm.AddField("s4", enemyKillCount);

        using (UnityWebRequest www = UnityWebRequest.Post(pushURL, scoreForm))
        {
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Data has been pushed successfully!");
            }
        }
    }
    IEnumerator UploadGrazeScore()
    {
        WWWForm scoreForm = new WWWForm();
        scoreForm.AddField("groupid", "am30");
        scoreForm.AddField("grouppw", "rBcj8PJPke");
        scoreForm.AddField("row", 1);
        scoreForm.AddField("s4", grazeCount);

        using (UnityWebRequest www = UnityWebRequest.Post(pushURL, scoreForm))
        {
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Data has been pushed successfully!");
            }
        }
    }
    IEnumerator UploadCancelScore()
    {
        WWWForm scoreForm = new WWWForm();
        scoreForm.AddField("groupid", "am30");
        scoreForm.AddField("grouppw", "rBcj8PJPke");
        scoreForm.AddField("row", 2);
        scoreForm.AddField("s4", cancelCount);

        using (UnityWebRequest www = UnityWebRequest.Post(pushURL, scoreForm))
        {
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Data has been pushed successfully!");
            }
        }
    }
    IEnumerator UploadTotalScore()
    {
        WWWForm scoreForm = new WWWForm();
        scoreForm.AddField("groupid", "am30");
        scoreForm.AddField("grouppw", "rBcj8PJPke");
        scoreForm.AddField("row", 3);
        scoreForm.AddField("s4", totalScore);

        using (UnityWebRequest www = UnityWebRequest.Post(pushURL, scoreForm))
        {
            yield return www.SendWebRequest();
            if(www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);
            }
            else
            {
                Debug.Log("Data has been pushed successfully!");
            }
        }
    }
}
