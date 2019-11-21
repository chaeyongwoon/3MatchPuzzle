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


    /*
    public float Max_health = 100;
    public float Current_health = 100;
    public float Damage = 10;
    public float Defend = 0f;
    public float Healing = 0f;
    public float Reroad = 0.6f;
    public float reroad_term = 0f;

    public float Damage_level = 1f;
    public float Max_health_level = 1f;
    public float Defend_level = 1f;
    public float Healing_level = 1f;
    public float Reroad_level = 1f;

    public int color_num = 0;

    public float Money = 0f;
    public bool is_shoot = false;
    */

    

    // Use this for initialization
    void Start()
    {  
        StartCoroutine("Take_Heal");
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
            SceneManager.LoadScene("2.town");
        }


        if (transform.position.y <= -5f)
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


    public void Take_Damage(float damage)
    {
        if (isdamaged == false)
        {
            isdamaged = true;
            DataController.instance.gameData.Current_health -= damage * (100 - DataController.instance.gameData.Defend) * 0.01f * Random.Range(0.8f, 1.4f);

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
        transform.position = new Vector2(0f, 3f);
        Camera.main.transform.position = new Vector3(0, 3, -10);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.transform.CompareTag("To_town"))
            SceneManager.LoadScene("2.town");


    }

}
