using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    Animator aim;
    public PlatformMovement[] platform;
    public bool sticks;
    void Start()
    {
        aim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            aim.SetBool("On", true);
            platform[0].moveY = true;
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {     if (sticks)
            return;
        if (collider.gameObject.tag == "Player")
        {
            aim.SetBool("On", false);
        }
    }
}
