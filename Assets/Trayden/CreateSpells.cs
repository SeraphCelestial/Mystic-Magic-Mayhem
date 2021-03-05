using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpells : MonoBehaviour
{
    public static Canvas spellSelectionUI;
    public GameObject boss1;
    public GameObject player;
    public GameObject enemyBullet;
    public GameObject enbullet;
    void Start()
    {
        spellSelectionUI = GameObject.FindGameObjectWithTag("SpellUI").GetComponent<Canvas>();
    }

    public void CreateSpell1()
    {
        if(!GameObject.Find("Player(Clone)") && !GameObject.Find("Boss1(Clone)"))
        {
            Instantiate(boss1, new Vector3(0, 3, 0), Quaternion.identity);
            Instantiate(player, new Vector3(0, -3.5f, 0), Quaternion.identity);
        }
        GameObject.Find("Player(Clone)").SetActive(true);
        GameObject.Find("Boss1(Clone)").SetActive(true);
        spellSelectionUI.gameObject.SetActive(false);
        StartCoroutine("GenSpell1");
    }

    public void CreateSpell2()
    {
        if(!GameObject.Find("Player(Clone)") && !GameObject.Find("Boss1(Clone)"))
        {
            Instantiate(boss1, new Vector3(0, 3, 0), Quaternion.identity);
            Instantiate(player, new Vector3(0, -3.5f, 0), Quaternion.identity);
        }
        GameObject.Find("Player(Clone)").SetActive(true);
        GameObject.Find("Boss1(Clone)").SetActive(true);
        spellSelectionUI.gameObject.SetActive(false);
        StartCoroutine("GenSpell2");
    }
    public void CreateSpell3()
    {
        if(!GameObject.Find("Player(Clone)") && !GameObject.Find("Boss1(Clone)"))
        {
            Instantiate(boss1, new Vector3(0, 3, 0), Quaternion.identity);
            Instantiate(player, new Vector3(0, -3.5f, 0), Quaternion.identity);
        }
        GameObject.Find("Player(Clone)").SetActive(true);
        GameObject.Find("Boss1(Clone)").SetActive(true);
        spellSelectionUI.gameObject.SetActive(false);
        StartCoroutine("GenSpell3");
    }

    public void ResetPractice()
    {
        CreateSpells.spellSelectionUI.gameObject.SetActive(true);
        GameObject.Find("Player(Clone)").SetActive(false);
        GameObject.Find("Boss1(Clone)").GetComponent<Boss1Behavior>().boss1Health = 1000;
        GameObject.Find("Boss1(Clone)").SetActive(false);
        StopAllCoroutines();
    }

    IEnumerator GenSpell1()
    {
        float x = Random.Range(-6.44f, 6.45f);
        enbullet = Instantiate(enemyBullet, new Vector3(x, 4.5f, 0), Quaternion.identity);
        enbullet.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -15), ForceMode2D.Impulse);
        yield return new WaitForSeconds(.75f);
        if(boss1.GetComponent<Boss1Behavior>().boss1Health > 0)
        {
            StartCoroutine("GenSpell1");
        }
        yield return null;
    }

    IEnumerator GenSpell2()
    {
        float x = Random.Range(-6.44f, 6.45f);
        float y = Random.Range(-4.5f, 4f);
        enbullet = Instantiate(enemyBullet, new Vector3(x, y, 0), Quaternion.identity);
        yield return new WaitForSeconds(.75f);
        if(boss1.GetComponent<Boss1Behavior>().boss1Health > 0)
        {
            StartCoroutine("GenSpell2");
        }
        yield return null;
    }

    IEnumerator GenSpell3()
    {
        float x = Random.Range(-6.44f, 6.45f);
        float y = Random.Range(-4.5f, 4f);
        enbullet = Instantiate(enemyBullet, new Vector3(x, y, 0), Quaternion.identity);
        enbullet.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        yield return new WaitForSeconds(.75f);
        if(boss1.GetComponent<Boss1Behavior>().boss1Health > 0)
        {
            StartCoroutine("GenSpell3");
        }
        yield return null;
    }
}
