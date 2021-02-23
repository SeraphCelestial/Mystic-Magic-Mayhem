using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MasterScript : MonoBehaviour
{
    public const string PushURL = "http://vgdapi.basmati.org/mods4.php";
    public const string PullURL = "http://vgdapi.basmati.org/gets4.php?groupid=am30&row=";

    private void Awake()
    {
        StartCoroutine(IsaiahClear());
    }

    public static IEnumerator Push(int index, string content)
    {
        IsaiahsVars.continueClearing = false;

        WWWForm form = new WWWForm(); // Creates new WebRequest form.

        form.AddField("groupid", "am30"); // Name of the database.
        form.AddField("grouppw", "rBcj8PJPke"); // Password for the database.
        form.AddField("row", index); // Row it puts the information.
        form.AddField("s4", content); // Content that it puts on the row.

        using (UnityWebRequest webRequest = UnityWebRequest.Post(PushURL, form))
        {
            yield return webRequest.SendWebRequest(); // Waits for WebRequest.

            IsaiahsVars.continueClearing = true;

            if (webRequest.isNetworkError)
            {
                Debug.LogError("An unexpected error has occured whilst trying to push.");
            }
            else
            {
                Debug.Log("A push " + content + " has been added to row " + index);
            }

            yield return null;
        }
    }

    public static IEnumerator Pull(string index, string varToAssign)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(PullURL + index))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = index.Split(',');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.LogError("An unexpected error has occured whilst trying to pull.");
            }
            else
            {
                varToAssign = webRequest.downloadHandler.text;

                IsaiahsVars.varToAssign = varToAssign;

                Debug.Log(varToAssign);
            }
        }
    }

    public IEnumerator IsaiahClear()
    {
        IsaiahsVars.continueClearing = true;

        for (int i = 249; i >= 249 && i <= 260; i++)
        {
            while(IsaiahsVars.continueClearing == false)
            {
                yield return null;
            }

            StartCoroutine(Push(i, "~"));
        }
    }
    
}
