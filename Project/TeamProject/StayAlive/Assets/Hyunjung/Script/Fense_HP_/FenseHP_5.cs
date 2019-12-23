using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenseHP_5 : MonoBehaviour {
    public float fenseHP_Max;
    public float fenseHP;
    //int i;
    // Use this for initialization
    void Start()
    {
        fenseHP = DataController.instance.gameData.fense_HP_5;
        if (fenseHP <= 0)
        {
            DataController.instance.gameData.fense_HP_5 = fenseHP_Max;
        }
        if (fenseHP > 0)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {// 펜스의 체력이 0이하로 떨어지면 오브젝트의 모습이 보이지 않도록 렌더링 비활성화
        if (fenseHP <= 0)
        {
            this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = false;
            this.gameObject.GetComponent<BoxCollider>().isTrigger = true;
        }
        else
        {// 펜스의 체력이 0보다 크다면 렌더링 활성화
            this.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.transform.GetChild(1).GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.transform.GetChild(2).GetComponent<MeshRenderer>().enabled = true;
            this.gameObject.GetComponent<BoxCollider>().isTrigger = false;
        }
        fenseHP = DataController.instance.gameData.fense_HP_5;
        if(DataController.instance.gameData.fense_HP_5 >= fenseHP_Max)
        {
            DataController.instance.gameData.fense_HP_5 = fenseHP_Max;
        }
    }
    private void OnCollisionStay(Collision coll)
    {// 디펜스 몬스터에게 닿을경우 펜스의 체력감소
        if (coll.transform.tag == "defense_enemy")
        {
            if (DataController.instance.gameData.fense_HP_5 > 0)
            {
                coll.gameObject.GetComponent<Defense_Enemy>().defense_enemyState = Defense_Enemy.State.FenseAttack;
            }         
            if (coll.gameObject.GetComponent<Defense_Enemy>().defense_enemyState == Defense_Enemy.State.FenseAttack)
            {
                DataController.instance.gameData.fense_HP_5 -= 1f;
           
            }
            if (DataController.instance.gameData.fense_HP_5 <= 0)
            {
                coll.gameObject.GetComponent<Defense_Enemy>().defense_enemyState = Defense_Enemy.State.Walk;
            }
        }
    }
}
