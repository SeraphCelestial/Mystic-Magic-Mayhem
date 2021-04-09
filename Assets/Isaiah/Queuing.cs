using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Queuing : MonoBehaviour
{
    string CurrentPlayerName;

    public string Player1Space;
    public string Player2Space;

    public InputField nameInput;
    public GameObject nameInputField;

    public Text Player1;
    public Text Player2;

    string[] player1Data;
    string[] player2Data;

    public static bool isPlayer1;
    public static bool isPlayer2;

    public bool player1Selected;
    public bool player2Selected;

    void Update()
    {
        if(player1Selected == true && player2Selected == true)
        {
            StartGame();
        }
    }

    public void NameChosen()
    {
        nameInput.onEndEdit.AddListener(SubmitName);
    }

    private void SubmitName(string name)
    {
        CurrentPlayerName = name;

        StartCoroutine(PlayerChoice());
    }

    public IEnumerator PlayerChoice()
    {
        nameInputField.SetActive(false);

        StartCoroutine(PullP1());

        yield return new WaitForSeconds(1f);

        StartCoroutine(PullP2());

        yield return new WaitForSeconds(1f);

        if (Player1Space == null || Player1Space.Contains("~"))
        {
            Player1Space = CurrentPlayerName;

            isPlayer1 = true;

            StartCoroutine(MasterScript.Push(250, Player1Space));

            yield return new WaitForSeconds(1f);
        }
        else if (Player2Space == null || Player2Space.Contains("~"))
        {
            Player2Space = CurrentPlayerName;

            isPlayer2 = true;

            StartCoroutine(MasterScript.Push(251, Player2Space));

            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(AssignName());
    }

    public IEnumerator AssignName()
    {
        while (Player2Space.Contains("~"))
        {
            StartCoroutine(PullP2());

            yield return new WaitForSeconds(1.5f);
        }

        StartCoroutine(PullP1());

        yield return new WaitForSeconds(1f);

        player1Data = Player1Space.Split(',');

        StartCoroutine(PullP2());

        yield return new WaitForSeconds(1f);

        Debug.Log(Player2Space);

        player2Data = Player2Space.Split(',');

        if (player1Data[1] != null)
        {
            Player1.text = player1Data[1];
        }

        if (player2Data[1] != null)
        {
            Player2.text = player2Data[1];
        }

        yield return new WaitForSeconds(1.5f);
        StartGame();
    }
    
    public IEnumerator PullP1()
    {
        StartCoroutine(MasterScript.Pull("250", IsaiahsVars.varToAssign));

        yield return new WaitForSeconds(.5f);

        Player1Space = IsaiahsVars.varToAssign;
    }

    public IEnumerator PullP2()
    {
        StartCoroutine(MasterScript.Pull("251", IsaiahsVars.varToAssign));

        yield return new WaitForSeconds(.5f);

        Player2Space = IsaiahsVars.varToAssign;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

}
