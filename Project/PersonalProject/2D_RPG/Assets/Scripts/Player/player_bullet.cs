using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_bullet : MonoBehaviour {

    public float Speed = 5f;

    public enum Color
    {
        red,
        yellow,
        green,
        blue
    }
    public Color state;

	
	// Update is called once per frame
	void Update () {
        transform.Translate(Vector2.right*Time.deltaTime*Speed);
	}

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Enemy")){
            Destroy(this.gameObject);
        }
        if (collision.transform.CompareTag("Boss"))
        {
            Destroy(this.gameObject);
        }


        if (collision.transform.CompareTag("Chest"))
        {
            Destroy(this.gameObject);
        }
    }

}
