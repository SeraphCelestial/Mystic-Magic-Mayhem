﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    #region MainVariables
    public float speed; //Speed of bullet
    Vector2 dir; //Direction used for all bullet logic
    public string bulletType; //Name of bullet, how script works
    public GameObject target; //Target of enemy bullets, used for dir, must be set by enemy script when bullet instantiated
    public int bouncesorWarps; //Number of screen bounces or warps bullet gets before being destroyed
    public float aliveTime; //Amount of time exploding bullet lasts before exploding, or laser lasts after fully charged
    public int explodeAmount; //Number of bullets ExplodeEnemy creates
    public GameObject explodeBulletType; //Type of bullet ExplodeEnemy creates
    public Transform enemyBulletParent; //Parent for enemy bullet, to avoid hierarchy clutter
    public float laserSpeed; //Charge speed of laser
    public float homingForce; //Amount of force on homing bullets
    public float homingRange; //How close an enemy has to be for homing bullets to lock on
    #endregion

    #region Bullet Explanations
    /*
     * Player bullet types are as follows:
     * Enemy bullet types are as follows:
     * BasicEnemy, the enemy's basic bullet
     * RicochetEnemy, this is the bullet which bounces of the edge of the screen
     * WarpEnemy, this is the bullet which warps to the opposite side of the screen
     * ExplosiveEnemy, this bullet travels for a set amount of time before exploding into a set amount of bullets
     * LaserEnemy, this is a laser which charges up for several seconds before it can damage the player, 
     * it then damages the player every *seperate* time they touch it, it does not continuously deal damage
     */
    #endregion
    void Start()
    {
        #region Start function for basic enemy bullet
        if (bulletType == "BasicEnemy")
        {
            dir = (target.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir = dir.normalized;
        }
        #endregion

        #region Start function for ricochet enemy bullet
        if (bulletType == "RicochetEnemy")
        {
            dir = (target.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir = dir.normalized;
            
        }
        #endregion

        #region Start function for explosive enemy bullet
        if (bulletType == "ExplosiveEnemy")
        {
            dir = (target.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir = dir.normalized;
            
        }
        #endregion

        #region Start function for warp enemy bullet
        if (bulletType == "WarpEnemy")
        {
            dir = (target.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir = dir.normalized;
        }
        #endregion

        #region Start function for laser enemy bullet
        if (bulletType == "LaserEnemy")
        {
            dir = (target.transform.position - new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir = dir.normalized;
            transform.rotation = Quaternion.FromToRotation(-transform.right, dir);//Rotates laser towards player
            transform.localScale = new Vector3(transform.localScale.x, .1f, 1f);//Sets laser initial scale

            Color color = gameObject.GetComponent<SpriteRenderer>().material.color;//Sets laser at 10% opacity to start
            color.a = .1f;
            this.GetComponent<SpriteRenderer>().material.color = color;

            //Moves laser forward so the center isn't on the enemy firing it
            transform.position += (transform.localScale.x/2) * -transform.right;
            
            
        }
        #endregion

       
    }
    void Update()
    {
        #region Basic movement for most bullets
        if (bulletType == "BasicEnemy" || bulletType == "ExplosiveEnemy" || bulletType == "RicochetEnemy" || bulletType == "WarpEnemy")
        {
            transform.Translate(dir * speed * Time.deltaTime);//Controls basic movement for most bullet types, notice it's translation based as opposed,
            //to force based
        }
        #endregion

        #region Ricochet bullet logic
        if (bulletType == "RicochetEnemy")
        {
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);//Gets position relative to camera view
            //These if statements detect if the bullet hits the edge of the camera and which edge
            if (pos.x < 0.0)
            {
                dir = Vector3.Reflect(dir, Vector3.right);//Use Reflect to bounce the bullet off the edge
                bouncesorWarps--;
            }   
            else if (1.0 < pos.x)
            {
                dir = Vector3.Reflect(dir, Vector3.right);
                bouncesorWarps--;
            }
            else if (pos.y < 0.0)
            {
                dir = Vector3.Reflect(dir, Vector3.up);
                bouncesorWarps--;
            }
            else if (1.0 < pos.y)
            {
                dir = Vector3.Reflect(dir, Vector3.up);
                bouncesorWarps--;
            }
            if (bouncesorWarps < 0)//Destroys enemy when they've used their max number of bounces
            {
                Destroy(gameObject);
            }
            
        }
        #endregion

        #region Explosive bullet logic
        if (bulletType == "ExplosiveEnemy")
        {
            aliveTime -= Time.deltaTime;//Countdown for explosion

            if (aliveTime <= 0)
            {
                for (int i = 0; i < explodeAmount; i++)
                {
                    //Instantiates bullets equal to explodeAmount and sends them in all directions
                    GameObject BA = Instantiate(explodeBulletType, transform.position, transform.rotation, enemyBulletParent);
                    BA.GetComponent<Bullet>().dir = Quaternion.Euler(0, 0, (360 / explodeAmount) * i) * dir;
                }
                Destroy(gameObject);
            }
        }
        #endregion

        #region Warp bullet logic
        if (bulletType == "WarpEnemy")
        {
            //Logic works basically the same as ricochet bullet
            Vector3 pos = Camera.main.WorldToViewportPoint(transform.position);

            if (pos.x < 0.0)
            {
                transform.position = Quaternion.Euler(0, 0, 180) * transform.position;
                bouncesorWarps--;
            }
            else if (1.0 < pos.x)
            {
                transform.position = Quaternion.Euler(0, 0, 180) * transform.position;
                bouncesorWarps--;
            }
            else if (pos.y < 0.0)
            {
                transform.position = Quaternion.Euler(0, 0, 180) * transform.position;
                bouncesorWarps--;
            }
            else if (1.0 < pos.y)
            {
                transform.position = Quaternion.Euler(0, 0, 180) * transform.position;
                bouncesorWarps--;
            }
            if (bouncesorWarps < 0)
            {
                Destroy(gameObject);
            }
        }
        #endregion

        #region Laser enemy logic
        if (bulletType == "LaserEnemy")
        {
            //Handles opacity change over time
            Color color = gameObject.GetComponent<SpriteRenderer>().material.color;
            color.a += .1f * laserSpeed;
            this.GetComponent<SpriteRenderer>().material.color = color;
            //Handles widening over time
            transform.localScale += new Vector3(0f, .1f, 0f) * laserSpeed;
            //Caps width so it doesn't keep going
            if (transform.localScale.y >= 1f)
            {
                transform.localScale = new Vector3(transform.localScale.x, 1f, 1f);
                //Once fully charged laser aliveTime starts ticking
                aliveTime -= Time.deltaTime;
                if (aliveTime <= 0)
                {
                    Destroy(gameObject);
                }
            }
            
        }
        #endregion
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (bulletType == "BasicEnemy" || bulletType == "RicochetEnemy" || bulletType == "ExplosiveEnemy" || bulletType == "WarpEnemy")
        {
            if (collision.gameObject.tag == "Player")
            {
                Destroy(gameObject);
            }
        }
    }
}
