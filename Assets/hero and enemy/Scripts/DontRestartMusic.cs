using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DontRestartMusic : MonoBehaviour
{
    private static DontRestartMusic instance;
    public static DontRestartMusic Instance
    {
        get
        {
            if(instance!=null)
            {
                return instance;
            }
            return new GameObject("(singleton) DontRestartMusic").AddComponent<DontRestartMusic>();
        }
    }

    void Start()
    {
        if(instance!=null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
}
