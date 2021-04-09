using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public bool Player1Selected;
    public bool Player2Selected;

    public bool isPlayer1;
    public bool isPlayer2;

    void Start()
    {
        
    }

    public IEnumerator CharacterSelect()
    {

    }

    public IEnumerator PlayerChoice()
    {

        StartCoroutine(PullP1());

        yield return new WaitForSeconds(1f);

        StartCoroutine(PullP2());

        yield return new WaitForSeconds(1f);

        if (Player1Selected == false)
        {
            isPlayer1 = true;

            StartCoroutine(MasterScript.Push(252, "true"));

            yield return new WaitForSeconds(1f);
        }
        else if (Player2Selected == false)
        {
            isPlayer2 = true;

            StartCoroutine(MasterScript.Push(253, "true"));

            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator PullP1()
    {
        StartCoroutine(MasterScript.Pull("252", IsaiahsVars.varToAssign));

        yield return new WaitForSeconds(.5f);

        if(IsaiahsVars.varToAssign == "true")
        {
            Player1Selected = true;
        }
    }

    public IEnumerator PullP2()
    {
        StartCoroutine(MasterScript.Pull("253", IsaiahsVars.varToAssign));

        yield return new WaitForSeconds(.5f);

        if (IsaiahsVars.varToAssign == "true")
        {
            Player2Selected = true;
        }
    }

    public void Homing()
    {
        IsaiahsVars.SelectedCharacter = "Homing";
    }
    public void Spread()
    {
        IsaiahsVars.SelectedCharacter = "Spread";
    }
    public void Forward()
    {
        IsaiahsVars.SelectedCharacter = "Forward";
    }
    public void Necro()
    {
        IsaiahsVars.SelectedCharacter = "Necro";
    }
}
