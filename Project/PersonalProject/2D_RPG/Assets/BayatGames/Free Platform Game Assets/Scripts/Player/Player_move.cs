using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour {


    public Animator animator;
    public float h, v;
    public Rigidbody2D rb;
    public float Speed = 5f;
    public float JumpPower = 5f;
    public bool isJump = true;

   
    // Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        h = 0;
	}
	
	// Update is called once per frame
	void Update () {
        // h = Input.GetAxis("Horizontal");

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

        if (h < 0f)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (h > 0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        rb.velocity = new Vector2(h * Speed, rb.velocity.y);
        animator.SetFloat("Speed", rb.velocity.x);

        if (Input.GetButton("Jump"))
        {
            Jump();
        }


    }

    public void Jump()
    {
        if (isJump == false)
        {
            isJump = true;
            animator.SetBool("Jump", true);
            rb.velocity = new Vector2(rb.velocity.x, JumpPower); // 파워 5 정도면 충분
            //rb.AddForce(new Vector2(0f, JumpPower)); // 파워 400정도가 위 코드 5랑 비슷
        }
    }

   

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Ground") || collision.transform.CompareTag("Enemy"))
            isJump = false; ;
        animator.SetBool("Jump", false);
    }

    public void LeftDown()
    {
        h = -1;
    }

    public void LeftUp()
    {
        h = 0;
    }
    public void RightDown()
    {
        h = 1;
    }
    public void RightUp()
    {
        h = 0;
    }
         
}
