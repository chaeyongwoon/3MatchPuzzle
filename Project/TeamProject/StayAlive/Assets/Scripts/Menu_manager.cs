using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Menu_manager : MonoBehaviour
{


    public GameObject Fadeout;
    public float w, h;
    // Use this for initialization

    public GameObject screen_size_panel;
    public bool setting = false;

    public AudioSource Musicplayer;
    public AudioClip click_sound;

    public GameObject stage_select_panel;
    public bool select = false;

    public Text stage_text;

    public enum State { none,stage1, stage2, stage3 }
    public GameObject stage2_lock, stage3_lock;
    public State stage_state;
    void Start()
    {
        DataController.instance.gameData.house_0 = true;
        screen_size_panel.SetActive(false);
        Musicplayer = GetComponent<AudioSource>();
        Musicplayer.clip = click_sound;
        stage_select_panel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DataController.instance.gameData.stage2 = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            DataController.instance.gameData.stage3 = true;
        }

        if (DataController.instance.gameData.stage2 == true)
        {
            stage2_lock.SetActive(false);
        }
        if (DataController.instance.gameData.stage3 == true)
        {
            stage3_lock.SetActive(false);
        }
    }

    public void NewGameButton()
    {
        Musicplayer.Play();

        if (stage_state == State.stage1)
        {
            DataController.instance.Data_Initialize();
            SceneManager.LoadScene("new_map");
        }
        else if (stage_state == State.stage2)
        {
            DataController.instance.Data_Initialize();
            SceneManager.LoadScene("stage2");
        }
        else if (stage_state == State.stage3)
        {
            DataController.instance.Data_Initialize();
            SceneManager.LoadScene("stage2");
        }
    }

    public void LoadGameButton()
    {
        Musicplayer.Play();

        if (stage_state == State.stage1)
        {
            DataController.instance.LoadGameData();
            if (DataController.instance.gameData.player_Hp < 100)
            {
                DataController.instance.gameData.player_Hp = 100;
            }
            if (DataController.instance.gameData.player_Hungry < 50)
            {
                DataController.instance.gameData.player_Hungry = 50;
            }
            DataController.instance._gameData.Hour = 13;
            DataController.instance._gameData.Minute = 0;
            SceneManager.LoadScene("new_map");
        }
        else if (stage_state == State.stage2)
        {
            DataController.instance.LoadGameData();
            if (DataController.instance.gameData.player_Hp < 100)
            {
                DataController.instance.gameData.player_Hp = 100;
            }
            if (DataController.instance.gameData.player_Hungry < 50)
            {
                DataController.instance.gameData.player_Hungry = 50;
            }
            DataController.instance._gameData.Hour = 13;
            DataController.instance._gameData.Minute = 0;
            SceneManager.LoadScene("stage2");
        }
        else if (stage_state == State.stage3)
        {
            DataController.instance.LoadGameData();
            if (DataController.instance.gameData.player_Hp < 100)
            {
                DataController.instance.gameData.player_Hp = 100;
            }
            if (DataController.instance.gameData.player_Hungry < 50)
            {
                DataController.instance.gameData.player_Hungry = 50;
            }
            DataController.instance._gameData.Hour = 13;
            DataController.instance._gameData.Minute = 0;
            SceneManager.LoadScene("stage2");
        }
    }

    public void QuitButton()
    {
        Musicplayer.Play();
        Application.Quit();
    }

    public void first()
    {
        Musicplayer.Play();
        Debug.Log("first");
        Screen.SetResolution(Screen.width, Screen.width * 9 / 16, true);
    }
    public void second()
    {
        Musicplayer.Play();
        Debug.Log("second");
        Screen.SetResolution(Screen.width, Screen.width * 10 / 16, true);
    }
    public void third()
    {
        Musicplayer.Play();
        Debug.Log("third");
        Screen.SetResolution(Screen.width, Screen.width * 3 / 4, true);
    }
    public void fourth()
    {
        Musicplayer.Play();
        Debug.Log("fourth");
        Screen.SetResolution(1024, 768, true);
    }
    public void fifth()
    {
        Musicplayer.Play();
        Debug.Log("fifth");
        Screen.SetResolution(1920, 1080, true);
    }
    public void sixth()
    {
        Musicplayer.Play();
        Debug.Log("sixth");
        Screen.SetResolution(2340, 1080, true);
        //  Screen.width = 1024;
        // Screen.height = 768;

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

    public void Stage_Select()
    {
        if (select == false)
        {
            stage_select_panel.SetActive(true);
            select = true;
        }
        else
        {
            stage_select_panel.SetActive(false);
            select = false;
        }

    }

    public void Stage1_button()
    {
        stage_text.text = string.Format("Stage 1 선택");
        stage_state = State.stage1;
    }

    public void Stage2_button()
    {
        if (DataController.instance.gameData.stage2 == true)
        {
            stage_text.text = string.Format("Stage 2 선택");
            stage_state = State.stage2;
        }
        else
        {
            stage_text.text = string.Format("Stage 2 잠금상태");
            stage_state = State.none;
        }
    }
    public void Stage3_button()
    {
        if (DataController.instance.gameData.stage3 == true)
        {
            stage_text.text = string.Format("Stage 3 선택");
            stage_state = State.stage3;
        }
        else
        {
            stage_text.text = string.Format("Stage 3 잠금상태");
            stage_state = State.none;
        }
    }


}
