using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossBehavior : MonoBehaviour
{
    public int defeatCount = 0;
    public float bossHealth = 2000;
    private static int completionValue = 100;
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
                bossHealth = 2000;
                SpellCreationManager.GetComponent<CreateSpells>().StopAllCoroutines();
                leftovers = GameObject.FindGameObjectsWithTag("Bullet");
                foreach(GameObject bullet in leftovers)
                {
                    Destroy(bullet);
                }
                leftovers = GameObject.FindGameObjectsWithTag("Enemy");
                foreach(GameObject bullet in leftovers)
                {
                    Destroy(bullet);
                }
                SpellCreationManager.GetComponent<CreateSpells>().StartCoroutine("GenSpell2");
            }
            if(defeatCount == 2)
            {
                bossHealth = 2000;
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
                if(Queuing.isPlayer1 == true)
                {
                    StartCoroutine(MasterScript.Push(8, completionValue.ToString()));
                }
                if(Queuing.isPlayer2 == true)
                {
                    StartCoroutine(MasterScript.Push(9, completionValue.ToString()));
                }
                SpellCreationManager.GetComponent<UploadScores>().FinalScoreUpload();
                SceneManager.LoadScene("FinalResults");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("PlayerBullet"))
        {
            bossHealth = bossHealth - .3f;
            Destroy(collision.gameObject);
        }
    }
}