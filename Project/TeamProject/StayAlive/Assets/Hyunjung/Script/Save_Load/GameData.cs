using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class GameData
{

    public enum State
    {
        none,
        nomalpet1,
        nomalpet2,
        legendpet
    }
    public State Pet;
    //플레이어 정보
    public int player_Level;
    public float player_Hp;
    public float player_Hungry;
    public float player_Exp;
    public int player_Pow;
    public float player_MaxHp;
    public float player_MaxHungry;
    public float player_MaxExp;
    public int day_of_game;

    //아이템 재료
    public int stone_count;
    public int leaf_count;
    public int tree_count;
    public int herb_count;
    public int bone_count;
    public int skin_count;
    public int big_tree_count;
    public int meat_count;
    public int rope_count;


    //제작된 아이템 
    public int ripe_meat_count;

    //옷
    public int leaf_dress_count;
    public int skin_dress_count;

    //무기
    public bool stone_knife;
    public bool ax;
    public bool pickax;

    //퀘스트 아이템
    //public bool radio;
    

    //지은 집에 대한 정보저장
    public bool house_0;
    public bool house_1;
    public bool house_2;
    public bool house_3;

    //울타리, 집 내구도 정보
    public float fense_HP_1;
    public float fense_HP_2;
    public float fense_HP_3;
    public float fense_HP_4;
    public float fense_HP_5;
    public float house_HP;
    public float house_MaxHP;



    // 하루 시스템 변수
    public int Day;
    public int Hour;
    public int Minute;

    // 커스터마이징 변수
    public float head;
    public float body;
    public float arms;
    public float legs;
    public float hands;
    public float foots;

    // 엔딩 관련변수
    public bool Raft; // 뗏목 생성되어있는지
    public bool Sos_sign; // sos 사인 생성되어있는지
    public int Rescue_probability; // 7일째에 지나가던 배가 구조할 확률


    //획득한 펫 변수
    public bool nomalpet1;
    public bool nomalpet2;
    public bool legendpet;

    //펫 능력
    public int Damage_Up;
    public float Spead_Up;
    public float Health_Up;

    public float hungry_val;
    //튜토리얼 체크
    //public bool tutorial;

    public bool stage2;
    public bool stage3;
}