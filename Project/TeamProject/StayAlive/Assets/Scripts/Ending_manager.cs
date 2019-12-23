using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending_manager : MonoBehaviour {

	


    public void New_Game()
    {
        DataController.instance.Data_Initialize(); // 새 게임 시작시 게임 데이터 초기화

        SceneManager.LoadScene("new_map");
    }

    public void Menu()
    {
        //메뉴 씬으로 이동
        SceneManager.LoadScene("menu");
    }

    public void Exit()
    {
        // 게임 종료 버튼
       // UnityEditor.EditorApplication.isPlaying = false;

        Application.Quit();
    }


}
