using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Game_Manager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
    }

    public void Shop_open()
    {
        gm.open_shop(); // 무한던전 스테이지에서 혼자 쓰이는 스크립트의 상점 오픈 함수
    }
}
