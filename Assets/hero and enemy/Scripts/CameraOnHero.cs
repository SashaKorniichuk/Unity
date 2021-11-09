using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOnHero : MonoBehaviour
{
    public Vector2 offset = new Vector2(2, 0);
    private float speed =15f;
    private Transform target;
    private int lastX;
    private bool isLeft;
    public GameObject leftBarer;
    public GameObject Finish;
    float posY;
    private void Awake()
    {
        offset = new Vector2(Mathf.Abs(offset.x), offset.y);
        FindPlayer();
        posY = transform.position.y;
    }
    public void FindPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        lastX = Mathf.RoundToInt(target.position.x);
    }

    private void FixedUpdate()
    {

       if(target)
        {
            int currentX = Mathf.RoundToInt(target.position.x);
            if (currentX > lastX)
                isLeft = false;
            else if (currentX < lastX)
                isLeft = true;
            lastX = Mathf.RoundToInt(target.position.x);
            Vector3 v3;
            if (isLeft)
            {
                v3 = new Vector3(target.position.x - offset.x, 0f, transform.position.z);
            }
            else
            {
                v3 = new Vector3(target.position.x + offset.x, 0f, transform.position.z);

            }
            Vector3 currentPosition = Vector3.Lerp(transform.position, v3, speed * Time.deltaTime);
            transform.position = currentPosition;
            var camPos = transform.position;
            if (camPos.x < leftBarer.transform.position.x + 6.5f)
            {
                camPos.x = leftBarer.transform.position.x + 6.5f;
            }
            else if(camPos.x>Finish.transform.position.x-6.5f)
            {
                camPos.x = Finish.transform.position.x - 6.5f;
            }
            else if(camPos.y>posY)
            {
                camPos.y = posY;
            }
                transform.position = camPos;
        }
    }
}
