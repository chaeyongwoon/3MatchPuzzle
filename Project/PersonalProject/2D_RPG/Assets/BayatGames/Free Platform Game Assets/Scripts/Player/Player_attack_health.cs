using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player_attack_health : MonoBehaviour
{
    public static Player_attack_health instance;

    

    public GameObject[] bullet;
    public Vector2 fire_position;

    public bool isdamaged = false;

    public Rigidbody2D rb;
    public float x_force, y_force;

    

    // Use this for initialization
    void Start()
    {  
        StartCoroutine("Take_Heal");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        DataController.instance.gameData.Reload_term += Time.deltaTime;
        fire_position = transform.position + transform.right * 2;

        if (Input.GetKey(KeyCode.Z))
        {
            Shoot();
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
            Dead();
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

            Vector3 direction = (transform.position - col_pos).normalized;

            rb.AddForce(new Vector2(direction.x*x_force, y_force));

            if (DataController.instance.gameData.Current_health <= 0f)
            {
                Dead();
            }
            StartCoroutine(Rehit());
        }
    }

    public IEnumerator Rehit()
    {
        yield return new WaitForSeconds(1f);
        isdamaged = false;
    }


    public void Dead()
    {
        transform.position = new Vector2(0f, 3f);
        Camera.main.transform.position = new Vector3(0, 3, -10);

        DataController.instance.gameData.Current_health = 50f;
        SceneManager.LoadScene("2.town");
    }

    public void Color_Change_Red()
    {
        DataController.instance.gameData.Color_num = 0;
        Game_Manager.instance.Color_change();
    }
    public void Color_Change_Yellow()
    {
        DataController.instance.gameData.Color_num = 1;
        Game_Manager.instance.Color_change();
    }
    public void Color_Change_Green()
    {
        DataController.instance.gameData.Color_num = 2;
        Game_Manager.instance.Color_change();
    }
    public void Color_Change_Blue()
    {
        DataController.instance.gameData.Color_num = 3;
        Game_Manager.instance.Color_change();
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


    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene , LoadSceneMode mode)
    {
     //   transform.position = new Vector2(0f, 3f);
      //  Camera.main.transform.position = new Vector3(0, 3, -10);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("To_town"))
        {
            transform.position = new Vector2(0f, 3f);
            Camera.main.transform.position = new Vector3(0, 3, -10);

            SceneManager.LoadScene("2.town");
        }

        if (coll.transform.CompareTag("Hole"))
        {
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Snow_storm"))
        {
            Dead();
        }
    }

}
