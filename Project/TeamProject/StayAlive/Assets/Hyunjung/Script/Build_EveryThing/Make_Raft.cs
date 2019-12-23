using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Make_Raft : MonoBehaviour {

    public GameObject makeRaft_button; // 뗏목 제작 버튼
    public Text raft_text;

	// Use this for initialization
	
	
	
    public void Make_raft() // 큰나무가 100개 이상이면 뗏목 생성
    {
        if (DataController.instance.gameData.big_tree_count >= 100)
        {
            DataController.instance.gameData.big_tree_count -= 100;
            raft_text.text = "뗏목이 완성 되었습니다";
        }
        
    }
}
