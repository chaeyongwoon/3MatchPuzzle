using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour {

    public GameObject[] obj;


	// Use this for initialization
	void Start () {
        DataController.instance.gameData.Color_num = 1;

        obj[0].SetActive(false);
        obj[1].SetActive(false);
        obj[2].SetActive(false);
        obj[3].SetActive(false);
    }
	
    public void New_start() // 새로 시작하기 버튼 함수
    {

        DataController.instance.Initialize_Data(); // 게임 데이터 초기화함수 호출


        obj[0].SetActive(true);
        obj[1].SetActive(true);
        obj[2].SetActive(true);
        obj[3].SetActive(true);

        Game_Manager.instance.Start();


        SceneManager.LoadScene("1.tutorial");

    }

    public void Game_Load()
    {

        DataController.instance.LoadGameData();

        obj[0].SetActive(true);
        obj[1].SetActive(true);
        obj[2].SetActive(true);
        obj[3].SetActive(true);

        Game_Manager.instance.Start();
        SceneManager.LoadScene("2.town");
    }

    public void Exit_game()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
