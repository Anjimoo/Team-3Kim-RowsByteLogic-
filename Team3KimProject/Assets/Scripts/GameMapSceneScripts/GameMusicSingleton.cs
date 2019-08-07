using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicSingleton : MonoBehaviour
{
    
    private static GameMusicSingleton instance = null;
    public static GameMusicSingleton Instance
    {
        get { return instance; }
    }

    // Start is called before the first frame update
    void Start()
    {
        MenuMusicSingleton.Instance.gameObject.GetComponent<AudioSource>().Stop();//stops the music from the main menu. You can do it with Pause() instead
        
        //this.gameObject.GetComponent<AudioSource>().volume = GameObject.Find("/PreloadScene/App").GetComponent<VolNum>().volNum;
    }

    //void Awake()
    //{
    //    if ((instance != null) && (instance != this))
    //    {
    //        Destroy(this.gameObject);
    //        return;
    //    }
    //    else
    //    {
    //        instance = this;
    //    }
    //    DontDestroyOnLoad(this.gameObject);
    //}

    // Update is called once per frame
    void Update()
    {
        
    }
}