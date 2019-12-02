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

    public Rigidbody2D rb;
    public float x_force, y_force;

    public GameObject[] color_img;
    public GameObject rewards_panel;
    public Text rewards_text;

    public Animator ani;

    public AudioSource audiosource;
    public AudioClip shoot_sound, dead_sound;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("Take_Heal");
        rb = GetComponent<Rigidbody2D>();
        ani = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {

        DataController.instance.gameData.Reload_term += Time.deltaTime;
        fire_position = transform.position + transform.right * 2;
        if (!ani.GetBool("Dead"))
        {
            if (Input.GetKey(KeyCode.Z))
            {
                Shoot();
            }
        }

        if (DataController.instance.gameData.Is_shoot == true)
        {
            Shoot();
        }


        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);
            SceneManager.LoadScene("2.town");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);
            SceneManager.LoadScene("3.stage1");

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);
            SceneManager.LoadScene("4.stage2");

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);
            SceneManager.LoadScene("5.stage_infinite");

        }


        if (transform.position.y <= -100f)
        {
            StartCoroutine(Dead());
        }

    }


    public void OnAttackDown()
    {
        DataController.instance.gameData.Is_shoot = true;
    }

    public void OnAttackUp()
    {
        DataController.instance.gameData.Is_shoot = false;
    }

    public void Shoot()
    {

        if (DataController.instance.gameData.Reload_term > DataController.instance.gameData.Reload)
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
            DataController.instance.gameData.Current_health -= damage * (100 - DataController.instance.gameData.Defend) * 0.01f * Random.Range(0.8f, 1.4f);

            if (DataController.instance.gameData.Current_health <= 0)
            {
                DataController.instance.gameData.Current_health = 0;
            }
            Vector3 direction = (transform.position - col_pos).normalized;

            rb.AddForce(new Vector2(direction.x * x_force, y_force));

            if (DataController.instance.gameData.Current_health <= 0f)
            {
                StartCoroutine(Dead());
            }
            StartCoroutine(Rehit());
        }
    }

    public IEnumerator Rehit()
    {
        yield return new WaitForSeconds(1f);
        isdamaged = false;
    }


    public IEnumerator Dead()
    {
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
            SceneManager.LoadScene("2.town");
        }
    }

    public void Color_Change_Red()
    {
        DataController.instance.gameData.Color_num = 0;
        Game_Manager.instance.Color_change();
        color_img[0].SetActive(true);
        color_img[1].SetActive(false);
        color_img[2].SetActive(false);
        color_img[3].SetActive(false);
    }
    public void Color_Change_Yellow()
    {
        DataController.instance.gameData.Color_num = 1;
        Game_Manager.instance.Color_change();
        color_img[0].SetActive(false);
        color_img[1].SetActive(true);
        color_img[2].SetActive(false);
        color_img[3].SetActive(false);
    }
    public void Color_Change_Green()
    {
        DataController.instance.gameData.Color_num = 2;
        Game_Manager.instance.Color_change();
        color_img[0].SetActive(false);
        color_img[1].SetActive(false);
        color_img[2].SetActive(true);
        color_img[3].SetActive(false);
    }
    public void Color_Change_Blue()
    {
        DataController.instance.gameData.Color_num = 3;
        Game_Manager.instance.Color_change();
        color_img[0].SetActive(false);
        color_img[1].SetActive(false);
        color_img[2].SetActive(false);
        color_img[3].SetActive(true);
    }

    IEnumerator Take_Heal()
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

    /*
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        //   transform.position = new Vector2(0f, 3f);
        //  Camera.main.transform.position = new Vector3(0, 3, -10);
    }*/

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("To_town"))
        {
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);


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

            SceneManager.LoadScene("2.town");
        }

        if (coll.transform.CompareTag("Hole"))
        {
            StartCoroutine(Dead());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Snow_storm"))
        {
            StartCoroutine(Dead());
        }
    }

    public void Clear()
    {
        transform.position = new Vector2(0f, 3f);
        Camera.main.transform.position = new Vector3(0, 3, -10);

        rewards_panel.SetActive(true);
        float rewards = 10000 * Game_Manager.instance.stage_level;
        DataController.instance.gameData.Money += rewards;
        rewards_text.text = string.Format("{0}", rewards);

        SceneManager.LoadScene("2.town");
    }
    public void close_rewards_panel()
    {
        rewards_panel.SetActive(false);
    }
}
