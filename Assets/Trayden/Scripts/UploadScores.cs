using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UploadScores : MonoBehaviour
{
    public const string PullURL = "http://vgdapi.basmati.org/gets4.php?groupid=am30&row=";
    public static int enemyKillCount = 0;
    public static int cancelCount = 0;
    public static int bombUseCount = 0;
    public static int missCount = 0;
    public static int bombPointLoss = 5;
    public static int missPointLoss = 10;
    public static int totalScore = 0;
    public void ScoreUpload()
    {
        StartCoroutine(MasterScript.Push(0, totalScore.ToString()));
    }
    public void RetriveScore()
    {
        StartCoroutine(Pull("0"));
    }
    public static void RetriveScore2(string webText)
    {
        string[] webData = webText.Split(',');
        totalScore = int.Parse(webData[1]);
    }
    public static void CalculateScore() 
    {
        totalScore = (enemyKillCount+cancelCount+totalScore)-((bombUseCount*bombPointLoss)+(missCount*missPointLoss));
    }
    public static IEnumerator Pull(string index)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(PullURL + index))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = index.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.LogError("An unexpected error has occured whilst trying to pull.");
            }
            else
            {
                RetriveScore2(webRequest.downloadHandler.text);
            }
        }
    }
}
