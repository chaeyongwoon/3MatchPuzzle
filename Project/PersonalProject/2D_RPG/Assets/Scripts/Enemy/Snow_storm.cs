using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snow_storm : MonoBehaviour
{
    public Transform player;
    public float Speed = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * Speed);

        transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);


    }
}
