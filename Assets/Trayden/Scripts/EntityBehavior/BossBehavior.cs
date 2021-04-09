using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
    public int defeatCount = 0;
    public float bossHealth = 1000;
    public GameObject player;
    public GameObject SpellCreationManager;
    public GameObject[] leftovers;

    void Start()
    {
        SpellCreationManager = GameObject.Find("SpellCreationManager");
        SpellCreationManager.GetComponent<CreateSpells>().StartCoroutine("GenSpell1");
    }
    

    void Update()
    {
        if(bossHealth <= 0)
        {
            defeatCount++;
            if(defeatCount == 1)
            {
                bossHealth = 1000;
                SpellCreationManager.GetComponent<CreateSpells>().StopAllCoroutines();
                leftovers = GameObject.FindGameObjectsWithTag("Bullet");
                foreach(GameObject bullet in leftovers)
                {
                    Destroy(bullet);
                }
                SpellCreationManager.GetComponent<CreateSpells>().StartCoroutine("GenSpell2");
            }
            if(defeatCount == 2)
            {
                bossHealth = 1000;
                SpellCreationManager.GetComponent<CreateSpells>().StopAllCoroutines();
                leftovers = GameObject.FindGameObjectsWithTag("Bullet");
                foreach(GameObject bullet in leftovers)
                {
                    Destroy(bullet);
                }
                SpellCreationManager.GetComponent<CreateSpells>().StartCoroutine("GenSpell3");
            }
            if(defeatCount == 3)
            {
                SceneManager.LoadScene("FinalResults");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            bossHealth = bossHealth - .5f;
            Destroy(collision.gameObject);
        }
    }
}
