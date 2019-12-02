using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ice_block : MonoBehaviour
{
    public Player_attack_health player;
    public Game_Manager gm;

    public float max_health;
    public float current_health;

    public Text damage_text;
    public int damage_num;

    public Slider hp_slider;

    public Vector3 orign_pos;

    public enum Color
    {
        red,
        yellow,
        green,
        blue
    }

    public Color state;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_attack_health>();
        gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();

        max_health = 100 + 1000 * gm.stage_level;
        current_health = max_health;
        hp_slider.value = current_health / max_health;

        orign_pos = transform.position;
    }


    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("bullet"))
        {
            if (state.ToString() == collision.gameObject.GetComponent<player_bullet>().state.ToString())
            {

                Take_damage(DataController.instance.gameData.Damage);
            }
        }
    }

    public void Take_damage(float damage)
    {
        float real_damage = damage * Random.Range(0.8f, 1.4f);

        current_health -= real_damage /* * (100 - defend) * 0.01f*/;
        hp_slider.value = current_health / max_health;

        Text obj = Instantiate(damage_text, transform.position, transform.rotation);
        obj.text = "" + Mathf.Floor(real_damage); /* * (100 - defend) * 0.01f*/
        obj.transform.parent = transform.GetChild(0);
        obj.rectTransform.localScale = new Vector2(1, 1);

        DataController.instance.gameData.Money += Mathf.Floor(real_damage) ;

        if (current_health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Revival()
    {
        transform.position = orign_pos;
    
        current_health = max_health;
        hp_slider.value = current_health / max_health;
    }
}
