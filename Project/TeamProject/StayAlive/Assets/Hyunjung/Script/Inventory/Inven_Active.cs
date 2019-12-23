using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inven_Active : MonoBehaviour
{
    //인벤토리에서 플레이어가 선택한 음식을 먹거나, 옷을 입게 하는 스크립트
    public static Inven_Active instance;
    public enum State
    {
        notChoose,
        meat,
        ripe_meat,
        medicine,
        leaf_dress,
        skin_dress,
        sword,
        axe,
        pickaxe
    }
    public State player_choose;
    public GameObject eatButton;
    public GameObject clothButton;

    public GameObject[] Clo_mat;
    public Material leaf_mat, skin_mat;


    // Use this for initialization
    void Start()
    { // 게임 시작시 아무것도 선택되지 않는 기본값으로 설정
        Inven_Active.instance = this;
        player_choose = State.notChoose;

        eatButton.GetComponent<Button>().enabled = false;
        clothButton.GetComponent<Button>().enabled = false;
        eatButton.GetComponent<Image>().color = Color.grey;
        clothButton.GetComponent<Image>().color = Color.grey;
    }

    // Update is called once per frame
    void Update()
    {  // 선택된 버튼 조건별 활성/비활성화
        if ((player_choose == State.meat) || (player_choose == State.ripe_meat) || (player_choose == State.medicine))
        {
            eatButton.GetComponent<Button>().enabled = true;
            clothButton.GetComponent<Button>().enabled = false;
            eatButton.GetComponent<Image>().color = Color.white;
            clothButton.GetComponent<Image>().color = Color.grey;
        }
        if ((player_choose == State.leaf_dress) || (player_choose == State.skin_dress) || (player_choose == State.sword)
            || (player_choose == State.axe) || (player_choose == State.pickaxe))
        {
            eatButton.GetComponent<Button>().enabled = false;
            clothButton.GetComponent<Button>().enabled = true;
            eatButton.GetComponent<Image>().color = Color.grey;
            clothButton.GetComponent<Image>().color = Color.white;
        }
        if (player_choose == State.notChoose)
        {
            eatButton.GetComponent<Button>().enabled = false;
            clothButton.GetComponent<Button>().enabled = false;
            eatButton.GetComponent<Image>().color = Color.grey;
            clothButton.GetComponent<Image>().color = Color.grey;
        }
    }
    public void Eat()
    { // 생고기 아이템 선택 후 먹을시 배고픔+5 , 체력-5
        if (player_choose == State.meat)
        {
            DataController.instance.gameData.player_Hungry += 5;
            DataController.instance.gameData.player_Hp -= 5;
            // 사용한 아이템 숫자 감소 코드 추가하기.
        }
        if (player_choose == State.ripe_meat)
        { // 익힌고기 아이템 선택 후 먹을시 배고픔 +20 체력 +5
            DataController.instance.gameData.player_Hungry += 20;
            DataController.instance.gameData.player_Hp += 5;
        }
        if (player_choose == State.medicine)
        { // 약 아이템 선택 후 먹을 시 체력 +20
            DataController.instance.gameData.player_Hp += 20;
        }
    }
    public void PutOnCloth()
    {
        if (player_choose == State.leaf_dress)
        { // 나뭇잎 옷 착용시 캐릭터 메테리얼 변경
            Clo_mat[0].GetComponent<Renderer>().material = leaf_mat;
            Clo_mat[1].GetComponent<Renderer>().material = leaf_mat;
            Clo_mat[2].GetComponent<Renderer>().material = leaf_mat;
            Clo_mat[3].GetComponent<Renderer>().material = leaf_mat;
            Clo_mat[4].GetComponent<Renderer>().material = leaf_mat;

            DataController.instance.gameData.Health_Up = 2f; // 캐릭터의 체력을 2배로 늘려주는 변수
            DataController.instance.gameData.hungry_val = 0.3f; // 배고픔이 줄어드는 변수값 0.3 설정 / 기본 배고픔 감소하는값
        }
        else if (player_choose == State.skin_dress)
        { // 가죽 옷 착용시 캐릭터 메테리얼 변경
            Clo_mat[0].GetComponent<Renderer>().material = skin_mat;
            Clo_mat[1].GetComponent<Renderer>().material = skin_mat;
            Clo_mat[2].GetComponent<Renderer>().material = skin_mat;
            Clo_mat[3].GetComponent<Renderer>().material = skin_mat;
            Clo_mat[4].GetComponent<Renderer>().material = skin_mat;

            DataController.instance.gameData.hungry_val = 0.1f; // 배고픔이 줄어드는 변수값 0.1설정 / 기본보다 천천히 배고픔이 감소
            DataController.instance.gameData.Health_Up = 1f; // 캐릭터의 체력을 기존 1배로 줄여주는 변수
            // 능력추가
        }
        else if (player_choose == State.sword)
        { // 검 아이템 선택 후 착용시 검 아이템 착용함수 호출
            Player_State pl_st = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>();
            pl_st.Change_sword();
        }
        else if (player_choose == State.axe)
        {// 도끼 아이템 선택 후 착용시 도끼 아이템 착용함수 호출
            Player_State pl_st = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>(); pl_st.Change_axe();
        }
        else if (player_choose == State.pickaxe)
        {// 곡괭이 아이템 선택 후 착용시 곡괭이 아이템 착용함수 호출
            Player_State pl_st = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>();
            pl_st.Change_pickaxe();
        }

    }
    public void PutOnLeaf()
    { // 나뭇잎 옷 선택
        player_choose = State.leaf_dress;
    }
    public void PutOnSkin()
    { // 가죽 옷 선택
        player_choose = State.skin_dress;
    }
    public void ClickMedicine()
    { // 약 아이템 선ㅌ개
        player_choose = State.medicine;
    }
    public void ClickripeMeat()
    { // 익힌 고기 아이템 선택
        player_choose = State.ripe_meat;
    }
    public void ClickMeat()
    { // 생고기 아이템 선택
        player_choose = State.meat;
    }
    public void ClickSword()
    { // 검 아이템 선택
        player_choose = State.sword;
    }
    public void ClickAxe()
    { // 도끼 아이템 선택
        player_choose = State.axe;
    }
    public void ClickPickaxe()
    {
        // 곡괭이 아이템 선택
        player_choose = State.pickaxe;
    }
}
