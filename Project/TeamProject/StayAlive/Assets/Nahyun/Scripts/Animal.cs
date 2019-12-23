using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{

    NavMeshAgent nav;

    public Player_State playerstate; // 플레이어 상태 스크립트

    public Transform[] Pos; // 랜덤하게 이동하게되는 목적지
    public Transform Target; // 플레이어 타겟
    public GameObject[] item; // 죽을때 생성될 아이템

    public float Max_Health = 100, current_health, damage = 10; // 최대체력,현재체력,데미지 변수
    public float exp = 30f; // 경험치 변수
 

    IEnumerator coroutine;

    //애니메이션 관련 변수
    Animator anim;
    enum Anim { Idle, Walk, Run, Sit, Eat };
    Anim currentAnim = Anim.Idle;

    // 오디오 관련 변수
    public AudioSource Musicplayer;
    public AudioClip sound;

    public Rigidbody rb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (!playerstate)
        { // 게임 시작시 플레이어 오브젝트를 할당
            playerstate = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_State>();
        }

        coroutine = ChangePos();
        current_health = Max_Health; // 체력을 최대체력으로 설정
        nav = GetComponent<NavMeshAgent>();
        StartCoroutine(coroutine);

        anim = GetComponent<Animator>();
        anim.SetTrigger("Walk"); // 걷는 애니메이션
        currentAnim = Anim.Walk; // 걷는 상태 설정
        nav.speed = 2f; // 이동속도 2 설정

        Musicplayer = GetComponent<AudioSource>();
        Musicplayer.clip = sound;
        rb.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        Change_Target(); // 목적지를 변경하는 함수
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player_attack")) // 플레이어의 공격에 맞을경우
        {
            Lose_Health(DataController.instance.gameData.player_Pow);
                Musicplayer.Play();
            rb.isKinematic = false;
        }
    }

    public void Lose_Health(float val) // 체력 감소 함수
    {
        current_health -= val;
        if (current_health <= 0) // 체력이 0 이 하가 되면 죽음
            Dead();
    }

    public void Change_Target() // 목적지 변경 함수
    {
        if (current_health < Max_Health) // 공격을 받아 체력이 감소하면 플레이어를 따라다님
        {
            StopCoroutine(coroutine);
            nav.SetDestination(Target.position);
            transform.LookAt(Target);

            anim.SetTrigger("Run"); // 뛰는 애니메이션

            currentAnim = Anim.Run; // 뛰는 상태 설정
            nav.speed = 4f; // 이동속도 4 설정
        }
    }

    public void Dead() // 죽음 함수
    {
        Instantiate(item[Random.Range(0, 2)], transform.position, transform.rotation); // 랜덤한 아이템을 생성
        playerstate.Take_exp(exp); // 플레이어에게 경험치 제공
        this.gameObject.SetActive(false); // 오브젝트를 삭제하지 않고 잠시 비활성화.
        current_health = Max_Health;  // 체력을 다시 최대치로 설정
        rb.isKinematic = true;
    }

    IEnumerator ChangePos()
    {
        while (true)
        {
            int a = Random.Range(0, 6);
            nav.SetDestination(Pos[a].position);
            transform.LookAt(Pos[a]);
            yield return new WaitForSeconds(30f); // 30초 마다 목적지를 변경
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player")) // 플레이어에 닿을경우 플레이어 체력감소 
        {
            playerstate.Lose_Health(damage); // 플레이어 체력 감소 함수
        }
    }

    void OnEnable()
    {
        Start();
    }

}

