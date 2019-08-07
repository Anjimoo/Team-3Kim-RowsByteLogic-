using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource music;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        this.music.volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}