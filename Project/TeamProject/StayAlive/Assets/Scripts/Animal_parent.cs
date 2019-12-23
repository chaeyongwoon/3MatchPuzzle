using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animal_parent : MonoBehaviour {

    public GameObject[] animal;

	// Use this for initialization
	void Start () {
		
        /*
         * 시작시 하위 자식오브젝트들을 배열로 저장한다.
          */
        for(int i=0; i < transform.childCount; i++)
        {
            animal[i] = transform.GetChild(i).gameObject;
        }

        StartCoroutine("Revive");

	}
	
    public IEnumerator Revive()
    {
        /*
         * 일정 시간마다 죽은 동물들을 리스폰 시켜 동물이 모두 없어지지 않도록 한다.
          */
        while (true)
        {
            yield return new WaitForSeconds(30f);

            for (int i = 0; i < transform.childCount; i++)
            {
                animal[i].SetActive(true);
                
               
            }
        }
    }
}
