using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerHealth : MonoBehaviour
{
    //Bullet
    public GameObject bullet; 

    //Player and name
    public string playerName = "TestPlayer";
    GameObject player;

    //Holds health
    public float health = 3f;

    //Holds angle data
    Vector3 vectorToPlayer;
    float angleToPlayer;
    Quaternion quaternionToPlayer;

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
        //Checks if dead
        if (health <= 0)
        {
            //Get a vector that points to the player
            vectorToPlayer = player.transform.position - transform.position;

            //Do this 12 times 
            for (int i = 0; i <= 11; i++)
            {
                //Turn the vector into an angle and offset by 90 so it aims correctly
                //Offset by (i * 30) so it makes a circle spread
                angleToPlayer = (Mathf.Atan2(vectorToPlayer.y, vectorToPlayer.x) * Mathf.Rad2Deg) - 90 + (i * 30);

                //Turn angle into a quaternion around the z axis
                quaternionToPlayer = Quaternion.AngleAxis(angleToPlayer, Vector3.forward);

                //Create bullet clone
                Instantiate(bullet, gameObject.transform.position, quaternionToPlayer);
            }

            //Destorys itself
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            health = health - .3f;
            Destroy(collision.gameObject);
        }
    }
}
