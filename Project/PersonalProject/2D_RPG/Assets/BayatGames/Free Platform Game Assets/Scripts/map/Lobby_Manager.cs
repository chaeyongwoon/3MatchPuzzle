﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Lobby_Manager : MonoBehaviour
{

    public Game_Manager gm;
    public GameObject[] portal_panel;
    public int stage_num = 0;

    public Text Infinite_level_text;

    public Transform player;


    // Update is called once per frame
    void Update()
    {
        if (!gm)
        {
            gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
        }

        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
    }




    public void Open_shop()
    {
        gm.open_shop();
    }

    public void Open_portal_panel(int num)
    {
        stage_num = num;
        portal_panel[num].SetActive(true);
    }

    public void Close_portal_panel()
    {
        portal_panel[0].SetActive(false);
        portal_panel[1].SetActive(false);
        portal_panel[2].SetActive(false);
    }

    public void Enter_stage()
    {
        gm.stage_level = 1;
        player.transform.position = new Vector2(0f, 3f);

        switch (stage_num)
        {
            case 0:
                Camera.main.transform.position = new Vector3(0, 3, -10);
                SceneManager.LoadScene("3.stage1");
                break;

            case 1:
                gm.stage_level = 3;

                SceneManager.LoadScene("4.stage2");
                break;

            case 2:
                SceneManager.LoadScene("5.stage_infinite");
                break;
        }
    }

    public void Stage_level_up()
    {
        gm.stage_level += 1;
        if (gm.stage_level >= 100)
        {
            gm.stage_level = 99;
        }
        Infinite_level_text.text = "Level : " + gm.stage_level;
    }

    public void Stage_level_down()
    {
        gm.stage_level -= 1;
        if (gm.stage_level <= 0)
        {
            gm.stage_level = 1;
        }
        Infinite_level_text.text = "Level : " + gm.stage_level;
    }




}
