using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public GameObject hitbox;
    public GameObject playerBullet;
    public GameObject boss;
    public GameObject SpellCreationManager;
    List<GameObject> bullets;
    private Rigidbody2D rb2;
    private float movementSpeed = 5.0f;
    
    void Start()
    {
        rb2 = gameObject.GetComponent<Rigidbody2D>();
        boss = GameObject.FindGameObjectWithTag("Boss");
        SpellCreationManager = GameObject.Find("SpellCreationManager");
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            rb2.velocity = new Vector2(-movementSpeed, 0);
        }
        else if(Input.GetKey(KeyCode.RightArrow))
        {
            rb2.velocity = new Vector2(movementSpeed, 0);
        }
        else if(Input.GetKey(KeyCode.UpArrow))
        {
            rb2.velocity = new Vector2(0, movementSpeed);
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            rb2.velocity = new Vector2(0, -movementSpeed);
        }
        else
        {
            rb2.velocity = Vector2.zero;
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            hitbox.SetActive(true);
            movementSpeed = 2.5f;
        }
        else
        {
            hitbox.SetActive(false);
            movementSpeed = 5.0f;
        }

        if(Input.GetKey(KeyCode.Z))
        {
            Instantiate(playerBullet, new Vector3(gameObject.transform.position.x - .15f, gameObject.transform.position.y + .55f, 0), Quaternion.identity);
            Instantiate(playerBullet, new Vector3(gameObject.transform.position.x + .15f, gameObject.transform.position.y + .55f, 0), Quaternion.identity);
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            bullets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Bullet"));
            foreach(GameObject bullet in bullets)
            {
                UploadScores.cancelCount++;
                Destroy(bullet);
            }
            UploadScores.bombUseCount++;
            UploadScores.CalculateScore();
            SpellCreationManager.GetComponent<UploadScores>().ScoreUpload();
            SpellCreationManager.GetComponent<UploadScores>().RetriveScore();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Boss"))
        {
            gameObject.transform.position = new Vector3(0f, -3.5f, 0f);
            UploadScores.missCount++;
            UploadScores.CalculateScore();
            SpellCreationManager.GetComponent<UploadScores>().ScoreUpload();
            SpellCreationManager.GetComponent<UploadScores>().RetriveScore();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.transform.position = new Vector3(0f, -3.5f, 0f);
            UploadScores.missCount++;
            UploadScores.CalculateScore();
            SpellCreationManager.GetComponent<UploadScores>().ScoreUpload();
            SpellCreationManager.GetComponent<UploadScores>().RetriveScore();
        }
    }
}
