using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuMusicVolumeChange : MonoBehaviour
{
    protected AudioSource audioSource;
    protected float musicVol = 1f;

    // Start is called before the first frame update
    void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        this.audioSource.volume = musicVol;
    }

    //called by the Slider GameObject from menu, gets the value from the slider and sets the volume according to value.
    public void SetVolume(float volume)
    {
        this.musicVol = volume;
        PlayerPrefs.SetFloat("volume", this.musicVol);
    }

}
