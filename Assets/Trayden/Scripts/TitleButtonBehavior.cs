using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonBehavior : MonoBehaviour
{
    public GameObject FinalScore;
    public void StartGame()
    {
        SceneManager.LoadScene("Queuing");
    }

    public void ReturnToTitle()
    {
        FinalScore.GetComponent<FinalScoreCalculation>().StartCoroutine("ClearData");
        SceneManager.LoadScene("Title");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
