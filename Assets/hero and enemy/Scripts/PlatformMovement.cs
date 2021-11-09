using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovement : MonoBehaviour
{
    public bool moveX;
    public bool moveY;
    float speed = 1.5f;
    bool moveingRight = true;
    bool moveingUp = true;
    public Transform[] points = new Transform[2];

    void FixedUpdate()
    {

        if (moveX)
        {
            MoveX();
        }
        else if (moveY)
        {
            MoveY();
        }

    }
    private void MoveX()
    {
        if (gameObject.transform.position.x < points[0].position.x)
        {
            moveingRight = true;
        }
        else if (gameObject.transform.position.x > points[1].position.x)
        {
            moveingRight = false;
        }
        if (moveingRight)
        {
            transform.position = new Vector2(transform.position.x + speed * Time.deltaTime, transform.position.y);
        }
        else
        {
            transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
        }
    }
    private void MoveY()
    {
        if (gameObject.transform.position.y > points[0].position.y)
        {
            moveingUp = false;
        }
        else if (gameObject.transform.position.y < points[1].position.y)
        {
            moveingUp = true;
        }
        if (moveingUp)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }
        else
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - speed * Time.deltaTime);
        }
    }
}
