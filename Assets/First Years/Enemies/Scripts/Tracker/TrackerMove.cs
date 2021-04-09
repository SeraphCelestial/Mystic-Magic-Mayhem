using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerMove : MonoBehaviour
{
    //Player and name
    public string playerName = "TestPlayer";
    GameObject player;

    //Holds move speed
    public int speed = 3;
    //Holds if you want it to look at player
    public bool facePlayer = true;

    //Holds angle data
    Vector3 vectorToPlayer;
    float angleToPlayer;
    Quaternion quaternionToPlayer;

    //Component references
    Rigidbody2D rb2;

    // Start is called before the first frame update
    void Start()
    {
        //Get component references
        rb2 = gameObject.GetComponent<Rigidbody2D>();

        //Finds player based off playerName
        player = GameObject.Find(playerName);

        //Checks if it found something
        if (player == null)
        {
            Debug.LogError("Couldn't find player of name " + playerName);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //----Move to player
        //Get a vector that points to the player
        vectorToPlayer = (player.transform.position - transform.position).normalized;
        //Adds force to tracker
        rb2.velocity = vectorToPlayer * speed;

        if (facePlayer)
        {
            //----Turn to face player
            //Turn the vector into an angle and offset by 90 so it aims correctly
            angleToPlayer = (Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg) - 90;
            //Turn angle into a quaternion around the z axis
            quaternionToPlayer = Quaternion.AngleAxis(angleToPlayer, Vector3.forward);
            //Rotate traker
            rb2.transform.rotation = quaternionToPlayer;
        }
    }
}
