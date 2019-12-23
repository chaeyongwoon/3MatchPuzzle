using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour {

    public Game_Manager Gam;
    
    // 자식 오브젝트들을 저장할 변수
        public GameObject Turret1;
    public GameObject Turret2;
    public GameObject Turret3;
    public GameObject Turret4;

    // Use this for initialization
    void Start()
    { // 게임 시작시 터렛 오브젝트 비활성화
        Turret1.SetActive(false);
        Turret2.SetActive(false);
        Turret3.SetActive(false);
        Turret4.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        TurretONOFF();
    }

    public void TurretONOFF()
    {
        if(Gam.day_state == Game_Manager.State.Day) // 게임상으로 '낮'이면 터렛 오브젝트들 비활성화
        {
            Turret1.SetActive(false);
            Turret2.SetActive(false);
            Turret3.SetActive(false);
            Turret4.SetActive(false);
        }


        if (Gam.day_state == Game_Manager.State.Night && DataController.instance.gameData.house_1 == true) // 게임상으로 '밤'이고 집1번이 활성화일경우 1번터렛 활성화
        {
            Turret1.SetActive(true);
        }
        if (Gam.day_state == Game_Manager.State.Night && DataController.instance.gameData.house_2 == true) //게임상으로 '밤'이고 집 2번이 활성화일경우 1,2번터렛 활성화
        {
            Turret1.SetActive(true);
            Turret2.SetActive(true);
        }
        if (Gam.day_state == Game_Manager.State.Night && DataController.instance.gameData.house_3 == true) // 게임상으로 '밤'이고 집3번이 활성화일경우 1,2,3,4번터렛 활성화
        {
            Turret1.SetActive(true);
            Turret2.SetActive(true);
            Turret3.SetActive(true);
            Turret4.SetActive(true);
        }
    }
}
