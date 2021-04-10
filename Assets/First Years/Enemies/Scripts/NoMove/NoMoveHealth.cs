using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoMoveHealth : MonoBehaviour
{
    public float health = 15f;
    void Update()
    {
        if (health <= 0)
        {
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
