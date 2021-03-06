﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateSpells : MonoBehaviour
{
    public GameObject boss;
    public GameObject player;
    public GameObject bouncyBullet;
    public GameObject enemyBullet;
    public GameObject explosiveBullet;
    public GameObject noMoveEnemy;
    public GameObject tracerEnemy;
    public GameObject enbullet;

    IEnumerator GenSpell1()
    {
        float x = Random.Range(-6.44f, 6.45f);
        enbullet = Instantiate(noMoveEnemy, new Vector3(0, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(1f);
        Destroy(enbullet);
        enbullet = Instantiate(bouncyBullet, new Vector3(x, 4.5f, 0), Quaternion.identity);
        if(boss.GetComponent<BossBehavior>().bossHealth > 0)
        {
            StartCoroutine("GenSpell1");
        }
        yield return null;
    }

    IEnumerator GenSpell2()
    {
        float x = Random.Range(-6.44f, 6.45f);
        float y = Random.Range(-4.5f, 4f);
        enbullet = Instantiate(explosiveBullet, new Vector3(x, y, 0), Quaternion.identity);
        //enbullet = Instantiate(tracerEnemy, new Vector3(0, 0, 0), Quaternion.identity);
        yield return new WaitForSeconds(.75f);
        if(boss.GetComponent<BossBehavior>().bossHealth > 0)
        {
            StartCoroutine("GenSpell2");
        }
        yield return null;
    }

    IEnumerator GenSpell3()
    {
        float x = Random.Range(-6.44f, 6.45f);
        float y = Random.Range(0f, 4f);
        enbullet = Instantiate(enemyBullet, new Vector3(x, y, 0), Quaternion.identity);
        enbullet.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        yield return new WaitForSeconds(.10f);
        if(boss.GetComponent<BossBehavior>().bossHealth > 0)
        {
            StartCoroutine("GenSpell3");
        }
        yield return null;
    }
}
