using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ice_ball : MonoBehaviour
{

    public Vector3 origin_pos;
    public Player_attack_health player;
    public Game_Manager gm;

    public float damage = 100f;
    public float max_health;
    public float current_health;

    public Text damage_text;
    public Slider hp_slider;
    public Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_attack_health>();
        gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
        rb = GetComponent<Rigidbody2D>();
        
        origin_pos = transform.position;
        max_health = 100 + 100* gm.stage_level;
        current_health = max_health;
        damage = 10 + 10*gm.stage_level;
    }

    
   
    void Take_damage(float damage)
    {
        float real_damage = damage * Random.Range(0.8f, 1.4f);

        current_health -= real_damage;
        hp_slider.value = current_health / max_health;

        Text obj = Instantiate(damage_text, transform.position, transform.rotation);
        obj.text = "" + Mathf.Floor(real_damage);
        obj.transform.parent = transform.GetChild(0);
        obj.rectTransform.localScale = new Vector2(1, 1);

        DataController.instance.gameData.Money += Mathf.Floor(real_damage);

        if (current_health <= 0)
        {
            Revival();
        }
    }

    public void Revival()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.position = origin_pos;
        current_health = max_health;
        rb.constraints = RigidbodyConstraints2D.None;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            player.Take_Damage(100,transform.position);
        }

        if (collision.transform.CompareTag("bullet"))
        {
            Take_damage(damage);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball_stop"))
        {
            Revival();
        }
    }

   

}
