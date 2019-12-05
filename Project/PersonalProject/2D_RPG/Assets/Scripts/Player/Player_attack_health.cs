using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Player_attack_health : MonoBehaviour
{
    public static Player_attack_health instance;



    public GameObject[] bullet;
    public Vector2 fire_position;

    public bool isdamaged = false;

    public float x_force, y_force;

    public GameObject[] color_img;
    public GameObject rewards_panel;
    public Text rewards_text;


    public Rigidbody2D rb;
    public Animator ani;
    public AudioSource audiosource;
    public AudioClip shoot_sound, dead_sound;

    // Use this for initialization
    void Start()
    {
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (!ani)
        {
            ani = GetComponent<Animator>();
        }
        if (!audiosource)
        {
            audiosource = GetComponent<AudioSource>();
        }

        StartCoroutine("Take_Heal"); // 일정시간마다 체력을 회복하는 함수 코루틴으로 실행
    }

    // Update is called once per frame
    void Update()
    {
        DataController.instance.gameData.Reload_term += Time.deltaTime;
        fire_position = transform.position + transform.right * 2;
        if (!ani.GetBool("Dead"))
        {
            if (Input.GetKey(KeyCode.Z)) // PC용 키 설정
            {
                Shoot();
            }
        }

        if (DataController.instance.gameData.Is_shoot == true)
        {
            Shoot();
        }


        if (Input.GetKeyDown(KeyCode.Alpha1)) // PC용 키 설정
        {
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);
            SceneManager.LoadScene("2.town");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))// PC용 키 설정
        {
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);
            SceneManager.LoadScene("3.stage1");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))// PC용 키 설정
        {
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);
            SceneManager.LoadScene("4.stage2");

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))// PC용 키 설정
        {
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);
            SceneManager.LoadScene("5.stage_infinite");

        }


        if (transform.position.y <= -100f) // 만약을 대비해 캐릭터가 계속 추락할 경우 실행
        {
            StartCoroutine(Dead());
        }

    }


    public void OnAttackDown() // 모바일 용 UI 트리거 함수
    {
        DataController.instance.gameData.Is_shoot = true;
    }

    public void OnAttackUp()// 모바일 용 UI 트리거 함수
    {
        DataController.instance.gameData.Is_shoot = false;
    }

    public void Shoot()
    {

        if (DataController.instance.gameData.Reload_term > DataController.instance.gameData.Reload) // 일정시간 지연을 두어 공격할 수 있도록 설정
        {
            audiosource.clip = shoot_sound;
            audiosource.Play();

            Instantiate(bullet[DataController.instance.gameData.Color_num], fire_position, transform.rotation);
            DataController.instance.gameData.Reload_term = 0f;
        }
    }


    public void Take_Damage(float damage, Vector3 col_pos)
    {
        if (isdamaged == false)
        {
            isdamaged = true;
            DataController.instance.gameData.Current_health -= damage * (100 - DataController.instance.gameData.Defend) * 0.01f * Random.Range(0.8f, 1.4f); // 80%~140%의 랜덤한 데미지를 방어력에 따라 재조정되어 피해계산

            if (DataController.instance.gameData.Current_health <= 0)
            {
                DataController.instance.gameData.Current_health = 0;
            }

            Vector3 direction = (transform.position - col_pos).normalized; // 적 과 충돌시 충돌반대 방향으로 벡터계산
            rb.AddForce(new Vector2(direction.x * x_force, y_force));       // 살짝 튕겨나가는 효과

            if (DataController.instance.gameData.Current_health <= 0f)
            {
                StartCoroutine(Dead());
            }
            StartCoroutine(Rehit());
        }
    }

    public IEnumerator Rehit() // 짧은시간에 플레이어의 체력이 여러번 감소되지 않도록 설정
    {
        yield return new WaitForSeconds(1f);
        isdamaged = false;
    }


    public IEnumerator Dead()
    {
        //// 사망시 3초 후 50의 체력으로 마을에서 부활///
        if (!ani.GetBool("Dead"))
            ani.SetBool("Dead", true);
        {
            audiosource.clip = dead_sound;
            audiosource.Play();

            yield return new WaitForSeconds(3f);
            ani.SetBool("Dead", false);
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);

            DataController.instance.gameData.Current_health = 50f;
            System.GC.Collect(); // 던전 씬에서 마을로 이동시 가비지컬렉터 사용
            SceneManager.LoadScene("2.town");
        }
    }

    public void Color_Change_Red() // 색상 변경함수. 빨강
    {
        DataController.instance.gameData.Color_num = 0;
        Game_Manager.instance.Color_change();
        color_img[0].SetActive(true);
        color_img[1].SetActive(false);
        color_img[2].SetActive(false);
        color_img[3].SetActive(false);
    }
    public void Color_Change_Yellow()// 색상 변경함수. 노랑
    {
        DataController.instance.gameData.Color_num = 1;
        Game_Manager.instance.Color_change();
        color_img[0].SetActive(false);
        color_img[1].SetActive(true);
        color_img[2].SetActive(false);
        color_img[3].SetActive(false);
    }
    public void Color_Change_Green()// 색상 변경함수. 초록
    {
        DataController.instance.gameData.Color_num = 2;
        Game_Manager.instance.Color_change();
        color_img[0].SetActive(false);
        color_img[1].SetActive(false);
        color_img[2].SetActive(true);
        color_img[3].SetActive(false);
    }
    public void Color_Change_Blue()// 색상 변경함수. 파랑
    {
        DataController.instance.gameData.Color_num = 3;
        Game_Manager.instance.Color_change();
        color_img[0].SetActive(false);
        color_img[1].SetActive(false);
        color_img[2].SetActive(false);
        color_img[3].SetActive(true);
    }

    IEnumerator Take_Heal() // 일정시간마다 체력회복
    {
        while (true)
        {
            if (DataController.instance.gameData.Current_health > 0 && DataController.instance.gameData.Current_health < DataController.instance.gameData.Max_health)
            {
                DataController.instance.gameData.Current_health += DataController.instance.gameData.Healing;
                if (DataController.instance.gameData.Current_health > DataController.instance.gameData.Max_health)
                    DataController.instance.gameData.Current_health = DataController.instance.gameData.Max_health;
            }

            yield return new WaitForSeconds(1f);
        }
    }


    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("To_town"))
        {
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);


            ///스테이지 클리어 시 보상///
            rewards_panel.SetActive(true);
            float rewards = 0;
            if (SceneManager.GetActiveScene().name == "1.tutorial")
            {
                rewards = 6000;
            }
            else if (SceneManager.GetActiveScene().name == "3.stage1")
            {
                rewards = 1000;
            }
            else if (SceneManager.GetActiveScene().name == "4.stage2")
            {
                rewards = 10000;
            }

            DataController.instance.gameData.Money += rewards;
            rewards_text.text = string.Format("{0}", rewards);

            System.GC.Collect(); // 던전 씬에서 마을로 이동시 가비지컬렉터 사용
            SceneManager.LoadScene("2.town");
        }

        if (coll.transform.CompareTag("Hole")) // 구멍에 빠질경우 사망
        {
            StartCoroutine(Dead());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Snow_storm")) // 눈사태에 닿을경우 사망
        {
            StartCoroutine(Dead());
        }
    }

    public void Clear() // 무한던전 클리어시 호출하는 함수
    {
        transform.position = new Vector2(0f, 3f);
        Camera.main.transform.position = new Vector3(0, 3, -10);

        rewards_panel.SetActive(true);
        float rewards = 10000 * Game_Manager.instance.stage_level;
        DataController.instance.gameData.Money += rewards;
        rewards_text.text = string.Format("{0}", rewards);
        System.GC.Collect(); // 던전 씬에서 마을로 이동시 가비지컬렉터 사용
        SceneManager.LoadScene("2.town");
    }
    public void close_rewards_panel()
    {
        rewards_panel.SetActive(false);
    }
}
