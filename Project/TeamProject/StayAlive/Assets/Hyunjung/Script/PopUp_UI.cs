using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUp_UI : MonoBehaviour {

    bool isPause = false;
    public GameObject[] popUp;
    public GameObject inven_act_obj;
    public Inven_Active inven_act;
    public GameObject guidBook;

    // 오디오 변수
    public AudioSource Musicplayer;
    public AudioClip click;

    public Camera customizing_camera,main_cam;

	// Use this for initialization
	void Start () {
        //게임 시작시 UI 창 비활성화 
        popUp[0].SetActive(false);
        popUp[1].SetActive(false);
        popUp[2].SetActive(true);
        popUp[3].SetActive(false);
        popUp[4].SetActive(false);
        guidBook.SetActive(false);
        //PopUp_UI.instance = this;
        inven_act = inven_act_obj.GetComponent<Inven_Active>();
        Musicplayer = GetComponent<AudioSource>();
        Musicplayer.clip = click;
    }
    private void Update()
    { 
        if (Input.GetKeyDown(KeyCode.I)) // I 키 입력시 인벤토리 창 활성화
        {
            Inventory();
            popUp[0].SetActive(true);
            Debug.Log("눌림");
        }
        if (Input.GetKeyDown(KeyCode.M)) // M 키 입력시 아이템제작 창 활성화
        {
            Make();
        }
        if (Input.GetKeyDown(KeyCode.Escape)) // esc키 입력시 게임 설정창 활성화
        {
            onPause();
        }
    }

    public void Inventory() // 인벤토리 창 활성화 함수
    {
        Musicplayer.Play();
        popUp[0].SetActive(true);
    }
    public void CloseInven() // 인벤토리 창 비활성화 함수
    {
        Musicplayer.Play();
        popUp[0].SetActive(false);
        inven_act.player_choose = Inven_Active.State.notChoose;
        main_cam.enabled = true;
        customizing_camera.enabled = false;
    }
    public void Make() // 아이템제작 창 활성화 함수
    {
        Musicplayer.Play();
        popUp[1].SetActive(true);
    }
    public void CloseMake() //  아이템 제작 창 비활성화 함수
    { 
        Musicplayer.Play();
        popUp[1].SetActive(false);
    }
    public void Item() // 제작된 아이템 창 활성화 함수
    {
        Musicplayer.Play();
        popUp[2].SetActive(true);
        popUp[3].SetActive(false);
    }
    public void MakedItems()  //제작된 아이템 창 비활성화 함수
    {
        Musicplayer.Play();
        popUp[2].SetActive(false);
        popUp[3].SetActive(true);

        customizing_camera.enabled = true;
        main_cam.enabled = false;
    }
    public void GuidBook() // 게임 설명창 활성화
    {
        Musicplayer.Play();
        guidBook.SetActive(true);
    }
    public void GuidBookClose() // 게임 설명창 비활성화
    {
        Musicplayer.Play();
        guidBook.SetActive(false);
    }
    public void onPause() // 게임 일시정지 설정창
    {
        Musicplayer.Play();
        if (!isPause)
        {
            Time.timeScale = 0;
            isPause = true;
            popUp[4].SetActive(true);
        }
        else
        {Musicplayer.Play();
            Time.timeScale = 1;
            isPause = false;
            popUp[4].SetActive(false);
        }
    }
    public void SaveGame() // 게임 저장 함수
    {
        Musicplayer.Play();
        DataController.instance.SaveGameData();
    }

    public void Menu() // 게임 메뉴로 이동하는 함수
    {
        Musicplayer.Play();
        Time.timeScale = 1;
        UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
    }

}
