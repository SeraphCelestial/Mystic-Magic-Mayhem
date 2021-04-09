using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButtonBehavior : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Queuing");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
