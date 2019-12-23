using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour
{//  인벤토리 UI에 표시할 아이템 변수들
    public Text[] counter;
    public GameObject stone;
    public GameObject leaf;
    public GameObject tree;
    public GameObject herb;
    public GameObject bone;
    public GameObject skin;
    public GameObject big_tree;
    public GameObject meat;
    public GameObject rope;
    //제작된 아이템
    public GameObject ripe_meat;
    public GameObject leaf_dress;
    public GameObject skin_dress;
    public GameObject stone_kni;
    public GameObject ax;
    public GameObject pickax;
    //public GameObject radio;
    //public GameObject raft;

    void Start()
    {
        Stone();
        Leaf();
        Tree();
        Herb();
        Bone();
        Skin();
        BigTree();
        Meat();
        Rope();
        Ripe_meat();
        Leaf_Dress();
        Skin_Dress();
        Stone_Knife();
        Ax();
        PickAx();

        // 인벤토리에 저장된 아이템들의 갯수를 UI에 표시
        counter[0].text = DataController.instance.gameData.stone_count.ToString();
        counter[1].text = DataController.instance.gameData.leaf_count.ToString();
        counter[2].text = DataController.instance.gameData.tree_count.ToString();
        counter[3].text = DataController.instance.gameData.herb_count.ToString();
        counter[4].text = DataController.instance.gameData.bone_count.ToString();
        counter[5].text = DataController.instance.gameData.skin_count.ToString();
        counter[6].text = DataController.instance.gameData.big_tree_count.ToString();
        counter[7].text = DataController.instance.gameData.meat_count.ToString();
        counter[8].text = DataController.instance.gameData.rope_count.ToString();
        counter[9].text = DataController.instance.gameData.ripe_meat_count.ToString();

    }

    // Update is called once per frame
    void Update()
    {
        Stone();
        Leaf();
        Tree();
        Herb();
        Bone();
        Skin();
        BigTree();
        Meat();
        Rope();
        Ripe_meat();
        Leaf_Dress();
        Skin_Dress();
        //----------------------------------------------------------------------------------
        Stone_Knife();
        Ax();
        PickAx();


        if (Input.GetKeyDown("0")) // 플레이어 레벨 증가
        {
            DataController.instance.gameData.player_Level += 2;
        }
    }
    public void Stone()
    {
        if (DataController.instance.gameData.stone_count >= 1)
        {
            stone.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.stone_count == 0)
        {
            stone.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }
        counter[0].text = DataController.instance.gameData.stone_count.ToString();

    }
    public void Leaf()
    {
        if (DataController.instance.gameData.leaf_count >= 1)
        {
            leaf.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.leaf_count == 0)
        {
            leaf.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }

        counter[1].text = DataController.instance.gameData.leaf_count.ToString();

    }
    public void Tree()
    {
        if (DataController.instance.gameData.tree_count >= 1)
        {
            tree.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.tree_count == 0)
        {
            tree.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }
        counter[2].text = DataController.instance.gameData.tree_count.ToString();

    }
    public void Herb()
    {
        if (DataController.instance.gameData.herb_count >= 1)
        {
            herb.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.herb_count == 0)
        {
            herb.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }

        counter[3].text = DataController.instance.gameData.herb_count.ToString();

    }
    public void Bone()
    {
        if (DataController.instance.gameData.bone_count >= 1)
        {
            bone.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.bone_count == 0)
        {
            bone.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }

        counter[4].text = DataController.instance.gameData.bone_count.ToString();

    }
    public void Skin()
    {
        if (DataController.instance.gameData.skin_count >= 1)
        {
            skin.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.skin_count == 0)
        {
            skin.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }

        counter[5].text = DataController.instance.gameData.skin_count.ToString();

    }
    public void BigTree()
    {
        if (DataController.instance.gameData.big_tree_count >= 1)
        {
            big_tree.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.big_tree_count == 0)
        {
            big_tree.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }

        counter[6].text = DataController.instance.gameData.big_tree_count.ToString();

    }
    public void Meat()
    {
        if (DataController.instance.gameData.meat_count >= 1)
        {
            meat.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.meat_count == 0)
        {
            meat.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }

        counter[7].text = DataController.instance.gameData.meat_count.ToString();

    }
    public void Rope()
    {
        if (DataController.instance.gameData.rope_count >= 1)
        {
            rope.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.rope_count == 0)
        {
            rope.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }

        counter[8].text = DataController.instance.gameData.rope_count.ToString();

    }



    public void Ripe_meat()
    {
        if (DataController.instance.gameData.ripe_meat_count >= 1)
        {
            ripe_meat.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.ripe_meat_count == 0)
        {
            ripe_meat.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }

        counter[9].text = DataController.instance.gameData.ripe_meat_count.ToString();

    }

    public void Leaf_Dress()
    {
        if (DataController.instance.gameData.leaf_dress_count >= 1)
        {
            leaf_dress.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.leaf_dress_count == 0)
        {
            leaf_dress.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }



    }
    public void Skin_Dress()
    {
        if (DataController.instance.gameData.skin_dress_count >= 1)
        {
            skin_dress.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.white;
        }
        if (DataController.instance.gameData.skin_dress_count == 0)
        {
            skin_dress.transform.GetChild(0).GetComponentInChildren<Image>().color = Color.grey;
        }



    }
    public void Stone_Knife()
    {
        if (DataController.instance.gameData.stone_knife == false)
        {
            stone_kni.SetActive(false);

        }
        if (DataController.instance.gameData.stone_knife == true)
        {
            stone_kni.SetActive(true);
        }
    }
    public void Ax()
    {
        if (DataController.instance.gameData.ax == false)
        {
            ax.SetActive(false);

        }
        if (DataController.instance.gameData.ax == true)
        {
            ax.SetActive(true);
        }
    }
    public void PickAx()
    {
        if (DataController.instance.gameData.pickax == false)
        {
            pickax.SetActive(false);
        }
        if (DataController.instance.gameData.pickax == true)
        {
            pickax.SetActive(true);
        }
    }

}
