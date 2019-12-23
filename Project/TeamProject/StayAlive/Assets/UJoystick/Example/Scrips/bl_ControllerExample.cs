using UnityEngine;

public class bl_ControllerExample : MonoBehaviour
{

    /// <summary>
    /// Step #1
    /// We need a simple reference of joystick in the script
    /// that we need add it.
    /// </summary>
	[SerializeField] private bl_Joystick Joystick;//Joystick reference for assign in inspector

    [SerializeField] private float Speed = 5;

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

    private void Start()
    {
        charactercontroller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        Musicplayer = GetComponent<AudioSource>();
        Musicplayer.clip = walk;
    }

    void Update()
    {
        if (!animator.GetBool("Attack")) // 공격중일때 움직이지 않도록 추가
        {
            if (!animator.GetBool("Dead"))
            {


                v = Joystick.Vertical; //get the vertical value of joystick
                h = Joystick.Horizontal;//get the horizontal value of joystick

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