using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_stone : MonoBehaviour
{
    public GameObject[] tree_stone;


    void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            tree_stone[i] = transform.GetChild(i).gameObject;
        }

        StartCoroutine("Revive");
    }




    public IEnumerator Revive()
    {
        while (true)
        {
            yield return new WaitForSeconds(30f);


            for (int i = 0; i < transform.childCount; i++)
            {
                tree_stone[i].SetActive(true);
             
            }
        }
    }




}
