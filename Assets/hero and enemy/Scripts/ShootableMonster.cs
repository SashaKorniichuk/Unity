using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootableMonster : MonoBehaviour
{
    private float rate = 2f;
    private Bullet bullet;
    private void Awake()
    {
        bullet = Resources.Load<Bullet>("Bullet");
    }
    private void Start()
    {
        InvokeRepeating("Shoot",rate,rate);
    }

    private void Shoot()
    {
        Vector3 position = transform.position;
        position.x += -0.5f;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;
         // newBullet.SetParent = gameObject;
        newBullet.Direction = -newBullet.transform.right;
    }
}
