using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicSingleton : MonoBehaviour
{
    private static MenuMusicSingleton instance = null;
    public static MenuMusicSingleton Instance
    {
        get { return instance; }
    }

    void Awake()
    {
        if ((instance != null) && (instance != this))
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        
    }
}
