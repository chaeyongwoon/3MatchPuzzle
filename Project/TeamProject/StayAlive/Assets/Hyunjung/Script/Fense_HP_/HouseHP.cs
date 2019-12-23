using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseHP : MonoBehaviour {
    public float HouseHP_Max;
    public float houseHP;

    // Use this for initialization
    void Start()
    {
        houseHP = DataController.instance.gameData.house_HP;
        if (DataController.instance.gameData.house_HP <= 0)
        {
            DataController.instance.gameData.house_HP = HouseHP_Max;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    { 
        houseHP = DataController.instance.gameData.house_HP;
        DataController.instance.gameData.house_MaxHP = HouseHP_Max;
        // 현재 집의 체력이 0이하면 집이 없는 house_0단계로 설정
        if (houseHP <= 0)
        {
            DataController.instance.gameData.house_0 = true;
        }        
        if (DataController.instance.gameData.house_HP >= HouseHP_Max)
        {
            DataController.instance.gameData.house_HP = HouseHP_Max;
        }
    }
  
    private void OnCollisionStay(Collision coll)
    { // 디펜스 몬스터에 닿으면 집 체력 감소
        if(coll.transform.tag == "defense_enemy")
        {
            coll.gameObject.GetComponent<Defense_Enemy>().defense_enemyState = Defense_Enemy.State.FenseAttack;
            if (coll.gameObject.GetComponent<Defense_Enemy>().defense_enemyState == Defense_Enemy.State.FenseAttack)
            {
                DataController.instance.gameData.house_HP -= 10f;
            }
            if (DataController.instance.gameData.house_HP <= 0)
            {
                coll.gameObject.GetComponent<Defense_Enemy>().defense_enemyState = Defense_Enemy.State.Walk;
            }
        }
    }
}
