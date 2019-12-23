using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rope : MonoBehaviour
{

    public GameObject rope_child;


    // Use this for initialization
    void Start()
    {
        StartCoroutine(Create_rope());
    }


    public IEnumerator Create_rope()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            rope_child.SetActive(true);
        }
    }

}
