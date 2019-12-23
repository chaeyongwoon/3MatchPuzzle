using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class stage_skip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Skip());
    }


    public IEnumerator Skip()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("menu");
    }
}
