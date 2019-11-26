using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class portal_tutorial : MonoBehaviour {

    public Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            if (CompareTag("To_Lobby"))
            {
                player.position = new Vector2(0f, 3f);
                Camera.main.transform.position = new Vector3(0, 3, -10);

                SceneManager.LoadScene("2.town");
            }
        }
    }

}
