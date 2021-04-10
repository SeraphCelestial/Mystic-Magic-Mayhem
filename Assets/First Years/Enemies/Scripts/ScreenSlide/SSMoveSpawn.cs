using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SSMoveSpawn : MonoBehaviour
{
    //Main camera and name
    public string cameraName = "Main Camera";
    Camera mainCamera;

    //Holds move speed
    public float speed = 2;

    //Holds screen dimensions and camera position
    Vector2 screenDimensions;
    Vector2 cameraPos;

    //Holds screen bounds
    float topBound;
    float bottomBound;
    float rightBound;
    float leftBound;

    float offset = 2; //Holds offscreen offset
    Vector3 moveVector; //Holds movement vector

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

        //Get random startside and if diagonal
        int startSide = Random.Range(1, 5);
        bool diagonal = (Random.value > 0.50f);

        //Some variables to temporarily hold things in the switch
        float newX;
        float newY;
        Vector3 moveTo;
        //Find which was chosen and move accordingly
        switch (startSide)
        {
            //Top------------------------------------------------------------------------------
            case 1:

                if (diagonal)
                {
                    //Find point inbetween points given
                    newX = rightBound + offset;
                    newY = topBound + (offset * 0.60f);

                    //Get new spawn postiiton and set postiton to it
                    rb2.transform.position = new Vector2(newX, newY);

                    //Get point to move to
                    moveTo = new Vector3(leftBound - offset, bottomBound - (offset * 0.60f), 0);

                    //Get a vector that points to the move position
                    moveVector = (moveTo - transform.position).normalized;
                }
                else
                {
                    //Find point inbetween points given
                    newX = Random.Range(leftBound + 0.5f, rightBound - 0.5f);
                    newY = topBound + offset;

                    //Get new spawn postiiton and set postiton to it
                    rb2.transform.position = new Vector2(newX, newY);

                    //Get point to move to
                    moveTo = new Vector3(newX, bottomBound - offset, 0);

                    //Get a vector that points to the move position
                    moveVector = (moveTo - transform.position).normalized;
                }

                break;//End--------------------------------------------------------------------

            //Bottom---------------------------------------------------------------------------
            case 2:
                if (diagonal)
                {
                    //Find point inbetween points given
                    newX = leftBound - offset;
                    newY = bottomBound - (offset * 0.60f);

                    //Get new spawn postiiton and set postiton to it
                    rb2.transform.position = new Vector2(newX, newY);

                    //Get point to move to
                    moveTo = new Vector3(rightBound + offset, topBound + (offset * 0.60f), 0);

                    //Get a vector that points to the move position
                    moveVector = (moveTo - transform.position).normalized;
                }
                else
                {
                    //Find point inbetween points given
                    newX = Random.Range(leftBound + 0.5f, rightBound - 0.5f);
                    newY = bottomBound - offset;

                    //Get new spawn postiiton and set postiton to it
                    rb2.transform.position = new Vector2(newX, newY);

                    //Get point to move to
                    moveTo = new Vector3(newX, topBound + offset, 0);

                    //Get a vector that points to the move position
                    moveVector = (moveTo - transform.position).normalized;
                }

                break;//End----------------------------------------------------------------------------

            //Left-----------------------------------------------------------------------------
            case 3:

                if (diagonal)
                {
                    //Find point inbetween points given
                    newX = leftBound - offset;
                    newY = topBound + (offset * 0.60f);

                    //Get new spawn postiiton and set postiton to it
                    rb2.transform.position = new Vector2(newX, newY);

                    //Get point to move to
                    moveTo = new Vector3(rightBound + offset, bottomBound - (offset * 0.60f), 0);

                    //Get a vector that points to the move position
                    moveVector = (moveTo - transform.position).normalized;
                }
                else
                {
                    //Find point inbetween points given
                    newX = leftBound - offset;
                    newY = Random.Range(topBound - 0.5f, bottomBound + 0.5f);

                    //Get new spawn postiiton and set postiton to it
                    rb2.transform.position = new Vector2(newX, newY);

                    //Get point to move to
                    moveTo = new Vector3(rightBound + offset, newY, 0);

                    //Get a vector that points to the move position
                    moveVector = (moveTo - transform.position).normalized;
                }

                break;//End--------------------------------------------------------------------

            //Right----------------------------------------------------------------------------
            case 4:

                if (diagonal)
                {
                    //Find point inbetween points given
                    newX = rightBound + offset;
                    newY = bottomBound - (offset * 0.60f);

                    //Get new spawn postiiton and set postiton to it
                    rb2.transform.position = new Vector2(newX, newY);

                    //Get point to move to
                    moveTo = new Vector3(leftBound - offset, topBound + (offset * 0.60f), 0);

                    //Get a vector that points to the move position
                    moveVector = (moveTo - transform.position).normalized;
                }
                else
                {
                    //Find point inbetween points given
                    newX = rightBound + offset;
                    newY = Random.Range(topBound - 0.5f, bottomBound + 0.5f);

                    //Get new spawn postiiton and set postiton to it
                    rb2.transform.position = new Vector2(newX, newY);

                    //Get point to move to
                    moveTo = new Vector3(leftBound - offset, newY, 0);

                    //Get a vector that points to the move position
                    moveVector = (moveTo - transform.position).normalized;
                }

                break;//End----------------------------------------------------------------------------
        }
    }

    void Update()
    {
        //Add movement
        rb2.velocity = moveVector * speed;

        //Remove enemy if to far offscreen
        if (transform.position.x > rightBound + (offset + 1))
        {
            Destroy(gameObject);
        }
        else if (transform.position.x < leftBound - (offset + 1))
        {
            Destroy(gameObject);
        }
        else if (transform.position.y > topBound + (offset + 1))
        {
            Destroy(gameObject);
        }
        else if (transform.position.y < bottomBound - (offset + 1))
        {
            Destroy(gameObject);
        }
    }

    public bool onScreen()
    {
        //Checks if on screen for shoot code (Did it here becuase I already have the bounds)
        if (transform.position.x > rightBound)
        {
            return false;
        }
        else if (transform.position.x < leftBound)
        {
            return false;
        }
        else if (transform.position.y > topBound)
        {
            return false;
        }
        else if (transform.position.y < bottomBound)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
