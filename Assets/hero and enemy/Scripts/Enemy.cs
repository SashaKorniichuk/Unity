using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Enemy : MonoBehaviour
{
    GameObject hero;
    Animator aim;
    float speed = 1f;
    private SpriteRenderer sprite;
    private Rigidbody2D enemyRb;
    public Transform[] points = new Transform[2];
    bool OnRight;
 
    protected void Awake()
    {
        hero = GameObject.FindGameObjectWithTag("Player");
        sprite = GetComponentInChildren<SpriteRenderer>();
        enemyRb = GetComponent<Rigidbody2D>();
        aim = GetComponent<Animator>();

    }
    protected void FixedUpdate()
    {
        Run();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "DownBarer")
            Destroy(gameObject);
    }
    protected void OnTriggerEnter2D(Collider2D collider)
    {

        if (collider.gameObject.tag == hero.tag)
        {
            if (Mathf.Abs(hero.transform.position.x - transform.position.x) < 0.35f)
                Destroy(gameObject);
            //else
            // hero.lives--;
        }

    }
    private void Run()
    {
        aim.SetInteger("State",1);
        sprite.flipX = OnRight;
        if(gameObject.transform.position.x<points[0].position.x)
        {
            OnRight = true;
        }
        else if(gameObject.transform.position.x > points[1].position.x)
        {
            OnRight = false;
        }
        if(OnRight)
        {
            enemyRb.velocity = new Vector2(speed, enemyRb.velocity.y);
        }
        else
        {
            enemyRb.velocity = new Vector2(-speed, enemyRb.velocity.y);

        }
    }
}
