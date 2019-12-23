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
        // 플레이어,게임매니저 및 기타 컴포넌트 참조
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_attack_health>();
        gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();

        if (!rend)
        {
            rend = GetComponent<SpriteRenderer>();
        }
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // 최대체력,공격력 초기화
        max_health = 100 + 20000 * gm.stage_level;
        current_health = max_health;
        damage = 10 + 100 * gm.stage_level;

        hp_slider.value = current_health / max_health;
        health_text.text = string.Format("{0}/{1}", Mathf.Floor(current_health), Mathf.Floor(max_health));
        damage_ui_text.text = string.Format("보스 공격력 : {0}  방어력 : {1}%", damage, gm.stage_level);

        StartCoroutine(Color_change()); // 일정시간마다 보스몬스터의 색상 상태 변경

        // 4가지의 공격패턴을 각각 설정된 시간마다 사용
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


    void Take_damage(float damage) // 체력 손실 함수
    {
        float real_damage = damage * Random.Range(0.8f, 1.4f); // 80%~140%의 랜덤한 데미지

        current_health -= real_damage * (100 - gm.stage_level) * 0.01f; // 플레이어 총알의 공격력을 방어력의 비율에따라 감소시킨 후 체력감소
        hp_slider.value = current_health / max_health;


        /// 데미지 텍스트 띄우기///
        Text obj = Instantiate(damage_text, transform.position, transform.rotation);
        obj.text = "" + Mathf.Floor(real_damage);
        obj.transform.parent = transform.GetChild(0);
        obj.rectTransform.localScale = new Vector2(1, 1);

        DataController.instance.gameData.Money += Mathf.Floor(real_damage); // 데미지에 따른 금화
        health_text.text = string.Format("{0}/{1}", Mathf.Floor(current_health), Mathf.Floor(max_health));
        if (current_health <= 0)
        {
            current_health = 0;
            Dead();
        }

    }

    public void Dead()
    {        // 사망시 추락하도록 설정, 일정시간 후 스테이지 전환
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation | RigidbodyConstraints2D.FreezePositionX;
        StartCoroutine(Stage_clear());
    }

    public IEnumerator Stage_clear()
    {
        yield return new WaitForSeconds(3f);
        player.Clear();
    }


    public IEnumerator Color_change() // 보스몬스터 색상 상태 변경함수
    {
        while (true)
        {
            yield return new WaitForSeconds(20f); // 20초마다 4가지 색상중 한가지로 랜덤하게 변경
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

    public IEnumerator Attack1() // 1번 공격패턴 함수. 총알 부채꼴 발사
    {
        while (true)
        {
            yield return new WaitForSeconds(attack1_term);

            Quaternion angle;
            for (int i = 1; i < bullet1_num; i++)
            {
                angle = Quaternion.Euler(new Vector3(0, 0, -90 + 180 / bullet1_num * i));   // 부채꼴 모양으로 각도 계산
                Instantiate(bullet, fire_pos.position, angle);
            }
        }
    }
    public IEnumerator Attack2()// 2번 공격패턴 함수. 가시블록 생성
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
    public IEnumerator Attack3()// 3번 공격패턴 함수. 총알 나선형 발사
    {
        while (true)
        {
            yield return new WaitForSeconds(attack3_term);

            int i = 1;
            Quaternion angle;
            while (i < bullet2_num)
            {
                angle = Quaternion.Euler(new Vector3(0, 0, -90 + 180 / bullet2_num * i));  // 부채꼴 모양으로 각도 계산
                Instantiate(bullet, fire_pos.position, angle);
                i++;
                yield return new WaitForSeconds(0.1f); // 0.1초의 딜레이를 주어 나선형으로 발사
            }
        }
    }
    public IEnumerator Attack4()// 4번 공격패턴 함수. 얼음블록 갱신
    {
        while (true)
        {
            yield return new WaitForSeconds(attack4_term);

            for (int i = 0; i < 8; i++)
            {
                Ice_block[i].SetActive(false);
                Ice_block[i].SetActive(true);
                Ice_block[i].GetComponent<Ice_block>().Revival(); // 부셔진 얼음블록 재생성
            }

        }
    }

}
