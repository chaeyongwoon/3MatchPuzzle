using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Make_Item : MonoBehaviour {

    public Text Leaf_Dress_stuff;
    public Text[] Skin_Dress_stuff;
    public Text[] Stone_Knife_stuff;
    public Text[] Ax_stuff;
    public Text[] PickAx_stuff;
    public GameObject Notree;

    //제작하는데 필요한 재료 개수
    public int LeafDress_leaf;
    public int SkinDress_leaf;
    public int SkinDress_skin;
    public int Knife_stone;
    public int Knife_tree;
    public int Ax_stone;
    public int Ax_tree;
    public int PickAx_stone;
    public int PickAx_tree;
    //아이템 제작팝업창에 사용하는 스크립트

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
         // 현재 아이템의 개수와 제작에 필요한 아이템 개수를 표시
        Leaf_Dress_stuff.text = string.Format("{0} / {1}", DataController.instance.gameData.leaf_count, LeafDress_leaf); 
        Skin_Dress_stuff[0].text = string.Format("{0} / {1}", DataController.instance.gameData.leaf_count, SkinDress_leaf);
        Skin_Dress_stuff[1].text = string.Format("{0} / {1}", DataController.instance.gameData.skin_count, SkinDress_skin);
        Stone_Knife_stuff[0].text = string.Format("{0} / {1}", DataController.instance.gameData.tree_count, Knife_tree);
        Stone_Knife_stuff[1].text = string.Format("{0} / {1}", DataController.instance.gameData.stone_count, Knife_stone);
        Ax_stuff[0].text = string.Format("{0} / {1}", DataController.instance.gameData.tree_count, Ax_tree);
        Ax_stuff[1].text = string.Format("{0} / {1}", DataController.instance.gameData.stone_count, Ax_stone);
        PickAx_stuff[0].text = string.Format("{0} / {1}", DataController.instance.gameData.tree_count, PickAx_tree);
        PickAx_stuff[1].text = string.Format("{0} / {1}", DataController.instance.gameData.stone_count, PickAx_stone);
        if (DataController.instance.gameData.leaf_count <= 0)
        {
            DataController.instance.gameData.leaf_count = 0;
        }
    }
    public void MakeLeafDress() // 나뭇잎 옷 아이템 제작버튼
    {
        //다 버튼에서 사용
        if(DataController.instance.gameData.leaf_count < LeafDress_leaf)
        {
            Notree.SetActive(true);
            
        }
        else if (DataController.instance.gameData.leaf_count >= LeafDress_leaf) // 재료가 있다면 재료를 소모하고 아이템 제작
        {
            DataController.instance.gameData.leaf_count -= 30;
            DataController.instance.gameData.leaf_dress_count += 1;
        }
    }
    public void MakeSkinDress()
    { // 가죽 옷 아이템 제작 버튼
        if ((DataController.instance.gameData.leaf_count < SkinDress_leaf) || (DataController.instance.gameData.skin_count < SkinDress_skin))
        {
            Notree.SetActive(true);
        }
        else if ((DataController.instance.gameData.leaf_count >= SkinDress_leaf) && (DataController.instance.gameData.skin_count >= SkinDress_skin)) // 재료가 있다면 재료를 소모하고 아이템 제작
        {
            DataController.instance.gameData.leaf_count -= 30;
            DataController.instance.gameData.skin_count -= 15;
            DataController.instance.gameData.skin_dress_count += 1;
        }
    }
    public void MakeStoneKnife()
    { // 칼 아이템 제작 버튼
        if ((DataController.instance.gameData.tree_count < Knife_tree) || (DataController.instance.gameData.stone_count < Knife_stone))
        {
            Notree.SetActive(true);

        }
        else if ((DataController.instance.gameData.tree_count >= Knife_tree) && (DataController.instance.gameData.stone_count >= Knife_stone)) // 재료를 소모하고 아이템 제작
        {
            DataController.instance.gameData.tree_count -= 30;
            DataController.instance.gameData.stone_count -= 15;
            DataController.instance.gameData.stone_knife = true;
        }
    }
    public void MackAx()
    { // 도끼 아이템 제작 버튼
        if ((DataController.instance.gameData.tree_count < Ax_tree) || (DataController.instance.gameData.stone_count < Ax_stone))
        {
            Notree.SetActive(true);
        }
        else if ((DataController.instance.gameData.tree_count >= Ax_tree) && (DataController.instance.gameData.stone_count >= Ax_stone)) // 재료를 소모하고 아이템 제작
        {
            DataController.instance.gameData.tree_count -= 30;
            DataController.instance.gameData.stone_count -= 40;
            DataController.instance.gameData.ax = true;
        }
        
    }
    public void MakePickAx()
    {// 곡괭이 아이템 제작 버튼
        if ((DataController.instance.gameData.tree_count < PickAx_tree) || (DataController.instance.gameData.stone_count < PickAx_stone))
        {
            Notree.SetActive(true);
        }
        else if ((DataController.instance.gameData.tree_count >= PickAx_tree) && (DataController.instance.gameData.stone_count >= PickAx_stone)) // 재료를 소모하고 아이템 제작
        {
            DataController.instance.gameData.tree_count -= 30;
            DataController.instance.gameData.stone_count -= 40;
            DataController.instance.gameData.pickax = true;
        }
    }

}
