using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidBook : MonoBehaviour {
    //게임 설명을 보여주는 가이드북 변수
    public GameObject page0;
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject page4;

    public int i;
    public int maxPage;

    // 오디오 변수
    public AudioSource Musicplayer;
    public AudioClip page_sound;

	// Use this for initialization
	void Start () {
        i = 0;
        Musicplayer = GetComponent<AudioSource>();
        Musicplayer.clip = page_sound;
	}
	
	// Update is called once per frame
	void Update () {
        // 현재 상태에 따라 다른 페이지를 보여준다
        if (i == 0)
        {
            page0.SetActive(true);
            page1.SetActive(false);
            page2.SetActive(false);
            page3.SetActive(false);
            page4.SetActive(false);
        }
        if (i == 1)
        {
            page0.SetActive(false);
            page1.SetActive(true);
            page2.SetActive(true);
            page3.SetActive(false);
            page4.SetActive(false);
        }
        if(i==2)
        {
            page0.SetActive(false);
            page1.SetActive(false);
            page2.SetActive(false);
            page3.SetActive(true);
            page4.SetActive(true);
        }
	}
    public void RigtPage() // 오른쪽 으로 책 넘기기
    {
        Musicplayer.Play();
        i = i + 1;
        if (i > maxPage)
        {
            i = maxPage;
        }
    }
    public void LeftPage() // 왼쪽으로 책 넘기기
    {
        Musicplayer.Play();
        i = i - 1;

        if (i < 0)
        {
            i = 0;
        }
    }
}
