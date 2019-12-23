using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public float stage_level = 1f;

    ///////////////////// UI ////////////
    public Slider player_health_slider;

    public Text damage;
    public Text defend;
    public Text health;
    public Text Reload;
    public Text healing;

    public Text damage_level;
    public Text defend_level;
    public Text max_health_level;
    public Text healing_level;
    public Text Reload_level;

    public Text damage_value;
    public Text defend_value;
    public Text max_health_value;
    public Text healing_value;
    public Text Reload_value;

    public Text damage_upgrade_price;
    public Text defend_upgrade_price;
    public Text max_health_upgrade_price;
    public Text healing_upgrade_price;
    public Text Reload_upgrade_price;

    public Text money;

    public GameObject shop_panel,pause_panel;

    public GameObject[] Color_image;

    public GameObject[] obj;

    /// ////////////////////////////////////

    // Use this for initialization
    public void Start()
    {
        if (!instance)
        {
            instance = this;
        }


        ////////////////// UI 관련 변수들 초기화 //////////////////
        player_health_slider.value = (DataController.instance.gameData.Current_health / DataController.instance.gameData.Max_health);
        damage.text = string.Format("공격력 : {0}", DataController.instance.gameData.Damage);
        health.text = string.Format(" {0} / {1}", DataController.instance.gameData.Current_health,DataController.instance.gameData.Max_health);
        defend.text = string.Format("방어력 : {0}%", DataController.instance.gameData.Defend);
        healing.text = string.Format("체력회복(1s) : {0}",DataController.instance.gameData.Healing);
        Reload.text = string.Format("공격속도 : {0}s", DataController.instance.gameData.Reload);

        damage_level.text = string.Format("(LV.{0})", DataController.instance.gameData.Damage_level);
        damage_value.text = string.Format("{0}", DataController.instance.gameData.Damage);
        damage_upgrade_price.text = string.Format("{0}",100 * DataController.instance.gameData.Damage_level);
        max_health_level.text = string.Format("(LV. {0})", DataController.instance.gameData.Max_health_level);
        max_health_value.text = string.Format("{0}", DataController.instance.gameData.Max_health);
        max_health_upgrade_price.text = string.Format("{0}", 100 * DataController.instance.gameData.Max_health_level);
        defend_level.text = string.Format("(LV. {0})",DataController.instance.gameData.Defend_level);
        defend_value.text = string.Format("{0}%", DataController.instance.gameData.Defend);
        defend_upgrade_price.text = string.Format("{0}", 1 + 100 * DataController.instance.gameData.Defend_level);
        healing_level.text = string.Format("(LV. {0})", DataController.instance.gameData.Healing_level);
        healing_value.text = string.Format("{0}", DataController.instance.gameData.Healing);
        healing_upgrade_price.text = string.Format("{0}", 100 * DataController.instance.gameData.Healing_level);
        Reload_level.text = string.Format("(LV. {0})",  DataController.instance.gameData.Reload_level);
        Reload_value.text = string.Format("{0}s", DataController.instance.gameData.Reload);
        Reload_upgrade_price.text = string.Format("{0}", 1000 * DataController.instance.gameData.Reload_level);

        if (DataController.instance.gameData.Reload_level == 7)
        {
            Reload.text = string.Format("공격속도 : 0.1s");
            Reload_level.text = string.Format("(LV.MAX)");
            Reload_value.text = string.Format("0.1s");
            Reload_upgrade_price.text = string.Format("Max");
        }
    }

    // Update is called once per frame
    void Update()
    {
        ////////////////// 플레이어의 체력과 금화는 자주 변동되기 때문에 실시간으로 초기화 //////////////////
        player_health_slider.value = (DataController.instance.gameData.Current_health / DataController.instance.gameData.Max_health);
        health.text = string.Format(" {0} / {1} ({2}%)",Mathf.Floor( DataController.instance.gameData.Current_health) ,DataController.instance.gameData.Max_health
            , Mathf.Floor(DataController.instance.gameData.Current_health / DataController.instance.gameData.Max_health * 100));
        money.text = string.Format("{0}", DataController.instance.gameData.Money);

        if (Input.GetKeyDown(KeyCode.Q))
        {
            open_shop();
        }

    }

    public void damage_upgrade() // 공격력 강화 함수
    {
        if (DataController.instance.gameData.Money >= 100 * DataController.instance.gameData.Damage_level)
        {
            DataController.instance.gameData.Money -= 100 * DataController.instance.gameData.Damage_level;
            DataController.instance.gameData.Damage += 1 * DataController.instance.gameData.Damage_level;
            DataController.instance.gameData.Damage_level += 1;
            damage.text = string.Format("공격력 : {0}", DataController.instance.gameData.Damage);
            damage_level.text = string.Format("(LV.{0})",  DataController.instance.gameData.Damage_level);
            damage_value.text = string.Format("{0}", DataController.instance.gameData.Damage);
            damage_upgrade_price.text = string.Format("{0}", 100 * DataController.instance.gameData.Damage_level);
        }
    }
    public void max_health_upgrade() // 최대체력 강화 함수
    {
        if (DataController.instance.gameData.Money >= 100 * DataController.instance.gameData.Max_health_level)
        {
            DataController.instance.gameData.Money -= 100 * DataController.instance.gameData.Max_health_level;
            DataController.instance.gameData.Max_health_level += 1;
            DataController.instance.gameData.Max_health = 100 + 10f * DataController.instance.gameData.Max_health_level;
            max_health_level.text = string.Format("(LV.{0})", DataController.instance.gameData.Max_health_level);
            max_health_value.text = string.Format("{0}", DataController.instance.gameData.Max_health);
            max_health_upgrade_price.text = string.Format("{0}", 100 * DataController.instance.gameData.Max_health_level);
        }
    }
    public void defend_upgrade() // 방어력 강화 함수
    {
        if (DataController.instance.gameData.Money >= 100 * DataController.instance.gameData.Defend_level)
        {
            if (DataController.instance.gameData.Defend_level < 99)
            {
                DataController.instance.gameData.Money -= 100 * DataController.instance.gameData.Defend_level;
                DataController.instance.gameData.Defend_level += 1;
                DataController.instance.gameData.Defend = 0 + 1f * DataController.instance.gameData.Defend_level;
                defend.text = string.Format("방어력 : {0}%",  DataController.instance.gameData.Defend);
                defend_level.text = string.Format("(LV.{0})", DataController.instance.gameData.Defend_level);
                defend_value.text = string.Format("{0}%", DataController.instance.gameData.Defend);
                defend_upgrade_price.text = string.Format("{0}",100 * DataController.instance.gameData.Defend_level);
                if (DataController.instance.gameData.Defend_level == 99)
                {
                    defend_level.text = string.Format("(LV.MAX)");
                }
            }
        }
    }
    public void healing_upgrade() // 체력회복 강화 함수
    {
        if (DataController.instance.gameData.Money >= 100 * DataController.instance.gameData.Healing_level)
        {
            DataController.instance.gameData.Money -= 100 * DataController.instance.gameData.Healing_level;
            DataController.instance.gameData.Healing_level += 1;
            DataController.instance.gameData.Healing = 0 + 0.2f * DataController.instance.gameData.Healing_level;
            healing.text = string.Format("체력회복(1s) : {0}",  DataController.instance.gameData.Healing);
            healing_level.text = string.Format("(LV.{0})", DataController.instance.gameData.Healing_level);
            healing_value.text = string.Format("{0}", DataController.instance.gameData.Healing);
            healing_upgrade_price.text = string.Format("{0}", 100 * DataController.instance.gameData.Healing_level);
        }
    }
    public void Reload_upgrade() // 공격속도 강화 함수
    {
        if (DataController.instance.gameData.Money >= 1000 * DataController.instance.gameData.Reload_level)
        {
            if (DataController.instance.gameData.Reload_level < 7)
            {
                DataController.instance.gameData.Money -= 1000 * DataController.instance.gameData.Reload_level;
                DataController.instance.gameData.Reload_level += 1;
                DataController.instance.gameData.Reload = 0.8f - 0.1f * DataController.instance.gameData.Reload_level;
                Reload.text = string.Format("공격속도 : {0}s", DataController.instance.gameData.Reload);
                Reload_level.text = string.Format("(LV.{0})", DataController.instance.gameData.Reload_level);
                Reload_value.text = string.Format("{0}s", DataController.instance.gameData.Reload);
                Reload_upgrade_price.text = string.Format("{0}", 1000 * DataController.instance.gameData.Reload_level);
                if (DataController.instance.gameData.Reload_level == 7)
                {
                    Reload.text = string.Format("공격속도 : 0.1s");
                    Reload_level.text = string.Format("(LV.MAX)");
                    Reload_value.text = string.Format("0.1s");
                    Reload_upgrade_price.text = string.Format("Max");
                }
            }
        }
    }

    public void open_shop() // 상점 열기 함수
    {
        damage_level.text = string.Format("(LV.{0})", DataController.instance.gameData.Damage_level);
        damage_value.text = string.Format("{0}", DataController.instance.gameData.Damage);
        damage_upgrade_price.text = string.Format("{0}", 100 * DataController.instance.gameData.Damage_level);
        max_health_level.text = string.Format("(LV. {0})", DataController.instance.gameData.Max_health_level);
        max_health_value.text = string.Format("{0}", DataController.instance.gameData.Max_health);
        max_health_upgrade_price.text = string.Format("{0}", 100 * DataController.instance.gameData.Max_health_level);
        defend_level.text = string.Format("(LV. {0})", DataController.instance.gameData.Defend_level);
        defend_value.text = string.Format("{0}%", DataController.instance.gameData.Defend);
        defend_upgrade_price.text = string.Format("{0}", 100 * DataController.instance.gameData.Defend_level);
        healing_level.text = string.Format("(LV. {0})", DataController.instance.gameData.Healing_level);
        healing_value.text = string.Format("{0}", DataController.instance.gameData.Healing);
        healing_upgrade_price.text = string.Format("{0}", 100 * DataController.instance.gameData.Healing_level);

        shop_panel.SetActive(true);
    }

    public void close_shop() // 상점 닫기 함수
    {
        shop_panel.SetActive(false);

    }

    public void Color_change() // 색상변환 함수
    {
        ////////////////// 좌측상단 색상 이미지를 변환하는 함수 //////////////////

        switch (DataController.instance.gameData.Color_num)
        {
            case 0:
                Color_image[0].SetActive(true);
                Color_image[1].SetActive(false);
                Color_image[2].SetActive(false);
                Color_image[3].SetActive(false);
                break;
            case 1:
                Color_image[0].SetActive(false);
                Color_image[1].SetActive(true);
                Color_image[2].SetActive(false);
                Color_image[3].SetActive(false);
                break;
            case 2:
                Color_image[0].SetActive(false);
                Color_image[1].SetActive(false);
                Color_image[2].SetActive(true);
                Color_image[3].SetActive(false);
                break;
            case 3:
                Color_image[0].SetActive(false);
                Color_image[1].SetActive(false);
                Color_image[2].SetActive(false);
                Color_image[3].SetActive(true);

                break;


        }
    }

    public void Pause_panel_open()
    {
        pause_panel.SetActive(true);
        Time.timeScale = 0f;

    }

    public void Pause_panel_close()
    {
        Time.timeScale = 1f;
        pause_panel.SetActive(false);
    }

    public void Save_data()
    {
        
        DataController.instance.SaveGameData();
    }

    public void Menu() 
    {
        //// 메인 메뉴화면으로 이동하고 DontDestroy 가 선언되어있던 오브젝트들 삭제 ///
        pause_panel.SetActive(false);
        Time.timeScale = 1f;
        Destroy(obj[0]);
        Destroy(obj[1]);
        Destroy(obj[2]);
        Destroy(obj[3]);
        Destroy(obj[4]);


        SceneManager.LoadScene("0.menu");
    }
    public void Quit() // 게임종료 버튼 함수
    {
        pause_panel.SetActive(false);
        Time.timeScale = 1f;
        Application.Quit();

    }

}
