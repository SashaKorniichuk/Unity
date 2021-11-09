﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed=2f;
    private Vector3 direction;
    public Vector3 Direction { set { direction = value; } }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
}
