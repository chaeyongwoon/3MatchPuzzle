using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player_build_house : MonoBehaviour
{

    public GameObject buildHouse_Button; // 집짓기 버튼
    public GameObject buildHouse_pannel; // 집짓기 판넬
    public GameObject notree;

    public int house_1_tree;
    public int house_1_bigtree;
    public int house_2_tree;
    public int house_2_bigtree;
    public int house_3_tree;
    public int house_3_bigtree;

    public Text house1_treetx;
    public Text house1_bigtreetx;
    public Text house2_treetx;
    public Text house2_bigtreetx;
    public Text house3_treetx;
    public Text house3_bigtreetx;


    public GameObject house_hp_panel;

    // Use this for initialization
    void Start()
    { // 게임 시작시 집짓기 버튼, 판넬 비활성화 / 특정 지역에서만 활성화 되도록함
        buildHouse_Button.SetActive(false);
        buildHouse_pannel.SetActive(false);
        notree.SetActive(false);
        house_hp_panel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("b")) // b키를 누르면 집짓기 판넬 활성화
        {
            ClickBuildButton();
        }
        house1_treetx.text = string.Format("{0} / {1}", DataController.instance.gameData.tree_count, house_1_tree.ToString());
        house1_bigtreetx.text = string.Format("{0} / {1}", DataController.instance.gameData.big_tree_count, house_1_bigtree.ToString());
        house2_treetx.text = string.Format("{0} / {1}", DataController.instance.gameData.tree_count, house_2_tree.ToString());
        house2_bigtreetx.text = string.Format("{0} / {1}", DataController.instance.gameData.big_tree_count, house_2_bigtree.ToString());
        house3_treetx.text = string.Format("{0} / {1}", DataController.instance.gameData.tree_count, house_3_tree.ToString());
        house3_bigtreetx.text = string.Format("{0} / {1}", DataController.instance.gameData.big_tree_count, house_3_bigtree.ToString());

        if(DataController.instance.gameData.tree_count <= 0)
        {
            DataController.instance.gameData.tree_count = 0;
        }
        if(DataController.instance.gameData.big_tree_count <= 0)
        {
            DataController.instance.gameData.big_tree_count = 0;
        }
    }

    public void OnTriggerStay(Collider coll)
    {
         // 집짓는 영역에 플레이어가 닿으면 집짓기 버튼 활성화
        if (coll.tag == "buildHouse")
        {
            if (DataController.instance.gameData.house_3 == false)
            {
                buildHouse_Button.SetActive(true);
            }
        }
    }
    public void OnTriggerExit(Collider coll)
    {
        if (coll.tag == "buildHouse") // 집짓는 영역에서 플레이어가 나가면 집짓기 버튼 비활성화
        {
            buildHouse_Button.SetActive(false);
            buildHouse_pannel.SetActive(false);
            notree.SetActive(false);
        }
    }
    public void ClickBuildButton()
    {
        buildHouse_pannel.SetActive(true);
    }
    public void BuildHouse1() // 1단계 집짓기 버튼
    {
        if((DataController.instance.gameData.tree_count >= house_1_tree)&& (DataController.instance.gameData.big_tree_count >= house_1_bigtree))
        {
            DataController.instance.gameData.house_0 = false;
            DataController.instance.gameData.house_1 = true;
            DataController.instance.gameData.house_2 = false;
            DataController.instance.gameData.house_3 = false;
            DataController.instance.gameData.tree_count -= house_1_tree;
            DataController.instance.gameData.big_tree_count -= house_1_bigtree;
            house_hp_panel.SetActive(true);
        }
        else if ((DataController.instance.gameData.tree_count < house_1_tree) || (DataController.instance.gameData.big_tree_count < house_1_bigtree))
        {
            notree.SetActive(true);
        }
    }
    public void BuildHouse2() // 2단계 집짓기 버튼
    {
        if ((DataController.instance.gameData.tree_count >= house_2_tree) && (DataController.instance.gameData.big_tree_count >= house_2_bigtree))
        {
            DataController.instance.gameData.house_0 = false;
            DataController.instance.gameData.house_2 = true;
            DataController.instance.gameData.house_1 = false;
            DataController.instance.gameData.house_3 = false;
            DataController.instance.gameData.tree_count -= house_2_tree;
            DataController.instance.gameData.big_tree_count -= house_2_bigtree;
            house_hp_panel.SetActive(true);
        }
        else if((DataController.instance.gameData.tree_count < house_2_tree) || (DataController.instance.gameData.big_tree_count < house_2_bigtree))
        {
            notree.SetActive(true);
        }
    }
    public void BuildHouse3() // 3단계 집짓기 버튼
    {
        if ((DataController.instance.gameData.tree_count >= house_3_tree) && (DataController.instance.gameData.big_tree_count >= house_3_bigtree))
        {
            DataController.instance.gameData.house_0 = false;
            DataController.instance.gameData.house_3 = true;
            DataController.instance.gameData.house_2 = true;
            DataController.instance.gameData.house_1 = false;
            DataController.instance.gameData.tree_count -= house_3_tree;
            DataController.instance.gameData.big_tree_count -= house_3_bigtree;
            house_hp_panel.SetActive(true);
        }
        else if ((DataController.instance.gameData.tree_count < house_3_tree) || (DataController.instance.gameData.big_tree_count < house_3_bigtree))
        {
            notree.SetActive(true);
        }
    }
    public void CloseNoTree()
    {
        notree.SetActive(false);
    }
    public void CloseBuildHousePannel()
    {
        buildHouse_pannel.SetActive(false);
    }
}
