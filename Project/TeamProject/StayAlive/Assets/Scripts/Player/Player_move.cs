using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_move : MonoBehaviour
{


    public float MoveSpeed = 5f;
    public float RotationSpeed = 360f;

    CharacterController charactercontroller;
    Animator animator;

    public float v;
    public float h;

    public float gravity = -80f;

    public AudioSource Musicplayer;
    public AudioClip walk;
    public bool isplay = false;
    // Use this for initialization
    private void Start()
    {
        charactercontroller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Musicplayer = GetComponent<AudioSource>();
        Musicplayer.clip = walk;
    }

    // Update is called once per frame
    void Update()
    {


        if (!animator.GetBool("Attack")) // 공격중일때 움직이지 않도록 추가
        {
            if (!animator.GetBool("Dead"))
            {

                h = Input.GetAxis("Horizontal");
                v = Input.GetAxis("Vertical");

                Vector3 direction = new Vector3(h, 0, v);
                if (direction.sqrMagnitude > 0.01f)
                {
                    Vector3 forward = Vector3.Slerp(transform.forward, direction, RotationSpeed * Time.deltaTime / Vector3.Angle(transform.forward, direction));
                    transform.LookAt(transform.position + forward);
                }
                direction.y += gravity * Time.deltaTime;
                charactercontroller.Move(direction * MoveSpeed * (1 + DataController.instance._gameData.Spead_Up) * Time.deltaTime);

                animator.SetFloat("Speed", charactercontroller.velocity.magnitude);

                if (charactercontroller.velocity.magnitude >= 0.2f)
                {
                    if (isplay == false)
                    {
                        isplay = true;
                        Musicplayer.clip = walk;
                        if (Musicplayer.isPlaying == false)
                        {
                            Musicplayer.Play();
                        }
                    }
                }
                else
                {
                    if (isplay == true)
                    {
                        isplay = false;
                        Musicplayer.Stop();
                    }
                }

            }
        }
    }
}