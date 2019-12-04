using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chest : MonoBehaviour
{

    public float max_health;
    public float current_health;

    public Text damage_text;


    // Use this for initialization
    void Start()
    {
        // 체력 초기화
        max_health = 10000;
        current_health = max_health;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.transform.CompareTag("bullet"))
        {
            Take_damage(DataController.instance.gameData.Damage);
        }
    }


    public void Take_damage(float damage) // 체력 손실 함수
    {
        float real_damage = damage * Random.Range(0.8f, 1.4f);
        current_health -= real_damage;

        /// 데미지 텍스트 띄우기 ///
        Text obj = Instantiate(damage_text, transform.position, transform.rotation);
        obj.text = "" + Mathf.Floor(real_damage);
        obj.transform.parent = transform.GetChild(0);
        obj.rectTransform.localScale = new Vector2(1, 1);

        DataController.instance.gameData.Money += Mathf.Floor(real_damage) * 5; // 일반 몬스터보다 5배 많은 금화를 획득

        if (current_health <= 0)
        {
            Dead();
        }
    }

    public void Dead()
    {
        Destroy(this.gameObject);
    }

}
