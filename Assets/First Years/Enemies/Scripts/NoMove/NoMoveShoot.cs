using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMoveShoot : MonoBehaviour
{
    //Bullet
    public GameObject bullet; 

    //Player and name
    public string playerName = "TestPlayer";
    GameObject player;

    //Holds seconds between each shot after the first
    public float shootDelayTime = .75f;

    //Holds angle data
    Vector3 vectorToPlayer;
    float angleToPlayer;
    Quaternion quaternionToPlayer;

    //Times out each shot
    float shootTimer = .75f;

    void Start()
    {
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
        //If timer is done
        if (shootTimer <= 0)
        {
            //Call shoot and reset timer
            shoot();
            shootTimer = shootDelayTime;
        }
        else
        {
            //Increment timer
            shootTimer -= Time.deltaTime;
        }
    }

    private void shoot()
    {
        //Get a vector that points to the player
        vectorToPlayer = player.transform.position - transform.position;

        //Do this 5 times 
        //Use the numbers -2 -1 0 1 2 so the offset is a cone spread
        for (int i = -2; i <= 2; i++)
        {
            //Turn the vector into an angle and offset by 90 so it aims correctly
            //Offset by (i * 15) so it makes a cone spread
            angleToPlayer = (Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg) - 90 + (i * 15);

            //Turn angle into a quaternion around the z axis
            quaternionToPlayer = Quaternion.AngleAxis(angleToPlayer, Vector3.forward);

            //Create bullet clone
            Instantiate(bullet, gameObject.transform.position, quaternionToPlayer);
        }
    }
}
