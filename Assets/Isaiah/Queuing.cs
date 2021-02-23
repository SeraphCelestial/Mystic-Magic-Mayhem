using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Queuing : MonoBehaviour
{
    string CurrentPlayer;

    public string Player1Space;
    public string Player2Space;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerChoice()
    {
        if (IsaiahsVars.Player1 == "~")
        {
            CurrentPlayer = IsaiahsVars.Player1;

            Player1Space = CurrentPlayer;
        }
        else if (IsaiahsVars.Player2 == "~")
        {
            CurrentPlayer = IsaiahsVars.Player2;

            Player1Space = CurrentPlayer;
        }
    }
}
