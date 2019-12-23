using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_House : MonoBehaviour
{

    public GameObject[] Level_House;
    // Use this for initialization
    void Start()
    {
         // 게임 시작시 1,2,3단계 집 오브젝트 비활성화
        if (DataController.instance.gameData.house_1 == false)
        {
            Level_House[0].SetActive(false);
        }
        if (DataController.instance.gameData.house_2 == false)
        {
            Level_House[1].SetActive(false);
        }
        if (DataController.instance.gameData.house_3 == false)
        {
            Level_House[2].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
         // 각 단계의 집이 활성화일때 다른 단계의 집 오브젝트는 비활성화
        if (DataController.instance.gameData.house_3 == true)
        {
            Level_House[2].SetActive(true);
            DataController.instance.gameData.house_1 = false;
            DataController.instance.gameData.house_2 = false;

        }
        else if (DataController.instance.gameData.house_2 == true)
        {
            Level_House[1].SetActive(true);
            DataController.instance.gameData.house_1= false;
            DataController.instance.gameData.house_3 = false;
        }
        else if (DataController.instance.gameData.house_1 == true)
        {
            Level_House[0].SetActive(true);
            DataController.instance.gameData.house_2 = false;
            DataController.instance.gameData.house_3 = false;

        }
        //else return;

        if (DataController.instance.gameData.house_0 == true)
        {
            DataController.instance.gameData.house_1 = false;
            DataController.instance.gameData.house_2 = false;
            DataController.instance.gameData.house_3 = false;

        }

        if (DataController.instance.gameData.house_1 == false)
        {
            Level_House[0].SetActive(false);
        }
        if(DataController.instance.gameData.house_2 == false)
        {
            Level_House[1].SetActive(false);
        }
        if (DataController.instance.gameData.house_3 == false)
        {
            Level_House[2].SetActive(false);
        }

    }
}
