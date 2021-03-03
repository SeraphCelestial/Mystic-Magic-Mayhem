using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

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

        StartCoroutine(PullNames());

        yield return new WaitForSeconds(1.5f);

        if (Player1Space == null || Player1Space.Contains("~"))
        {
            Player1Space = CurrentPlayerName;

            StartCoroutine(MasterScript.Push(250, Player1Space));
        }
        else if (Player2Space == null || Player2Space.Contains("~"))
        {
            Player2Space = CurrentPlayerName;

            StartCoroutine(MasterScript.Push(251, Player2Space));
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(AssignName());
    }

    public IEnumerator AssignName()
    {
        while (Player2Space.Contains("~"))
        {
            StartCoroutine(PullNames());

            yield return new WaitForSeconds(1.5f);
        }

        yield return new WaitForSeconds(2f);

        Debug.Log(Player1Space);
        Debug.Log(Player2Space);

        player1Data = Player1Space.Split(',');

        if (player1Data[1] != null)
        {
            Player1.text = player1Data[1];
        }

        player2Data = Player2Space.Split(',');

        if (player2Data[1] != null)
        {
            Player2.text = player2Data[1];
        }
        
    }
    
    public IEnumerator PullNames()
    {
        StartCoroutine(MasterScript.Pull("250", IsaiahsVars.varToAssign));

        yield return new WaitForSeconds(.5f);

        Debug.Log(IsaiahsVars.varToAssign);

        Player1Space = IsaiahsVars.varToAssign;

        StartCoroutine(MasterScript.Pull("251", IsaiahsVars.varToAssign));

        yield return new WaitForSeconds(.5f);

        Debug.Log(IsaiahsVars.varToAssign);

        Player2Space = IsaiahsVars.varToAssign;
    }

    public IEnumerator PullP2()
    {
        StartCoroutine(MasterScript.Pull("251", IsaiahsVars.varToAssign));

        yield return new WaitForSeconds(.5f);

        Player2Space = IsaiahsVars.varToAssign;
    }

}
