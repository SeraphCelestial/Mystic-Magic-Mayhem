using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1Behavior : MonoBehaviour
{
    public float boss1Health = 1000;
    public GameObject player;
    public GameObject SpellCreationManager;

    void Start()
    {
        SpellCreationManager = GameObject.Find("SpellCreationManager");
    }
    

    void Update()
    {
        if(boss1Health <= 0)
        {
           SpellCreationManager.GetComponent<CreateSpells>().ResetPractice();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            boss1Health = boss1Health - .5f;
            Destroy(collision.gameObject);
        }
    }
}
