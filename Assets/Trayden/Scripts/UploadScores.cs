using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class UploadScores : MonoBehaviour
{
    public const string PullURL = "http://vgdapi.basmati.org/gets4.php?groupid=am30&row=";
    public static int enemyKillCount = 0;
    public static int cancelCount = 0;
    public static int bombUseCount = 0;
    public static int missCount = 0;
    public static int bombPointLoss = 2;
    public static int missPointLoss = 3;
    public static int totalScore = 0;
    public static GameObject scoreText;
    public static GameObject opposingScoreText;
    private int rowNumber;
    public void Start() 
    {
        scoreText = GameObject.Find("Score");
        opposingScoreText = GameObject.Find("OpposingScore");
    }
    public void ScoreUpload()
    {
        if(Queuing.isPlayer1 == true)
        {
            rowNumber = 0;
        }
        else if(Queuing.isPlayer2 == true)
        {
            rowNumber = 1;
        }
        StartCoroutine(MasterScript.Push(rowNumber, totalScore.ToString()));
    }
    public void RetriveScore()
    {
        if(Queuing.isPlayer1 == false)
        {
            rowNumber = 0;
        }
        else if(Queuing.isPlayer2 == false)
        {
            rowNumber = 1;
        }
        StartCoroutine(Pull(rowNumber.ToString()));
    }
    public static void RetriveScore2(string webText)
    {
        string[] webData = webText.Split(',');
        opposingScoreText.GetComponent<UnityEngine.UI.Text>().text = int.Parse(webData[1]).ToString();
    }
    public static void CalculateScore() 
    {
        totalScore = (enemyKillCount+cancelCount)-((bombUseCount*bombPointLoss)+(missCount*missPointLoss));
        if(totalScore < 0)
        {
            totalScore = 0;
        }
        scoreText.GetComponent<UnityEngine.UI.Text>().text = totalScore.ToString();
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
