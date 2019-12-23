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
        orignal_position = transform.position; // 초기위치 저장


        // 플레이어, 게임매니저 오브젝트 및 기타 컴포넌트 참조
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_attack_health>();
        gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
        if (!cap_col)
        {
            cap_col = GetComponent<CapsuleCollider2D>();
        }
        if (!rend)
        {
            rend = GetComponent<SpriteRenderer>();
        }
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }


        // 체력, 공격력 초기화
        max_health = 100 + 100 * gm.stage_level;
        current_health = max_health;
        damage = 10 + 10 * gm.stage_level;

        hp_slider.value = current_health / max_health;

    }

    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(transform.position, Target_player.position) < dis) // 플레이어와의 거리가 일정거리 이하일 경우
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
                        //rb.velocity = new Vector2(rb.velocity.x, JumpPow); // 몬스터가 점프할시 난이도가 높아져 무기한 보류
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
            if (state.ToString() == collision.gameObject.GetComponent<player_bullet>().state.ToString()) // 충돌한 총알의 색과 몬스터의 색상이 일치하는지 판단
            {
                Take_damage(DataController.instance.gameData.Damage);
            }
        }

        if (collision.transform.CompareTag("Player"))
        {
            player.Take_Damage(damage, transform.position);
        }

    }


    void Take_damage(float damage) // 체력 손실 함수
    {
        float real_damage = damage * Random.Range(0.8f, 1.4f);

        current_health -= real_damage;
        hp_slider.value = current_health / max_health;


        ////// 데미지 텍스트 띄우기 ////// 
        Text obj = Instantiate(damage_text, transform.position, transform.rotation);
        obj.text = "" + Mathf.Floor(real_damage);
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
        // 몬스터 사망시 오브젝트를 삭제하지 않고 컴포넌트들을 비활성화 시켜둔 후 일정시간이 지나면 다시 활성화
        isdead = true;
        cap_col.isTrigger = true;
        rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        rend.enabled = false;
        hp_slider.gameObject.SetActive(false);

        StartCoroutine(revival());// 일정시간 후 부활

    }


    public IEnumerator revival() // 오브젝트를 새로 생성하지 않고 풀링하여 재사용
    {
        yield return new WaitForSeconds(revival_term);

        transform.position = orignal_position;
        current_health = max_health;
        hp_slider.value = current_health / max_health;    // 체력,포지션,콜라이더를 초기상태로 재설정
        cap_col.isTrigger = false;                                                              
        isdead = false;
        rb.constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
        rend.enabled = true;
        hp_slider.gameObject.SetActive(true);           // 새로 생성하지 않고 오브젝트를 풀링하여 재사용 

    }

    public IEnumerator ReJump()
    {
        yield return new WaitForSeconds(2f);
        isjump = false;
    }
}
