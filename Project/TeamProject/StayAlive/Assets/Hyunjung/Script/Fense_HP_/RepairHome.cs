using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairHome : MonoBehaviour {

    public GameObject repairButton;
    public GameObject coll_Obj;
	// Use this for initialization
	void Start () {
        repairButton.SetActive(false);
	}	
	
    public void OnCollisionEnter(Collision coll)
    {
        TogetHitState(true, coll.gameObject);
    }
    public void OnCollisionExit(Collision coll)
    {
        TogetHitState(false, coll.gameObject);
    }
    public void OnTriggerEnter(Collider coll)
    {
        TogetHitState(true, coll.gameObject);
    }
    public void OnTriggerExit(Collider coll)
    {
        TogetHitState(false, coll.gameObject);
    }
    public void Repair()
    {
        //수리버튼 눌었을때
        if (coll_Obj.transform.CompareTag("home1"))
        {
            DataController.instance.gameData.house_HP += 1000;
        }
        if (coll_Obj.transform.CompareTag("home2"))
        {
            DataController.instance.gameData.house_HP += 1000;
        }
        if (coll_Obj.transform.CompareTag("home3"))
        {
            DataController.instance.gameData.house_HP += 1000;
        }
        if (coll_Obj.transform.CompareTag("fense"))
        {
            DataController.instance.gameData.fense_HP_1 += 1000;
        }
        if (coll_Obj.transform.CompareTag("fense2"))
        {
            DataController.instance.gameData.fense_HP_2 += 1000;
        }
        if (coll_Obj.transform.CompareTag("fense3"))
        {
            DataController.instance.gameData.fense_HP_3 += 1000;

        }
        if (coll_Obj.transform.CompareTag("fense4"))
        {
            DataController.instance.gameData.fense_HP_4 += 1000;
        }
        if (coll_Obj.transform.CompareTag("fense5"))
        {
            DataController.instance.gameData.fense_HP_5 += 1000;
        }
    }
    public void TogetHitState(bool active, GameObject coll)
    {
        //Collider에 닿았을때 태그판별
        if (coll.transform.CompareTag("home1"))
        {
            repairButton.SetActive(active);
            coll_Obj = GameObject.FindGameObjectWithTag("home1");
        }
        if (coll.transform.CompareTag("home2"))
        {
            repairButton.SetActive(active);
            coll_Obj = GameObject.FindGameObjectWithTag("home2");
        }
        if (coll.transform.CompareTag("home3"))
        {
            repairButton.SetActive(active);
            coll_Obj = GameObject.FindGameObjectWithTag("home3");
        }
        if (coll.transform.CompareTag("fense"))
        {
            repairButton.SetActive(active);
            coll_Obj = GameObject.FindGameObjectWithTag("fense");
        }
        if (coll.transform.CompareTag("fense2"))
        {
            repairButton.SetActive(active);
            coll_Obj = GameObject.FindGameObjectWithTag("fense2");
        }
        if (coll.transform.CompareTag("fense3"))
        {
            repairButton.SetActive(active);
            coll_Obj = GameObject.FindGameObjectWithTag("fense3");
        }
        if (coll.transform.CompareTag("fense4"))
        {
            repairButton.SetActive(active);
            coll_Obj = GameObject.FindGameObjectWithTag("fense4");
        }
        if (coll.transform.CompareTag("fense5"))
        {
            repairButton.SetActive(active);
            coll_Obj = GameObject.FindGameObjectWithTag("fense5");
        }
    }
}
