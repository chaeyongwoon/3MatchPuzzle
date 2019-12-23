using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret1 : MonoBehaviour
{

    public GameObject Bullet; // 총알 오브젝트 
    public Transform FirePos; // 총알 생성 위치
    public Transform target;  // 타겟 오브젝트

    public bool lockon = false; // 타겟팅 판별 변수
    public BoxCollider box_col; // 콜라이더 변수
    public float term = 0; // 발사 대기시간 측정변수

    public float fire_term = 1f; // 설정할 발사 대기시간
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Shooting()); // 코루틴 함수 시작
        lockon = false; // 타겟팅 비활성화
        box_col = GetComponent<BoxCollider>(); // 콜라이더 컴포넌트 받아옴
    }

    // Update is called once per frame
    void Update()
    {
        if (target)  // 타겟팅 상태인경우
        {
            transform.LookAt(target); // 타겟팅이 되면 타겟 오브젝트를 바라봄
        }
        else if (!target) // 타겟팅 상태가 아닌경우
        {
            lockon = false;
            box_col.enabled = false;
        }

        term += Time.deltaTime;

        if (term >= 1f) 
        {
            term = 0;
            box_col.enabled = true;

        }
    }

    IEnumerator Shooting() // 총알을 발사하는 함수
    {
        while (true)
        {
            if (lockon == true) // 타겟팅 상태일 경우
            {
                Instantiate(Bullet, FirePos.transform.position, transform.rotation); // 총알 오브젝트 생성
           }
                yield return new WaitForSeconds(fire_term); // 설정한 시간만큼 시간 지연 후 재발사
        }

    }

    void OnTriggerEnter(Collider col)
    {
        if (lockon == false) // 타겟팅이 아닌상태일 겨우
        {
            if (col.CompareTag("defense_enemy")) // 범위안에 몬스터가 들어올경우
            {
                lockon = true; // 타겟팅상태로 전환
                target = col.transform;             // 타겟오브젝트 설정   
            }
        }
    }

}