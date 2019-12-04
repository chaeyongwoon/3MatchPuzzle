using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thorn_block : MonoBehaviour
{
    public Player_attack_health player;
    public Boss boss;
    public Game_Manager gm;
    public float damage;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_attack_health>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
        damage = boss.damage;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.Take_Damage(damage, transform.position);
        }
    }

}
