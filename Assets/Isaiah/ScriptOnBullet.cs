using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptOnBullet : MonoBehaviour
{
    public Vector2 target;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 80;

        Vector2 screenPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        target = Camera.main.ScreenToWorldPoint(screenPos);
        target = (target - new Vector2(transform.position.x, transform.position.y));
        target = target.normalized;

        //target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Update is called once per frame
    void Update()
    {
        Forward();
    }


    public void Forward()
    {
        float step = (speed * Time.deltaTime) / 30000;
        transform.GetComponent<Rigidbody2D>().AddForce(target * step, ForceMode2D.Impulse);
    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }
}
