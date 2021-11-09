using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRotate : MonoBehaviour
{
    //private HeroScript hero;
    GameObject player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position =  player.transform.position;
       // hero = Resources.Load<HeroScript>("Hero");
    }
    private void FixedUpdate()
    {
        
        transform.Rotate(0, 0, -5f*transform.parent.GetComponent<HeroScript>().directionInput);
    }
}
