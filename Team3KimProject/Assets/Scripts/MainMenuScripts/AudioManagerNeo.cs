using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManagerNeo : MonoBehaviour
{

    [SerializeField]
    public AudioSource music;
    public Slider musicVol;
    [SerializeField]
    AudioClip[] clips;
    int x=0;
    //public Slider soundfxVol;  Will be added after we add sound effects

    // Initialization
    void Start()
    {
        music.volume = 0.4f;
        this.music.clip = clips[0];
        this.music.Play();       
            
        //this.musicVol.value = 0.5f; //PlayerPrefs.GetFloat("MusicVolume");
        //this.soundfxVol.value = PlayerPrefs.GetFloat("FxVolume");

    }

    // Update is called once per frame
    void Update()
    {
        //this.music.volume = musicVol.value;
        if (this.music.clip != clips[0]&&x==0){this.music.Play(); x = 1; }

    }

    public void VolPreference()
    {
        PlayerPrefs.SetFloat("MusicVolume", this.music.volume);
        //PlayerPrefs.SetFloat("FxVolume", this.soundfxVol.value);
    }

    void PlayMusic()
    {
        StartCoroutine("FadeSound");
    }

    IEnumerator FadeSound()
    {
        while(this.music.volume>0.01f)
        {
            music.volume -= Time.deltaTime / 1.0f;
            yield return null;
        }
    }
}
