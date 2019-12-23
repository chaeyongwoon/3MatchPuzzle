using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Egg : MonoBehaviour
{
    Animator anim;
 
    public GameObject getpet;
    public Text pet_text;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        getpet.SetActive(false);
    }

    // Update is called once per frame
   
    void Crack()
    { // 알이 깨지면 3가지중 한가지의 랜덤한 펫을 획득할 수 있다 , 펫은 각자의 고유능력이 있다
        anim.SetBool("EggCracking", true);
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            DataController.instance.gameData.nomalpet1 = true;
            pet_text.text = string.Format("1번펫 획득");
            getpet.SetActive(true);
        }
        else if (a == 1)
        {
            DataController.instance.gameData.nomalpet2 = true;
            pet_text.text = string.Format("2번펫 획득");
            getpet.SetActive(true);
        }
        else if (a == 2)
        {
            DataController.instance.gameData.legendpet = true;
            pet_text.text = string.Format("레전드 펫 획득");
            getpet.SetActive(true);
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.transform.CompareTag("Player")) // 플레이어에 닿으면 알이 깨지는 함수 호출
        {
            Crack();
        }
    }

    public void close()
    {
        getpet.SetActive(false);
    }

}
