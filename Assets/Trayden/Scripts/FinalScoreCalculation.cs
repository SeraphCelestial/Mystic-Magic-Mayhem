using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class FinalScoreCalculation : MonoBehaviour
{
    public const string PullURL = "http://vgdapi.basmati.org/gets4.php?groupid=am30&row=";
    public GameObject resultsTitleText;
    public GameObject waitText;
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
    private bool p1Ready = false;
    private bool p2Ready = false;

    public void Start() 
    {
        if(Queuing.isPlayer1 == true)
        {
            p1Ready = true;
            StartCoroutine(Pull("9"));
        }
        else if(Queuing.isPlayer2 == true)
        {
            p2Ready = true;
            StartCoroutine(Pull("8"));
        }
    }
    public void Update() 
    {
        if(ReadyToLeaveThisAbomination == true && p1Ready == true && p2Ready == true)
        {   
            resultsTitleText.GetComponent<UnityEngine.UI.Text>().text = "Final Results";
            waitText.SetActive(false);
            TitleButton.SetActive(true);
            ReadyToLeaveThisAbomination = false;
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
            else if(index == 8.ToString() || index == 9.ToString())
            {
                DetermineBool(webRequest.downloadHandler.text);
            }
            else
            {
                PlaceScore(webRequest.downloadHandler.text);
            }
        }
    }
    public void DetermineBool(string webText)
    {
        string[] webData = webText.Split(',');
        if(int.Parse(webData[1]).ToString() == 100.ToString())
        {
            if(Queuing.isPlayer1 == true)
            {
                p2Ready = true;
            }
            else if(Queuing.isPlayer2 == true)
            {
                p1Ready = true;
            }
            StartCoroutine("PullData");
        }
        else
        {
            if(Queuing.isPlayer1 == true)
            {
                StartCoroutine(Pull("9"));
            }
            else if(Queuing.isPlayer2 == true)
            {
                StartCoroutine(Pull("8"));
            }
        }
    }
    public IEnumerator PullData()
    {
        if(Queuing.isPlayer1 == true)
        {
            StartCoroutine(Pull("0"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("1"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("2"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("3"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("4"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("5"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("6"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("7"));
        }
        else if(Queuing.isPlayer2 == true)
        {
            StartCoroutine(Pull("4"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("5"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("6"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("7"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("0"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("1"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("2"));
            yield return new WaitForSeconds(.25f);
            StartCoroutine(Pull("3"));
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
    public IEnumerator ClearData()
    {
        StartCoroutine(MasterScript.Push(0, "0"));
        StartCoroutine(MasterScript.Push(1, "0"));
        StartCoroutine(MasterScript.Push(2, "0"));
        StartCoroutine(MasterScript.Push(3, "0"));
        StartCoroutine(MasterScript.Push(4, "0"));
        StartCoroutine(MasterScript.Push(5, "0"));
        StartCoroutine(MasterScript.Push(6, "0"));
        StartCoroutine(MasterScript.Push(7, "0"));
        StartCoroutine(MasterScript.Push(8, "0"));
        StartCoroutine(MasterScript.Push(9, "0"));
        UploadScores.totalScore = 0;
        UploadScores.cancelCount = 0;
        UploadScores.bombUseCount = 0;
        UploadScores.missCount = 0;
        Queuing.isPlayer1 = false;
        Queuing.isPlayer2 = false;
        yield return null;
    }
}
