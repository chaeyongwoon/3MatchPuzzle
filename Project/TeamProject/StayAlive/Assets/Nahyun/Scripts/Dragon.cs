using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Dragon : MonoBehaviour
{
    NavMeshAgent nav;
    Animation anim;

    public Player_State playerstate; // 플레이어 상태 변수

    public float HP = 100; // 최대체력
    public float CurHP; // 현재체력
    public float exp = 30f; // 경험치
    public float damage = 10f; // 데미지
    public Transform Nestpos; // 둥지 위치
    public GameObject DragonEgg; // 드래곤 알 오브젝트
    public Transform Target;
    public float limit_dis = 10f;

    public bool isDead = false; // 죽음 판별 변수
    // Use this for initialization
    void Start()
    {
        //체력 최대치설정, 컴포넌트값 받아오기 등 게임시작시 초기설정
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        anim.Play("idle");
        CurHP = HP;

        DragonEgg.SetActive(false);

        if (!playerstate)
        {
            playerstate = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>();
        }
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            Attack(); // 공격함수
        }

    }

    public void Attack()
    { // 플레이어와 일정거리 이상 가까워지면 플레이어를 쫓아다님  
        if (Vector3.Distance(transform.position, Target.position) <= limit_dis)
        {
            nav.SetDestination(Target.position);
            if (CurHP >= 50)
            {
                anim.Play("attack");
            }
            else
            {
                anim.Play("tail_attack");
            }
        }
        else
        {
            nav.SetDestination(Nestpos.transform.position);
            anim.Play("fly");
        }
    }

    public void Dead() // 죽음함수
    {
        isDead = true;
        anim.Play("death");
        nav.SetDestination(Nestpos.transform.position);

        Destroy(this.gameObject, 2f);
        playerstate.Take_exp(20); //플레이어에게 경험ㄴ치 제공
        DragonEgg.SetActive(true);
    }

    public void Lose_Health(float val) // 체력 감소 함수
    {
        CurHP -= val;
        anim.Play("idle0");
        if (CurHP <= 0) // 체력이 0 이하로 감소하면 죽음함수 호출
            Dead();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player")) // 플레이어에 닿을경우 플레이어 체력감소
        {
            playerstate.Lose_Health(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_attack")) // 플레이어의 공격에 닿을경우 체력 감소
        {
            Lose_Health(DataController.instance.gameData.player_Pow);
        }
    }

}
