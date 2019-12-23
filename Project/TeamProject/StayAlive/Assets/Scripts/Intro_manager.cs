using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class Intro_manager : MonoBehaviour {


    public VideoPlayer vp;

    float term =0f;

	// Use this for initialization
	void Start () {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        vp = GetComponent<VideoPlayer>();
        vp.Play();
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(vp.isPlaying);
        term += Time.deltaTime;
        if (term >= 2f)
        {
            if (vp.isPlaying == false)
            {
                SceneManager.LoadScene("menu");
            }
        }
	}

    public void Skip_button()
    {
        SceneManager.LoadScene("menu");
    }
}
