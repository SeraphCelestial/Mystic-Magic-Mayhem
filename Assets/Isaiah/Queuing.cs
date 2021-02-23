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

        IsaiahsVars.Player1 = IsaiahsVars.varToAssign;

        if (IsaiahsVars.Player1 == null || IsaiahsVars.Player1.Contains("~"))
        {
            IsaiahsVars.Player1 = CurrentPlayerName;

            Player1Space = CurrentPlayerName;

            StartCoroutine(MasterScript.Push(250, Player1Space));
        }
        else if (IsaiahsVars.Player2 == null || IsaiahsVars.Player2.Contains("~"))
        {
            IsaiahsVars.Player2 = CurrentPlayerName;

            Player2Space = CurrentPlayerName;

            StartCoroutine(MasterScript.Push(251, Player2Space));
        }

        yield return new WaitForSeconds(1f);

        StartCoroutine(AssignName());
    }

    public IEnumerator AssignName()
    {
        StartCoroutine(PullNames());

        yield return new WaitForSeconds(1f);

        player1Data = IsaiahsVars.Player1.Split(',');

        player2Data = IsaiahsVars.Player2.Split(',');

        if (player1Data[1] != null)
        {
            Player1.text = player1Data[1];
        }

        if (player2Data[1] != null)
        {
            Player2.text = player2Data[1];
        }
        
    }
    
    public IEnumerator PullNames()
    {
        StartCoroutine(MasterScript.Pull("250", IsaiahsVars.varToAssign));

        yield return new WaitForSeconds(.2f);

        IsaiahsVars.Player1 = IsaiahsVars.varToAssign;

        StartCoroutine(MasterScript.Pull("251", IsaiahsVars.varToAssign));

        yield return new WaitForSeconds(.2f);

        IsaiahsVars.Player2 = IsaiahsVars.varToAssign;
    }

}
