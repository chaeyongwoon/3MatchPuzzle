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
        // 플레이어와 게임매니저 오브젝트 참조
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_attack_health>();
        gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();

        // 체력 초기화
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

    public void Take_damage(float damage) // 체력손실 함수
    {
        float real_damage = damage * Random.Range(0.8f, 1.4f); // 80%~140%의 랜덤한 데미지

        current_health -= real_damage;
        hp_slider.value = current_health / max_health;

        //////////데미지 텍스트 띄우기 /////////
        Text obj = Instantiate(damage_text, transform.position, transform.rotation);
        obj.text = "" + Mathf.Floor(real_damage); 
        obj.transform.parent = transform.GetChild(0);
        obj.rectTransform.localScale = new Vector2(1, 1);

        DataController.instance.gameData.Money += Mathf.Floor(real_damage) ;

        if (current_health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Revival() // 부활 (재 생성) 함수
    {
        //일반 던전에서는 호출되지 않으며 무한던전에서만 보스몬스터가 호출
        transform.position = orign_pos; // 가장 초기위치로 이동
    
        current_health = max_health;
        hp_slider.value = current_health / max_health;
    }
}
