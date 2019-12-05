using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{


    public Animator animator;
    public float h, v;
    public Rigidbody2D rb;
    public float Speed = 5f;
    public float JumpPower = 5f;
    public bool isJump = true;
    public bool isleft = false;

    public AudioSource audiosource;
    public AudioClip jump_sound;

    // Use this for initialization
    void Start()
    {
        if (!animator)
        {
        animator = GetComponent<Animator>();
        }
        if (!rb)
        {
            rb = GetComponent<Rigidbody2D>();
        }
        if (!audiosource)
        {
            audiosource = GetComponent<AudioSource>();
        }
            audiosource.clip = jump_sound;
        h = 0;
    }

    // Update is called once per frame
    void Update()
    {      
        /// PC용 키 설정//
        if (Input.GetKeyDown(KeyCode.A))
        {
            LeftDown();
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            LeftUp();
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            RightDown();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            RightUp();
        }


        if (isleft == true)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (isleft == false)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        if (!animator.GetBool("Dead"))
        {
            rb.velocity = new Vector2(h * Speed, rb.velocity.y);
            animator.SetFloat("Speed", rb.velocity.x);
            if (Input.GetButton("Jump"))
            {
                Jump();
            }
        }


    }

    public void Jump()
    {
        if (isJump == false)
        {
            isJump = true;
            audiosource.clip = jump_sound;
            audiosource.Play();
            animator.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, JumpPower); // 파워 5 정도면 충분
            //rb.AddForce(new Vector2(0f, JumpPower)); // 파워 400정도가 위 코드 5랑 비슷
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Enemy")) // 땅이나 몬스터에 닿았을경우 점프 사용가능
            isJump = false; ;
        animator.SetBool("Jump", false);
    }

   /// 모바일 용 UI에 연결된 트리거 함수
    public void LeftDown()
    {
        h = -1;
        isleft = true;
    }

    public void LeftUp()
    {
        h = 0;
    }
    public void RightDown()
    {
        h = 1;
        isleft = false;
    }
    public void RightUp()
    {
        h = 0;
    }

}
