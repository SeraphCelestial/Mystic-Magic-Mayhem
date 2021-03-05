using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public GameObject player;

    void Start() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnBecameInvisible() 
    {
        Destroy(this.gameObject);
    }
}
