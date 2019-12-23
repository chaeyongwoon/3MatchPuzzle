using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_size : MonoBehaviour
{
    public GameObject screen_size_panel;
    public bool setting = false;
    public AudioSource Musicplayer;
    public AudioClip click_sound;
    // Start is called before the first frame update
    void Start()
    {
        screen_size_panel.SetActive(false);
        Musicplayer = GetComponent<AudioSource>();
        Musicplayer.clip = click_sound;
    }

    public void first()
    {
        Musicplayer.Play();        
        Screen.SetResolution(Screen.width, Screen.width * 9 / 16, true);
    }
    public void second()
    {
        Musicplayer.Play();
        Screen.SetResolution(Screen.width, Screen.width * 10 / 16, true);
    }
    public void third()
    {
        Musicplayer.Play();
        Screen.SetResolution(Screen.width, Screen.width * 3 / 4, true);
    }
    public void fourth()
    {
        Musicplayer.Play();
        Screen.SetResolution(1024, 768, true);
    }
    public void fifth()
    {
        Musicplayer.Play();
        Screen.SetResolution(1920, 1080, true);
    }
    public void sixth()
    {
        Musicplayer.Play();
        Screen.SetResolution(2340, 1080, true); 
    }

    public void size_setting()
    {
        Musicplayer.Play();
        if (setting == false)
        {
            screen_size_panel.SetActive(true);
            setting = true;
        }
        else
        {
            screen_size_panel.SetActive(false);
            setting = false;
        }
    }


}
