using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class petmove : MonoBehaviour
{

    public Transform target;

    public float Speed = 2f;

    public float dis;
    public float limit_dis = 2f; // 플레이어와의 일정 제한 거리

    Animator animator;
    Rigidbody rb;
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        dis = Vector3.Distance(target.position, transform.position);

        if (dis >= limit_dis)
        {
            animator.SetBool("walk", true);
            transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * Speed); //그 둘 사이의 값을 더해 보정한다. 이렇게 되면 멀어지면 따라간다.
            transform.LookAt(target);
        }
        else
            animator.SetBool("walk", false);

    }
}
