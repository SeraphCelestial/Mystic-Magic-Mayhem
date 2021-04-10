using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RScreenSpawn : MonoBehaviour
{
    //Main camera and name
    public string cameraName = "Main Camera";
    Camera mainCamera;

    //Holds screen dimensions and camera position
    Vector2 screenDimensions;
    Vector2 cameraPos;

    //Holds screen bounds
    float topBound;
    float bottomBound;
    float rightBound;
    float leftBound;

    //Component references
    Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        //Get component references
        rb2 = gameObject.GetComponent<Rigidbody2D>();

        //Find a Camera with cameraName as it's name
        Camera[] typeList = GameObject.FindObjectsOfType<Camera>();
        for (var i = 0; i < typeList.Length; i++)
        {
            if (typeList[i].gameObject.name == cameraName)
            {
                mainCamera = typeList[i];
            }
        }

        //Checks if it found something
        if (mainCamera == null)
        {
            Debug.LogError("Couldn't find Camera of name " + cameraName);
        }

        //Get screenDimensions and camera positon
        screenDimensions = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        cameraPos = mainCamera.transform.position;

        //Gets screen bounds
        topBound = screenDimensions.y;
        bottomBound = cameraPos.y - (screenDimensions.y - cameraPos.y);
        rightBound = screenDimensions.x;
        leftBound = cameraPos.x - (screenDimensions.x - cameraPos.x);

        //Get random spawn cords 
        //(Random.Range inbetween the two screen bounds, with a 0.5f offset so they dont spawn right on screen edge)
        float randoX = Random.Range(leftBound + 0.5f, rightBound - 0.5f);
        float randoY = Random.Range(bottomBound + 0.5f, topBound - 0.5f);

        //Set enemy position to this x and y
        rb2.transform.position = new Vector2(randoX, randoY);
    }
}
