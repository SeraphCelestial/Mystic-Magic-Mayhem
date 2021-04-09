using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMoveHealth : MonoBehaviour
{
    //Holds health
    public int health = 15;

    // Update is called once per frame
    void Update()
    {
        //Checks if dead
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    //Subtracts health based off given number
    public void damage(int damage)
    {
        health -= damage;
    }

    //Subtracts one hp
    public void damage()
    {
        health--;
    }
}
