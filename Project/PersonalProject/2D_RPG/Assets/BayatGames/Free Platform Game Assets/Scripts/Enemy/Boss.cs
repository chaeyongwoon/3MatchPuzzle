using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public Player_attack_health player;
    public Game_Manager gm;


    public Transform fire_pos;
    public GameObject bullet;
    public GameObject Thorn_block_group;
    public GameObject[] Ice_block;

    public SpriteRenderer rend;
    public Rigidbody2D rb;

    public float max_health;
    public float current_health;
    public float damage;
    public int bullet1_num = 5;
    public int bullet2_num = 20;

    public Sprite[] spr;
    public Slider hp_slider;

    public Text damage_text;
    public Text health_text;
    public Text damage_ui_text;

    public float attack1_term, attack2_term, attack3_term, attack4_term;

    public enum Color
    {
        red,
        yellow,
        green,
        blue
    }
    public Color state;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_attack_health>();
        gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
        rend = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        max_health = 100 + 20000 * gm.stage_level;
        current_health = max_health;
        damage = 10 + 100 * gm.stage_level;

        hp_slider.value = current_health / max_health;
        health_text.text = string.Format("{0}/{1}", Mathf.Floor(current_health), Mathf.Floor(max_health));
        damage_ui_text.text = string.Format("보스 공격력 : {0}  방어력 : {1}%", damage,gm.stage_level);

        StartCoroutine(Color_change());

        StartCoroutine(Attack1());
        StartCoroutine(Attack2());
        StartCoroutine(Attack3());
        StartCoroutine(Attack4());    

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
    }


    void Take_damage(float damage)
    {
        float real_damage = damage * Random.Range(0.8f, 1.4f);

        current_health -= real_damage * (100-gm.stage_level)*0.01f;
        hp_slider.value = current_health / max_health;

        Text obj = Instantiate(damage_text, transform.position, transform.rotation);
        obj.text = "" + Mathf.Floor(real_damage);
        obj.transform.parent = transform.GetChild(0);
        obj.rectTransform.localScale = new Vector2(1, 1);

        DataController.instance.gameData.Money += Mathf.Floor(real_damage);
        health_text.text = string.Format("{0}/{1}",Mathf.Floor( current_health), Mathf.Floor(max_health));
        if (current_health <= 0)
        {
            current_health = 0;
            Dead();
        }

    }

    public void Dead()
    {
        // 죽는 애니메이션 및 보상 , 장비? 펫?
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        StartCoroutine(Stage_clear());



    }

    public IEnumerator Stage_clear()
    {
        yield return new WaitForSeconds(3f);
        player.Clear();
    }


    public IEnumerator Color_change()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);
            int a = Random.Range(0, 4);
            switch (a)
            {
                case 0:
                    rend.sprite = spr[0];
                    state = Color.red;
                    break;

                case 1:
                    rend.sprite = spr[1];
                    state = Color.yellow;
                    break;
                case 2:
                    rend.sprite = spr[2];
                    state = Color.green;
                    break;
                case 3:
                    rend.sprite = spr[3];
                    state = Color.blue;
                    break;
            }

        }
    }

    public IEnumerator Attack1()
    {
        while (true)
        {
            yield return new WaitForSeconds(attack1_term);

            Quaternion angle;
            for (int i = 1; i < bullet1_num; i++)
            {
                angle = Quaternion.Euler(new Vector3(0, 0, -90 + 180 / bullet1_num * i));
               Instantiate(bullet, fire_pos.position, angle); 
            }
        }
    }
    public IEnumerator Attack2()
    {
        while (true)
        {
            yield return new WaitForSeconds(attack2_term);
            Thorn_block_group.GetComponent<Rigidbody2D>().constraints
                = RigidbodyConstraints2D.FreezePositionY;
            Thorn_block_group.transform.position = new Vector3(-10, 30, 0);
            Thorn_block_group.GetComponent<Rigidbody2D>().constraints
                = RigidbodyConstraints2D.None;

        }
    }
    public IEnumerator Attack3()
    {
        while (true)
        {
            yield return new WaitForSeconds(attack3_term);
            
            int i = 1;
            Quaternion angle;
            while (i < bullet2_num)
            {
                angle = Quaternion.Euler(new Vector3(0, 0, -90 + 180 / bullet2_num * i));
                Instantiate(bullet, fire_pos.position, angle);
                i++;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }
    public IEnumerator Attack4()
    {
        while (true)
        {
            yield return new WaitForSeconds(attack4_term);

            for(int i = 0; i < 8; i++)
            {
                Ice_block[i].SetActive(false);
                Ice_block[i].SetActive(true);
                Ice_block[i].GetComponent<Ice_block>().Revival();
            }

        }
    }
    
}
