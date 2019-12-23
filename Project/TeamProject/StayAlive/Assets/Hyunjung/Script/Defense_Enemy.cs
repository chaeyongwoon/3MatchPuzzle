using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defense_Enemy : MonoBehaviour
{
    public Player_State playerstate; // 플레이어의 상태를 참조하기 위한 스크립트
    public static Defense_Enemy instance; // 싱글턴 패턴을 위한 인스턴스 
    public float enemyHP; // 몬스터 현재 체력
    public float enemyHP_Max = 100f; // 몬스터의 최대 체력

    public Transform target; // 쫓아갈 타겟 오브젝트
    private float Heigth = -0.3f;
    private float zDistance = -1.0f;
    private float xDistance = 1.3f;
    public float Speed; // 이동 속도
    public Game_Manager game_ma; // 몬스터를 생성하기 위한 조건판별 게임 매니저 스크립트
    float distance; // 해당 오브젝트와 유지할 거리
    public float playerdistance;
    public float playerattackdistance;

    public Animator anim;
    // Use this for initialization
    public enum State
    {
        Idle,
        Walk,
        Attack,
        FenseAttack,
        Damage,
        Die
    }
    public State defense_enemyState;

    void Start()
    {
        if (!playerstate) // 게임 시작시 플레이어 오브젝트가 할당되어 있지 않다면 새로 할당
        {
            playerstate = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>();
        }
        Defense_Enemy.instance = this;
        game_ma = GameObject.FindGameObjectWithTag("gameManager").GetComponent<Game_Manager>(); // 게임 매니저 스크립트 할당
        enemyHP = enemyHP_Max; // 현재 체력을 최대 체력으로 설정
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector3.Distance(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);
        // 플레이어와 몬스터의 거리
        if (defense_enemyState == State.Idle) // 몬스터의 기본상태
        {
            anim.SetBool("Idle", true);
            AttackTarget();
        }
        if (defense_enemyState == State.Walk) // 몬스터의 이동상태
        {
            anim.SetBool("Walk", true);
            anim.SetBool("Attack", false);
            AttackTarget();

        }
        if (defense_enemyState == State.Attack) // 몬스터의 공격상태
        {
            //몬스터 애니매이션 공격
            anim.SetBool("Attack", true);
            AttackTarget();

        }
        if (defense_enemyState == State.FenseAttack) // 몬스터의 펜스 공격상태
        {
            anim.SetBool("Attack", true);

            if (distance <= playerdistance) // 플레이어와의 거리를 계산
            {//플레이어와의 거리가 일정 거리 이하일때 플레이어를 따라옴
                defense_enemyState = State.Walk;
            }

        }
        if (defense_enemyState == State.Damage)
        {
            // 몬스터 애니매이션 데미지 입는 애니매이션
            anim.SetBool("Damage", true);

        }
        if (defense_enemyState == State.Die)
        {
            //몬스터 애니매이션 죽는다.
            anim.SetBool("Die", true);
        }
        if (game_ma.day_state == Game_Manager.State.Day)
        {
            Destroy(gameObject);
        }
        if (enemyHP <= 0)
        {
            defense_enemyState = State.Die;
        }
    }
    public void AttackTarget()
    {   //플레이어가집이 있을때

        if (DataController.instance.gameData.house_1 == true)
        {
            if (distance > playerdistance) // 플레이어와의 거리가 일정 거리보다 크다면 타겟을 집으로 설정
            {
                target = GameObject.FindGameObjectWithTag("home1").transform;
                FollowTarget(target);
            }
            if (distance <= playerdistance)
            {//플레이어와의 거리가 일정 거리 이하일때 플레이어를 따라옴
                defense_enemyState = State.Walk;
                target = GameObject.FindGameObjectWithTag("Player").transform;
                FollowTarget(target);
            }
            if (distance <= playerattackdistance)
            {//공격거리일때 플레이어를 공격한다.
                defense_enemyState = State.Attack;
            }
        }
        if (DataController.instance.gameData.house_2 == true)
        {
            if (distance > playerdistance)
            {
                target = GameObject.FindGameObjectWithTag("home2").transform;
                FollowTarget(target);
            }
            if (distance <= playerdistance)
            {
                defense_enemyState = State.Walk;
                target = GameObject.FindGameObjectWithTag("Player").transform;
                FollowTarget(target);
            }
            if (distance <= playerattackdistance)
            {
                defense_enemyState = State.Attack;
            }
        }
        if (DataController.instance._gameData.house_3 == true)
        {
            if (distance > playerdistance)
            {
                target = GameObject.FindGameObjectWithTag("home3").transform;
                FollowTarget(target);
            }
            if (distance <= playerdistance)
            {
                defense_enemyState = State.Walk;
                target = GameObject.FindGameObjectWithTag("Player").transform;
                FollowTarget(target);
            }
            if (distance <= playerattackdistance)
            {
                defense_enemyState = State.Attack;
            }
        }
        if (DataController.instance.gameData.house_0 == true)
        {
            //플레이어가 집이 없을때 상태
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInCave>().playerPos == PlayerInCave.State.InIsland)
            {
                target = GameObject.FindGameObjectWithTag("Player").transform;
                FollowTarget(target);
            }
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInCave>().playerPos == PlayerInCave.State.InCave)
            {
                defense_enemyState = State.Idle;
            }
        }

    }

    public void FollowTarget(Transform tar)
    {
        Vector3 newPos = tar.position + new Vector3(xDistance, Heigth, -zDistance);
        // 타겟 포지선에 해당 위치를 더해.. 즉 타겟 주변에 위치할 위치를 담는다.. 일정의 거리를 구하는 방법
        transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * Speed);
        //그 둘 사이의 값을 더해 보정한다. 이렇게 되면 멀어지면 따라간다.
        transform.LookAt(tar);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            playerstate.Lose_Health(10); // 플레이어와 닿으면 플레이어에게 10의 데미지를 준다.
        }

        if (collision.transform.CompareTag("Bullet"))
        {
            Lose_Health(30); // 총알 오브젝트에 닿으면 30의 피해를 입는다
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_attack"))
        {            
            Lose_Health(DataController.instance.gameData.player_Pow); // 플레이어의 공격에 닿으면 플레이어의 공격력 만큼 피해를 받는다
        }
    }
    void Lose_Health(int val) // 체력 감소 함수
    {
        enemyHP -= val;
        if (enemyHP <= 0) // 체력이 0 이하로 감소하면 죽는 함수 호출
            Dead();
    }

    void Dead()
    {
        playerstate.Take_exp(20); // 몬스터가 죽을시 플레이어에게 20의 경험치를 제공
        Destroy(this.gameObject);
    }


}
