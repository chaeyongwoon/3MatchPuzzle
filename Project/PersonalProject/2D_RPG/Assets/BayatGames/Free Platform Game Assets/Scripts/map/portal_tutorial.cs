using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal_tutorial : MonoBehaviour {


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (CompareTag("To_Lobby"))
            {
                SceneManager.LoadScene("2.town");
            }
        }
    }

}
