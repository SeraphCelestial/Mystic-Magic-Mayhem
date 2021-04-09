using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public GameObject bullet;
    public Transform bulletParent;
    public float shootingTime;
    public float shootingMultiplier;

    void Start()
    {
        shootingMultiplier = 3;
    }

    void Update()
    {
        IsaiahsVars.SelectedCharacter = "Spread";

        if (Input.GetMouseButton(0))
        {
            Shoot();    
        }
    }

    public void Shoot()
    {

        if (Time.time > shootingTime)
        {
            shootingTime = Time.time + 1 / shootingMultiplier;
            GameObject BA = Instantiate(bullet, transform.position, transform.rotation, bulletParent);
        }
    }
}
