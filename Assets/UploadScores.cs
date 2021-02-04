using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UploadScores : MonoBehaviour
{
    public const string PullURL = "http://vgdapi.basmati.org/gets4.php?groupid=am30&row=";
    public int enemyKillCount = 0;
    public int grazeCount = 0;
    public int cancelCount = 0;
    public static int totalScore = 0;
    public int bombUseCount = 0;
    public int missCount = 0;
    private int bombPointLoss = 10;
    private int missPointLoss = 50;
    public void ScoreUpload()
    {
        StartCoroutine(MasterScript.Push(0, enemyKillCount.ToString()));
        StartCoroutine(MasterScript.Push(1, grazeCount.ToString()));
        StartCoroutine(MasterScript.Push(2, cancelCount.ToString()));
        StartCoroutine(MasterScript.Push(3, totalScore.ToString()));
    }
    public void GrazeBullet()
    {
        grazeCount += 10;
    }
    public void KillEnemy()
    {
        enemyKillCount++;
    }
    public void UseBomb()
    {
        bombUseCount++;
    }
    public void TakeHit()
    {
        missCount++;
    }
    public void RetriveScore()
    {
        StartCoroutine(Pull("3"));
    }
    public static void RetriveScore(string webText)
    {
        string[] webData = webText.Split(',');
        totalScore = int.Parse(webData[1]);
    }
    void Update() 
    {
        totalScore = (enemyKillCount+grazeCount+cancelCount)-((bombUseCount*bombPointLoss)+(missCount*missPointLoss));
        gameObject.GetComponent<Text>().text = totalScore.ToString();
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
                RetriveScore(webRequest.downloadHandler.text);
            }
        }
    }
}
