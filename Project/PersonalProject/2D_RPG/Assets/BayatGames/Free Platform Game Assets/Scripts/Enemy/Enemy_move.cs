using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Enemy_move : MonoBehaviour
{

    public Player_attack_health player;
    public Game_Manager gm;

    public Transform Target_player;
    public Vector3 orignal_position;

    public float speed = 2f;

    public float max_health;
    public float current_health;
    public float damage;

    public Text damage_text;
    public int damage_num = 1;
    public bool isdead = false;
    public bool isjump = false;
   

    public Slider hp_slider;

    public CapsuleCollider2D cap_col;
    public Rigidbody2D rb;

    public float revival_term = 5f;
    public float JumpPow = 30f;
    public float dis = 10f;

    public enum Color
    {
        red,
        yellow,
        green,
        blue
    }

    public Color state;

    public SpriteRenderer rend;

    // Use this for initialization
    void Start()
    {
        if (!Target_player)
        {
            Target_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        orignal_position = transform.position;

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_attack_health>();
        gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
        cap_col = GetComponent<CapsuleCollider2D>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        max_health = 100 + 100* gm.stage_level;
        current_health = max_health;
        damage = 10 + 10* gm.stage_level;

        hp_slider.value = current_health / max_health;

    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, Target_player.position) < dis)
        {
            if (isdead == false)
            {
                transform.position = Vector2.MoveTowards(
                    transform.position,
                    new Vector2(Target_player.position.x, transform.position.y),
                    Time.deltaTime * speed);

                if (Target_player.transform.position.y > transform.position.y + 1)
                {
                    if (isjump == false)
                    {
                        //rb.velocity = new Vector2(rb.velocity.x, JumpPow); // 일단 보류
                        isjump = true;
                        StartCoroutine(ReJump());
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("bullet"))
        {
            if (state.ToString() == collision.gameObject.GetComponent<player_bullet>().state.ToString())
            {

                Take_damage(DataController.instance.gameData.Damage);
            }
        }

        if (collision.transform.CompareTag("Player"))
        {
            player.Take_Damage(damage, transform.position);
        }

    }


    void Take_damage(float damage)
    {
        float real_damage = damage * Random.Range(0.8f, 1.4f);

        current_health -= real_damage /* * (100 - defend) * 0.01f*/;
        hp_slider.value = current_health / max_health;

        Text obj = Instantiate(damage_text, transform.position, transform.rotation);
        obj.text = "" + Mathf.Floor(real_damage); /* * (100 - defend) * 0.01f*/
        obj.transform.parent = transform.GetChild(0);
        obj.rectTransform.localScale = new Vector2(1, 1);

        DataController.instance.gameData.Money += Mathf.Floor(real_damage);

        if (current_health <= 0)
        {
            Dead();
        }



    }
    void Dead()
    {
        isdead = true;
        cap_col.isTrigger = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rend.enabled = false;
        hp_slider.gameObject.SetActive(false);
        //gameObject.SetActive(false);
        Invoke("revival", revival_term);

    }


    void revival()
    {
        transform.position = orignal_position;
        current_health = max_health;
        hp_slider.value = current_health / max_health;
        cap_col.isTrigger = false;
        isdead = false;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        rend.enabled = true;
        hp_slider.gameObject.SetActive(true);
        //gameObject.SetActive(true);
    }

    public IEnumerator ReJump()
    {
        yield return new WaitForSeconds(2f);
        isjump = false;
    }
}
