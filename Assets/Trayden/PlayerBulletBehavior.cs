using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletBehavior : MonoBehaviour
{
    public Rigidbody2D rb2;
    public GameObject boss;
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        rb2.velocity = new Vector2(0, 40);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject == boss && boss.gameObject.name == "Boss1")
        {
            boss.GetComponent<Boss1Behavior>().boss1Health--;
            Destroy(this.gameObject);
        }
    }
}
