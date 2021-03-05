using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonBehavior : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Queuing test");
    }

    public void StartPractice()
    {
        SceneManager.LoadScene("BossSpellPracticeMode");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
