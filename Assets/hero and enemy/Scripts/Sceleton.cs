using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sceleton : MonoBehaviour
{
    GameObject hero;
    Animator aim;
    public float skeletDistance;
    float speed = 1.5f;
    private Rigidbody2D enemyRb;
    float posX;
    protected void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        enemyRb = GetComponent<Rigidbody2D>();
        aim = GetComponent<Animator>();
        posX = transform.position.x;
    }

    protected void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag != hero.tag)
        {     
                Destroy(gameObject);
        }

    }
    void FixedUpdate()
    {
        float distance = posX- hero.transform.position.x;
        if (distance < 5f + skeletDistance)
        {
            Run();
        }
        else
        {
            aim.SetInteger("State", 1);
            enemyRb.velocity = new Vector2(0, enemyRb.velocity.y);
            posX = transform.position.x;
        }
    }
    private void Run()
    {
        aim.SetInteger("State", 0);
        enemyRb.velocity = new Vector2(-speed, enemyRb.velocity.y);
        
    }
}
