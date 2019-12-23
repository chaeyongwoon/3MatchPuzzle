using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Defense_Manager : MonoBehaviour {
    public Game_Manager game_ma; // 게임 매니저 스크립트
    public GameObject enemy;
    public Transform[] enemy_spawn;
    public Transform target;
    bool spawn;
    public int i;
    public float fenseHP_Max;
    public float HouseHP_Max;

    // UI관련 변수
    public Slider house_sli;
    public Slider fense1_sli;
    public Slider fense2_sli;
    public Slider fense3_sli;
    public Slider fense4_sli;
    public Slider fense5_sli;
    

    Vector3 enemy_direction;
	// Use this for initialization
	void Start () {

        spawn = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(game_ma.day_state == Game_Manager.State.Day) // 현재 게임의 상태가 '낮'이면 몬스터는 소환되지 않음.
        {
            spawn = false;
        }
		if(game_ma.day_state == Game_Manager.State.Night) // 현재 게임의 상태가  '밤'이면 몬스터 소환
        {
            EnemySpawn();
        }
        fenseHouseHP();
        
    }
    public void Repair() // 집,울타리 수리 함수
    {    
        if ((DataController.instance.gameData.big_tree_count >= 2) || (DataController.instance.gameData.tree_count >= 5))
        {
            DataController.instance.gameData.big_tree_count -= 2;
            DataController.instance.gameData.tree_count -= 5;
            DataController.instance.gameData.fense_HP_1 += 10;
            DataController.instance.gameData.fense_HP_2 += 10;
            DataController.instance.gameData.fense_HP_3 += 10;
            DataController.instance.gameData.fense_HP_4 += 10;
            DataController.instance.gameData.fense_HP_5 += 10;
            DataController.instance.gameData.house_HP += 30;
        }
        else
        {
            Debug.Log("나무의 양이 부족합니다");
        }
    }
    public void EnemySpawn() // 몬스터 생성 함수
    {
        if(spawn == false)
        {
            Instantiate(enemy, enemy_spawn[UnityEngine.Random.Range(0,10)].position, enemy.transform.rotation); // 10개의 랜덤한 위치에서 몬스터가 생성된다
            i++;
            if (i == enemy_spawn.Length)
            {
                i = 0;
                spawn = true;
            }
        }

    }
    public void fenseHouseHP()
    {//집 레벨마다 갖고있는 최대 HP가 다름
        HouseHP_Max = DataController.instance.gameData.house_MaxHP;
        //fense와 house의HP를 플레이어가 실시간으로 확인 가능
        fense1_sli.value = DataController.instance.gameData.fense_HP_1 / fenseHP_Max;
        fense2_sli.value = DataController.instance.gameData.fense_HP_2 / fenseHP_Max;
        fense3_sli.value = DataController.instance.gameData.fense_HP_3 / fenseHP_Max;
        fense4_sli.value = DataController.instance.gameData.fense_HP_4 / fenseHP_Max;
        fense5_sli.value = DataController.instance.gameData.fense_HP_5 / fenseHP_Max;
        house_sli.value = DataController.instance.gameData.house_HP / HouseHP_Max;
    }

}
