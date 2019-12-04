using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_bullet1 : MonoBehaviour
{
    public Player_attack_health player;
    public Boss boss;
    public Game_Manager gm;

    public float damage;
    public float Speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        // 플레이어,보스몬스터,게임매니저 오브젝트 참조
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_attack_health>();
        boss = GameObject.FindGameObjectWithTag("Boss").GetComponent<Boss>();
        gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();

        damage = boss.damage;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * Time.deltaTime * Speed);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            player.Take_Damage(damage, transform.position);
            Destroy(this.gameObject);
        }
        if (collision.transform.CompareTag("Enemy") || collision.transform.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }

    }

    private void OnBecameInvisible()
    {
        Destroy(this.gameObject); // 카메라 밖으로 나갈경우 오브젝트 삭제
    }
}
