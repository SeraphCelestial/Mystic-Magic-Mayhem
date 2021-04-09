using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FinalScoreCalculation : MonoBehaviour
{
    public const string PullURL = "http://vgdapi.basmati.org/gets4.php?groupid=am30&row=";
    public GameObject scoreText;
    public GameObject cancelText;
    public GameObject bombUseText;
    public GameObject missText;
    public GameObject p2ScoreText;
    public GameObject p2CancelText;
    public GameObject p2BombUseText;
    public GameObject p2MissText;
    public GameObject TitleButton;
    private bool ReadyToLeaveThisAbomination = true;

    public void Start() 
    {
        StartCoroutine("PullData");
    }
    public void Update() 
    {
        if(BossBehavior.p1HasKilled == true && BossBehavior.p2HasKilled == true)
        {
            if(ReadyToLeaveThisAbomination == true)
            {
                TitleButton.SetActive(true);
                StartCoroutine("PullData");
                ReadyToLeaveThisAbomination = false;
            }
        }
    }
    public void PlaceScore(string webText)
    {
        string[] webData = webText.Split(',');
        if(scoreText.GetComponent<UnityEngine.UI.Text>().text == "...")
        {
            scoreText.GetComponent<UnityEngine.UI.Text>().text = int.Parse(webData[1]).ToString();
        }
        else if(cancelText.GetComponent<UnityEngine.UI.Text>().text == "...")
        {
            cancelText.GetComponent<UnityEngine.UI.Text>().text = int.Parse(webData[1]).ToString();
        }
        else if(bombUseText.GetComponent<UnityEngine.UI.Text>().text == "...")
        {
            bombUseText.GetComponent<UnityEngine.UI.Text>().text = int.Parse(webData[1]).ToString();
        }
        else if(missText.GetComponent<UnityEngine.UI.Text>().text == "...")
        {
            missText.GetComponent<UnityEngine.UI.Text>().text = int.Parse(webData[1]).ToString();
        }
        else if(p2ScoreText.GetComponent<UnityEngine.UI.Text>().text == "...")
        {
            p2ScoreText.GetComponent<UnityEngine.UI.Text>().text = int.Parse(webData[1]).ToString();
        }
        else if(p2CancelText.GetComponent<UnityEngine.UI.Text>().text == "...")
        {
            p2CancelText.GetComponent<UnityEngine.UI.Text>().text = int.Parse(webData[1]).ToString();
        }
        else if(p2BombUseText.GetComponent<UnityEngine.UI.Text>().text == "...")
        {
            p2BombUseText.GetComponent<UnityEngine.UI.Text>().text = int.Parse(webData[1]).ToString();
        }
        else if(p2MissText.GetComponent<UnityEngine.UI.Text>().text == "...")
        {
            p2MissText.GetComponent<UnityEngine.UI.Text>().text = int.Parse(webData[1]).ToString();
        }
    }
    public IEnumerator Pull(string index)
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
                PlaceScore(webRequest.downloadHandler.text);
            }
        }
    }

    public IEnumerator PullData()
    {
        Queuing.isPlayer1 = true;
        if(Queuing.isPlayer1 == true)
        {
            StartCoroutine(Pull("0"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("1"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("2"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("3"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("4"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("5"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("6"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("7"));
            yield return new WaitForSeconds(.05f);
        }
        else if(Queuing.isPlayer2 == true)
        {
            StartCoroutine(Pull("4"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("5"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("6"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("7"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("0"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("1"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("2"));
            yield return new WaitForSeconds(.05f);
            StartCoroutine(Pull("3"));
            yield return new WaitForSeconds(.05f);
        }
    }

    public IEnumerator ClearData()
    {
        StartCoroutine(MasterScript.Push(0, "~"));
        StartCoroutine(MasterScript.Push(1, "~"));
        StartCoroutine(MasterScript.Push(2, "~"));
        StartCoroutine(MasterScript.Push(3, "~"));
        StartCoroutine(MasterScript.Push(4, "~"));
        StartCoroutine(MasterScript.Push(5, "~"));
        StartCoroutine(MasterScript.Push(6, "~"));
        StartCoroutine(MasterScript.Push(7, "~"));
        UploadScores.totalScore = 0;
        UploadScores.cancelCount = 0;
        UploadScores.bombUseCount = 0;
        UploadScores.missCount = 0;
        BossBehavior.p1HasKilled = false;
        BossBehavior.p2HasKilled = false;
        if(Queuing.isPlayer1 == true)
        {
            Queuing.isPlayer1 = false;
        }
        if(Queuing.isPlayer2 == true)
        {
            Queuing.isPlayer2 = false;
        }
        yield return null;
    }
}
