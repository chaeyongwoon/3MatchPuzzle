using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour {

    NavMeshAgent nav;
    Animator anim;
    public Player_State playerstate; // 플레이어 상태 변수

    public float exp = 20f; // 경험치
    public float damage = 8f; // 데미지
    public float HP = 50; // 최대체력
    public float CurHP; // 현재체력
    public Transform Pos;
    public float Distance; // 거리

    public AudioSource Musicplayer;

    // Use this for initialization
    void Start () {
        // 게임 시작시 초기설정
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        CurHP = HP;
        anim.SetBool("Walk", true);

        if (!playerstate)
        {
            playerstate = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>();
        }
        Musicplayer = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
         
        Distance = Vector3.Distance(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position); // 플레이어와의 거리를 계산
        Attack();
    }

    public void Attack()
    {
        if (Distance <= 7) // 플레이어와의 거리가 7 이하일경우
        {
            nav.SetDestination(GameObject.FindGameObjectWithTag("Player").transform.position); // 플레이어를 쫓아다님
            anim.SetBool("Walk", false);
            anim.SetTrigger("Bite Attack");
            if(CurHP <= 25)
            {
                nav.speed = 5; // 체력이 25 이하일경우 속도를 5로 설정
            }
        }
        else
        {
            nav.SetDestination(Pos.transform.position);
            anim.ResetTrigger("Bite Attack");
            anim.SetBool("Walk", true);
        }
    }

    public void Dead() // 죽음 함수 
    {
        anim.SetTrigger("Die"); // 죽음 애니메이션 
        playerstate.Take_exp(10); // 플레이어 경험치 제공
        Destroy(this.gameObject); // 오브젝트 삭제  
    }

    public void Lose_Health(float val)
    {
        Musicplayer.Play();
        CurHP -= val;
        if (CurHP <= 0)
            Dead();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player")) // 플레이어에 닿을 경우
        {
            playerstate.Lose_Health(10); // 플레이어의 체력 감소 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_attack")) // 플레이어의 공격에 닿을 경우
        {
            Lose_Health(DataController.instance.gameData.player_Pow); // 체력감소          
        }
    }

}
