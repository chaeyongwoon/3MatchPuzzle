using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player_State : MonoBehaviour
{
    Animator animator;

    // 게임오브젝트 ----------------------------------------------------------------
    public GameObject Raft, Sos_sign; // 배 오브젝트, SOS 사인 오브젝트
    public GameObject Lighting;
    public GameObject stone_item; // 돌 아이템
    public GameObject[] tree_item; // 나무가 생성하는 아이템 배열로 저장
    public SphereCollider attack_col; // 플레이어 공격범위를 나타낼 콜라이더
    GameObject hitobject;
    public GameObject[] Weapon; // 플레이어 무기
    public GameObject to_cave_portal, to_island_portal; // 섬과 동굴을 이동하는 포탈

    // UI 오브젝트 ----------------------------------------------------------------
    public GameObject Raft_button, Sos_sign_button;
    public GameObject cooking;
    public Slider cook_maker;
    public Text Rescue_text;
    public Text Level_text, HP_text, Hungry_text, Exp_text;
    public Slider HP_slider, Hungry_slider, Exp_slider;
    public GameObject status_panel;
    public Slider head_slider, body_slider, arms_slider, legs_slider, hands_slider, foots_slider;
    public Text[] Quick_item;
    public Text damage_text;
    public GameObject Notree;
    public Text ripemeat_text;

    // 메테리얼 ----------------------------------------------------------------
    public float cook_make_time;
    public Material In_Cave, In_Island;

    // 게임 변수----------------------------------------------------------------
    public float cook_maxtime;
    public bool cook_complete = false;
    public bool damaged = false;
    public float noHomeHP; //플레이어에게 집이 없을때 HP줄어드는 양 설정
    public enum State { none, sword, axe, pickaxe };
    State weapon = State.none;
    public bool status = false;
    public bool Isdead = false;
    public bool Infire = false;

    // 카메라 종류 ----------------------------------------------------------------
    public Camera customizing_camera, main_cam;


    // 커스터마이징 관련 변수 ----------------------------------------------------------------
    public Transform head, body, left_arm, right_arm, left_leg, right_leg, left_hand, right_hand, left_foot, right_foot;

    //  오디오 관련 변수 ----------------------------------------------------------------
    public AudioSource Musicplayer;
    public AudioClip attack, item, weapon_change, rock_crash, eat, dead, make;

    // Use this for initialization
    void Start()
    {
        main_cam = Camera.main;
        /*
         게임 시작시 플레이어의 공격력, 체력을 현재 레벨에 맞게 재설정 및 UI를 초기화
         */
        DataController.instance.gameData.player_Pow = 8 + 2 * DataController.instance.gameData.player_Level; // 공격력 설정
        DataController.instance.gameData.player_MaxHp = 90 + 10 * DataController.instance.gameData.player_Level; // 체력 설정
        HP_slider.value = DataController.instance.gameData.player_Hp / DataController.instance.gameData.player_MaxHp; // 체력UI
        Hungry_slider.value = DataController.instance.gameData.player_Hungry / DataController.instance.gameData.player_Hungry; //배고픔 UI
        Exp_slider.value = DataController.instance.gameData.player_Exp / DataController.instance.gameData.player_MaxExp; // 경험치UI
        HP_text.text = string.Format("HP : {0} / {1}", DataController.instance.gameData.player_Hp, DataController.instance.gameData.player_MaxHp); // 체력 UI
        Hungry_text.text = string.Format("Hungry : {0} / {1}", DataController.instance.gameData.player_Hungry, DataController.instance.gameData.player_MaxHungry); // 배고픔 UI
        Exp_text.text = string.Format("Exp : {0} / {1}", DataController.instance.gameData.player_Exp, DataController.instance.gameData.player_MaxExp); // 경험치 UI
        Level_text.text = string.Format("Level : {0}", DataController.instance.gameData.player_Level); // 레벨 UI
        Quick_item[0].text = string.Format("{0}", DataController.instance.gameData.meat_count); // 아이템버튼1
        Quick_item[1].text = string.Format("{0}", DataController.instance.gameData.tree_count); // 아이템버튼2
        Rescue_text.text = string.Format("{0}%", DataController.instance._gameData.Rescue_probability); // 구조확률 UI
        DataController.instance.gameData.hungry_val = 0.3f; // 배고픔 감소속도
        Musicplayer = GetComponent<AudioSource>();


        cooking.SetActive(false);
        cook_maker = cooking.GetComponent<Slider>();
        cook_make_time = cook_maxtime;
        cook_maker.maxValue = cook_maxtime;




        /*
         * 이전에 저장했던 커스터마이징 값으로 캐릭터의 외형을 변환, 저장된 값이 없다면 기본값.
         * 각 파츠별로 크기값 조절 가능
         */
        head.localScale = new Vector3(DataController.instance.gameData.head, DataController.instance.gameData.head, DataController.instance.gameData.head);
        body.localScale = new Vector3(DataController.instance.gameData.body, DataController.instance.gameData.body, DataController.instance.gameData.body);
        left_arm.localScale = new Vector3(DataController.instance.gameData.arms, DataController.instance.gameData.arms, DataController.instance.gameData.arms);
        right_arm.localScale = new Vector3(DataController.instance.gameData.arms, DataController.instance.gameData.arms, DataController.instance.gameData.arms);
        left_leg.localScale = new Vector3(DataController.instance.gameData.legs, DataController.instance.gameData.legs, DataController.instance.gameData.legs);
        right_leg.localScale = new Vector3(DataController.instance.gameData.legs, DataController.instance.gameData.legs, DataController.instance.gameData.legs);
        left_hand.localScale = new Vector3(DataController.instance.gameData.hands, DataController.instance.gameData.hands, DataController.instance.gameData.hands);
        right_hand.localScale = new Vector3(DataController.instance.gameData.hands, DataController.instance.gameData.hands, DataController.instance.gameData.hands);
        left_foot.localScale = new Vector3(DataController.instance.gameData.foots, DataController.instance.gameData.foots, DataController.instance.gameData.foots);
        right_foot.localScale = new Vector3(DataController.instance.gameData.foots, DataController.instance.gameData.foots, DataController.instance.gameData.foots);
        head_slider.value = DataController.instance.gameData.head;
        body_slider.value = DataController.instance.gameData.body;
        arms_slider.value = DataController.instance.gameData.arms;
        hands_slider.value = DataController.instance.gameData.hands;
        legs_slider.value = DataController.instance.gameData.legs;
        foots_slider.value = DataController.instance.gameData.foots;


        /*
         * 시작시 게임 오브젝트 비활성화후 저장된 오브젝트만 활성화
         */

        Raft.SetActive(false); // 뗏목 오브젝트
        Sos_sign.SetActive(false); // SOS 사인 오브젝트
        if (DataController.instance._gameData.Raft == true)
        {
            Raft.SetActive(true); // 게임시작시 뗏목을 획득한 상태라면 뗏목 오브젝트 활성화
            Raft_button.SetActive(false);
        }
        if (DataController.instance._gameData.Sos_sign == true)
        {
            Sos_sign.SetActive(true); // 게임 시작시 SOS사인 오브젝트를 획득한 상태라면 오브젝트 활성화
            Sos_sign_button.SetActive(true);
        }

        /*
         시작시 플레이어 설정
         */
        attack_col.enabled = false; // 플레이어 공격 비활성화
        damaged = false; // 피격 피활성화
        StartCoroutine("Lose_hungry"); // 일정시간마다 배고픔이 감소한다
        animator = GetComponent<Animator>();
        weapon = State.none;
        Weapon[0].SetActive(false); // 무기 비활성화
        Weapon[1].SetActive(false);// 무기 비활성화
        Weapon[2].SetActive(false);// 무기 비활성화

        damage_text.text = string.Format("Damage : {0}", DataController.instance.gameData.player_Pow);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            transform.position += new Vector3(0, 5, 0); // 캐릭터가 맵에 끼일경우 탈출
        }

        if (Input.GetKeyDown(KeyCode.Q) || Input.GetMouseButtonDown(1))
        {
            Attack(); // Q 또는 마우스 우클릭으로 공격
        }


        if (Input.GetKey(KeyCode.C)) // C키를 꾹 누르면 생고기를 익힌고기로 변환한다.
        {
            if (Infire == true)
            {
                if (DataController.instance.gameData.meat_count > 0) // 생고기의 개수가 0보다 클경우
                {
                    if (cook_complete == false)
                    {
                        animator.SetBool("Roast", true);
                        cooking.SetActive(true);
                        cook_maker.value = cook_make_time;
                        cook_make_time -= Time.deltaTime;

                        if (cook_make_time < 0f)
                        {
                            cook_complete = true;
                            DataController.instance.gameData.meat_count -= 1;
                            DataController.instance.gameData.ripe_meat_count += 1;

                            cooking.SetActive(false);
                            animator.SetBool("Roast", false);
                        }
                    }
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            cook_complete = false;
            cooking.SetActive(false);
            cook_make_time = cook_maxtime;
            animator.SetBool("Roast", false);
        }

        if (Input.GetKeyDown(KeyCode.G)) // 게임 데이터 저장 ( PC모드용)
        {
            DataController.instance.SaveGameData();
        }

        if (Input.GetKeyDown(KeyCode.X)) // 캐릭터 커스터마이징창 활성화/비활성화
        {
            Customiz_button();

        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) // 빠른 시연을 위한 아이템획득 치트키
        {
            DataController.instance.Cheatkey();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2)) // 빠른 시연을 위한 아이템획득 치트키
        {
            DataController.instance.Cheatpet();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // 빠른 시연을 위한 아이템획득 치트키
        {
            DataController.instance.gameData.Day += 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            transform.position = to_island_portal.transform.position + new Vector3(0, 0, 5f); // 플레이어의 위치를 동굴의 위치로 이동한다            
            this.gameObject.GetComponent<PlayerInCave>().playerPos = PlayerInCave.State.InCave; // 플레이어의 상태를 동굴상태로 전환
            Lighting.SetActive(false); // 빛 오브젝트 비활성화
            Game_Manager.instance.In_island = false; // '섬'에서 '동굴'상태로 전환
            Game_Manager.instance.In_cave = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            transform.position = to_cave_portal.transform.position + new Vector3(0, 0, -5f); // 플레이어의 위치를 섬의 위치로 이동한다
            this.gameObject.GetComponent<PlayerInCave>().playerPos = PlayerInCave.State.InIsland; // 플레이어의 상태를 섬 상태로 전환
            Lighting.SetActive(true); // 빛 오브젝트 활성화
            Game_Manager.instance.In_cave = false;
            Game_Manager.instance.In_island = true;
        }


        NoHome();
        Customizing();

        if (DataController.instance.gameData.player_Hp <= 0 || DataController.instance.gameData.player_Hungry <= 0f) // 플레이어 사망할 경우의 조건문
        {
            if (Isdead == false)
            {
                Isdead = true;
                StartCoroutine(Dead());
            }
        }
        DataController.instance.gameData.player_MaxHp = (90 + 10 * DataController.instance.gameData.player_Level) * DataController.instance.gameData.Health_Up;
        HP_text.text = string.Format("HP : {0} / {1}", Mathf.Floor(DataController.instance.gameData.player_Hp), DataController.instance.gameData.player_MaxHp); // 실시간 체력UI표시
        HP_slider.value = DataController.instance.gameData.player_Hp / DataController.instance.gameData.player_MaxHp;// 실시간 체력UI표시
        Quick_item[0].text = string.Format("{0}", DataController.instance.gameData.meat_count);
        Quick_item[1].text = string.Format("{0}", DataController.instance.gameData.tree_count);
        ripemeat_text.text = string.Format("{0}", DataController.instance.gameData.ripe_meat_count);
    }

    public void Attack()
    {
        /*
         * 공격판정 1. 충돌판정
         * 동물,몬스터 공격관련.
         */
        if (attack_col.enabled == false)
        {
            DataController.instance.gameData.player_Pow = 8 + 2 * DataController.instance.gameData.player_Level * (1 + DataController.instance._gameData.Damage_Up); // 공격시 아이템에 따라 공격력 재설정
            damage_text.text = string.Format("Damage : {0}", DataController.instance.gameData.player_Pow);

            if (weapon == State.sword)
            {
                DataController.instance.gameData.player_Pow = (8 + 2 * DataController.instance.gameData.player_Level * (1 + DataController.instance._gameData.Damage_Up)) * 2;
               
            }
            /*
             * 공격시 공격콜라이더 활성화 / 공격 사운드 재생 / 공격대기시간 코루틴 시작
             */
            attack_col.enabled = true; // 공격 콜라이더 활성화
            animator.SetBool("Attack", true); //  공격 애니메이션 
            Musicplayer.clip = attack; // 공격 사운드 클립
            Musicplayer.Play(); // 사운드 재생
            StartCoroutine("Reload"); // 공격 대기시간 함수 실행

            /*
             * 공격판정 2. 레이캐스트(히트스캔)판정
             * 채집 관련. 닿은 오브젝트의 태그를 비교하여 아이템을 생성
             */
            RaycastHit hitInfo;
            Vector3 ray_position = transform.position + new Vector3(0, 0.5f, 0);
            if (Physics.Raycast(ray_position, transform.forward, out hitInfo, 3)) // 플레이어가 바라보는 방향으로 레이캐스트 발사
            {
                hitobject = hitInfo.transform.gameObject; // 레이캐스트에 닿은 오브젝트를 변수에 저장

                if (hitobject.CompareTag("tree_obj")) // 닿은 오브젝트가 나무일 경우
                {
                    Instantiate(tree_item[Random.Range(0, 3)], hitobject.transform.position + new Vector3(0, 3, 0), transform.rotation); // 나무에서 얻을 수 있는 아이템을 랜덤하게 생성
                    Take_exp(10); // 플레이어에게 경험치 제공
                    if (weapon == State.axe) // 현재 착용무기가 도끼일경우 나무에서 나오는 아이템을 2개 생성
                    {
                        Instantiate(tree_item[Random.Range(0, 3)], hitobject.transform.position + new Vector3(0, 3, 0), transform.rotation);
                        Take_exp(10); // 플레이어에게 경험치 제공
                    }

                    hitobject.SetActive(false); // 한번 채집한 나무는 사라짐. 일정시간이 지나면 재생성.
                }

                if (hitobject.CompareTag("rock")) // 닿은 오브젝트가 돌 일 경우
                {
                    Musicplayer.clip = rock_crash;
                    Musicplayer.Play();
                    Instantiate(stone_item, hitobject.transform.position + new Vector3(0, 3, 0), transform.rotation);
                    Take_exp(10); // 경험치
                    if (weapon == State.pickaxe) // 현재 착용무기가 곡괭이일 경우 돌에서 나오는 아이템을 2개 생성
                    {
                        Instantiate(stone_item, hitobject.transform.position + new Vector3(0, 3, 0), transform.rotation);
                        Take_exp(10);// 경험치
                    }
                    hitobject.SetActive(false); // 채집한 돌은 사라짐. 일정시간이 지나면 재생성
                }
            }

        }
    }

    IEnumerator Reload() // 공격 대기시간 코루틴
    {
        yield return new WaitForSeconds(1f); // 공격시간 1초로 변경
        attack_col.enabled = false; // 공격 비활성화
        animator.SetBool("Attack", false); // 공격애니메이션 비활성화

    }

    public void Take_exp(float exp) // 경험치 획득함수 , 경험치가 일정량 쌓이면 플레이어의 레벨이 오르고 공격력과 체력이 증가한다
    {
        DataController.instance.gameData.player_Exp += exp; // 경험치 획득
        if (DataController.instance.gameData.player_Exp >= DataController.instance.gameData.player_MaxExp) // 현재 경험치가 최대 경험치를 넘을경우
        {
            DataController.instance.gameData.player_Exp -= DataController.instance.gameData.player_MaxExp; // 현재경험치에서 최대경험치값을 차등한다
            DataController.instance.gameData.player_Level += 1; // 플레이어 레벨을 1 상승시킨다
            Level_text.text = string.Format("Level : {0}", DataController.instance.gameData.player_Level); // 레벨 UI표시

        }

        Exp_text.text = string.Format("Exp : {0} / {1}", DataController.instance.gameData.player_Exp, DataController.instance.gameData.player_MaxExp); // 경험치 UI
        Exp_slider.value = DataController.instance.gameData.player_Exp / DataController.instance.gameData.player_MaxExp; // 경험치 UI
    }

    public void Take_Heal(float val) // 체력회복 함수, 회복 아이템을 사용할시 체력회복.
    {
        DataController.instance.gameData.player_Hp += val; // 체력 증가
        if (DataController.instance.gameData.player_Hp >= DataController.instance.gameData.player_MaxHp) // 현재 체력이 최대 체력을 넘을경우
        {
            DataController.instance.gameData.player_Hp = DataController.instance.gameData.player_MaxHp; // 현재체력을 최대체력값으로 설정한다
        }
    }

    public void Lose_Health(float val) // 몬스터나 동물에게 공격을 당할경우 호출
    {
        if (damaged == false) // 이미 피격상태가 아닌지 판단
        {
            damaged = true; // 피격상태 설정
            DataController.instance.gameData.player_Hp -= val; //  체력 감소
            StartCoroutine("Rehit"); // 피격 대기시간 함수 호출
        }

    }

    IEnumerator Rehit() // 몬스터나 동물에게 여러번 맞는것을 방지하기위해 1초에 한대 맞도록 대기시간 함수설정
    {
        yield return new WaitForSeconds(0.5f);
        damaged = false;
    }

    public IEnumerator Dead() // 플레이어가 죽을경우 호출되는 함수 , 죽는 애니메이션과 사운드가 출력되고 3초후 '죽음'엔딩씬으로 전환
    {
        Musicplayer.clip = dead;
        Musicplayer.Play();

        animator.SetBool("Dead", true); // 죽음 애니메이션 실행

        yield return new WaitForSeconds(3f); // 3초후 엔딩씬으로 전환
        SceneManager.LoadScene("dead_ending");

    }

    public void OnControllerColliderHit(ControllerColliderHit hit) // 캐릭터의 충돌판별 함수. 돌,나무,나뭇잎,로프,고기 등의 태그를 판별하고 아이템을 획득 하거나 엔딩씬으로 이동
    {


        if (hit.transform.CompareTag("stone")) // 닿은 오브젝트가 돌 아이템일 경우
        {
            Musicplayer.clip = item;
            Musicplayer.Play();
            DataController.instance.gameData.stone_count += 1;
            Destroy(hit.gameObject);
        }
        if (hit.transform.CompareTag("leaf")) // 닿은 오브젝트가 나뭇잎 아이템일 경우
        {
            Musicplayer.clip = item;
            Musicplayer.Play();
            DataController.instance.gameData.leaf_count += 1;
            Destroy(hit.gameObject);
        }
        if (hit.transform.CompareTag("tree")) // 닿은 오브젝트가 나무 아이템일 경우
        {
            Musicplayer.clip = item;
            Musicplayer.Play();
            DataController.instance.gameData.tree_count += 1;
            Destroy(hit.gameObject);
        }

        if (hit.transform.CompareTag("Raft")) // 닿은 오브젝트가 뗏목 아이템일 경우
        {
            Musicplayer.clip = item;
            Musicplayer.Play();
            SceneManager.LoadScene("Escape_ending");
        }

        if (hit.transform.CompareTag("rope")) // 닿은 오브젝트가 로프 아이템일 경우
        {
            Musicplayer.clip = item;
            Musicplayer.Play();
            DataController.instance._gameData.rope_count += 1;
            hit.gameObject.SetActive(false);
        }
        if (hit.transform.CompareTag("meat")) // 닿은 오브젝트가 생고기 아이템일 경우
        {
            Musicplayer.clip = item;
            Musicplayer.Play();
            DataController.instance._gameData.meat_count += 1;
            Destroy(hit.gameObject);
        }
        if (hit.transform.CompareTag("bone")) // 닿은 오브젝트가 뼈 아이템일 경우
        {
            Musicplayer.clip = item;
            Musicplayer.Play();
            DataController.instance._gameData.bone_count += 1;
            Destroy(hit.gameObject);
        }


    }

    void OnTriggerEnter(Collider col) // 섬과 동굴을 이동하는 포탈 충돌함수
    {
        if (col.CompareTag("to_cave")) // 동굴로 이동하는 포탈에 닿은경우
        {
            transform.position = to_island_portal.transform.position + new Vector3(0, 0, 5f); // 플레이어의 위치를 동굴의 위치로 이동한다            
            this.gameObject.GetComponent<PlayerInCave>().playerPos = PlayerInCave.State.InCave; // 플레이어의 상태를 동굴상태로 전환
            Lighting.SetActive(false); // 빛 오브젝트 비활성화
            Game_Manager.instance.In_island = false; // '섬'에서 '동굴'상태로 전환
            Game_Manager.instance.In_cave = true;

        }
        if (col.CompareTag("to_island")) // 섬으로 이동하는 포탈에 닿은경우
        {
            transform.position = to_cave_portal.transform.position + new Vector3(0, 0, -5f); // 플레이어의 위치를 섬의 위치로 이동한다
            this.gameObject.GetComponent<PlayerInCave>().playerPos = PlayerInCave.State.InIsland; // 플레이어의 상태를 섬 상태로 전환
            Lighting.SetActive(true); // 빛 오브젝트 활성화
            Game_Manager.instance.In_cave = false;
            Game_Manager.instance.In_island = true;

        }

        if (col.CompareTag("fire"))
        {
            Infire = true;
        }
    }


    private void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("fire"))
        {
            Infire = false;
        }
    }




    public void Take_Hungry(float val) // 배고픔 회복함수
    {
        DataController.instance.gameData.player_Hungry += val; // 배고픔 획득
        if (DataController.instance.gameData.player_Hungry >= DataController.instance.gameData.player_MaxHungry) // 현재 배고픔값이 최대 배고픔값을 넘을경우
            DataController.instance.gameData.player_Hungry = DataController.instance.gameData.player_MaxHungry; // 현재 배고픔값을 최대배고픔 값으로 설정

        Hungry_text.text = string.Format("Hungry : {0} / {1}", Mathf.Floor(DataController.instance.gameData.player_Hungry), DataController.instance.gameData.player_MaxHungry); // 배고픔 UI
        Hungry_slider.value = DataController.instance.gameData.player_Hungry / DataController.instance.gameData.player_MaxHungry; // 배고픔 UI
    }

    public IEnumerator Lose_hungry() // 일정시간마다 배고픔이 줄어드는 함수. 0 이 되면 플레이어 사망
    {
        while (true)
        {
            DataController.instance.gameData.player_Hungry -= DataController.instance.gameData.hungry_val; // 배고픔 감소
            Hungry_text.text = string.Format("Hungry : {0} / {1}", Mathf.Floor(DataController.instance.gameData.player_Hungry), DataController.instance.gameData.player_MaxHungry);
            Hungry_slider.value = DataController.instance.gameData.player_Hungry / DataController.instance.gameData.player_MaxHungry;
            yield return new WaitForSeconds(1f); // 1초의 대기시간 
        }
    }

    public void Eat_ripeMeat_button()
    {
        Musicplayer.clip = eat;
        Musicplayer.Play();
        if (DataController.instance.gameData.ripe_meat_count >= 1)
        {
            DataController.instance.gameData.ripe_meat_count -= 1;
            Take_Hungry(30);
            Take_Heal(30);
        }
    }

    public void Eat_meat_button() // 배고픔회복 아이템사용 함수. / UI 버튼에 연결됨
    {
        Musicplayer.clip = eat;
        Musicplayer.Play();
        if (DataController.instance.gameData.meat_count >= 1)
        {
            DataController.instance.gameData.meat_count -= 1;
            Take_Hungry(10); // 배고픔 획득함수 호출
          
        }
    }

    public void Eat_fruit_button() // 체력회복 아이템 사용 함수. / UI 버튼에 연결됨
    {
        Musicplayer.clip = eat;
        Musicplayer.Play();
        if (DataController.instance.gameData.tree_count >= 1)
        {
            DataController.instance.gameData.tree_count -= 1;        
            Take_Heal(30);
        }
    }

    public void Customizing() // 플레이어 캐릭터의 크기를 마음대로 조절 할 수 있는 커스터마이징 함수.
    {
        if (status == true) // 커스터마이징 상태인지 판단
        {
            // 슬라이더 값을 조절하면 캐릭터의 해당 파츠 크기가 변경된다. 
            head.localScale = new Vector3(head_slider.value, head_slider.value, head_slider.value);
            body.localScale = new Vector3(body_slider.value, body_slider.value, body_slider.value);
            left_arm.localScale = new Vector3(arms_slider.value, arms_slider.value, arms_slider.value);
            right_arm.localScale = new Vector3(arms_slider.value, arms_slider.value, arms_slider.value);
            left_leg.localScale = new Vector3(legs_slider.value, legs_slider.value, legs_slider.value);
            right_leg.localScale = new Vector3(legs_slider.value, legs_slider.value, legs_slider.value);
            left_hand.localScale = new Vector3(hands_slider.value, hands_slider.value, hands_slider.value);
            right_hand.localScale = new Vector3(hands_slider.value, hands_slider.value, hands_slider.value);
            left_foot.localScale = new Vector3(foots_slider.value, foots_slider.value, foots_slider.value);
            right_foot.localScale = new Vector3(foots_slider.value, foots_slider.value, foots_slider.value);

            // 파츠별 크기값을 저장
            DataController.instance.gameData.head = head_slider.value;
            DataController.instance.gameData.body = body_slider.value;
            DataController.instance.gameData.arms = arms_slider.value;
            DataController.instance.gameData.hands = hands_slider.value;
            DataController.instance.gameData.legs = legs_slider.value;
            DataController.instance.gameData.foots = foots_slider.value;

        }
    }


    public void Create_Raft() // 섬을 탈출 할 수 있는 배를 만드는 함수. 일정량의 아이템을 필요로함
    {
        Musicplayer.clip = make;
        Musicplayer.Play();
        if (DataController.instance._gameData.big_tree_count >= 100) // 큰 나무가 100개 이상일경우
        {
            if (DataController.instance._gameData.rope_count >= 10) // 로프가 10개 이상일경우 뗏목 생성가능
            {
                DataController.instance._gameData.big_tree_count -= 100;
                DataController.instance._gameData.rope_count -= 10;
                Raft.SetActive(true); // 뗏목 오브젝트 활성화
                Raft_button.SetActive(false); // 뗏목 제작 버튼 비활성화
                DataController.instance._gameData.Raft = true; // 뗏목획득 데이터 저장
            }
            else
            {
                Notree.SetActive(true);
            }
        }
        else
        {
            Notree.SetActive(true);
        }
    }

    public void Create_Sossign() // 구조요청 SOS사인을 만드는 함수, 일정량의 아이템을 필요로함. 생성시 구조확률이 올라가며 게임상의 시간으로 7일 12시가 되는날에 구조확률에 따라 구조됨. (확률이기에 실패할 수 있음 )
    {
        Musicplayer.clip = make;
        Musicplayer.Play();

        if (DataController.instance._gameData.Sos_sign == false) // SOS 사인이 활성화 되어있지 않은경우
        {
            if (DataController.instance._gameData.stone_count >= 100) // 돌 100개 이상일경우
            {
                if (DataController.instance._gameData.tree_count >= 10) // 나무가 10개 이상일경우 SOS사인 제작
                {
                    DataController.instance._gameData.stone_count -= 100;
                    DataController.instance._gameData.tree_count -= 10;
                    Sos_sign.SetActive(true); // SOS 사인 오브젝트 활성화
                    DataController.instance._gameData.Sos_sign = true; // SOS 사인 획득
                    DataController.instance._gameData.Rescue_probability = 10; // 구조 확률 10 설정
                    Rescue_text.text = string.Format("{0}%", DataController.instance._gameData.Rescue_probability); //구조확률 UI
                }
                else
                {
                    Notree.SetActive(true);
                }
            }
            else
            {
                Notree.SetActive(true);
            }
        }
        else if (DataController.instance._gameData.Sos_sign == true) // SOS사인이 활성화 되어있을 경우
        {
            if (DataController.instance._gameData.tree_count >= 1) // 나무가 1개 이상일경우
            {
                DataController.instance._gameData.tree_count -= 1; // 나무 하나를 감소시키고
                DataController.instance._gameData.Rescue_probability += 1; // 구조확률을 1 증가시킨다
                Rescue_text.text = string.Format("{0}%", DataController.instance._gameData.Rescue_probability); // 구조확률UI
            }
            else
            {
                Notree.SetActive(true);
            }
        }
    }
    public void NoHome() // 섬에 온 지 1일 12시간이 지나도 집이 없다면 점점 체력을 잃음
    {//플레이어가 집이 없을때
        if (DataController.instance.gameData.house_0 == true)
        {
            if ((DataController.instance.gameData.Day >= 1) && (DataController.instance.gameData.Hour >= 12))
            {
                DataController.instance.gameData.player_Hp -= noHomeHP; // 일정시간마다 체력이 감소
            }
        }
    }

    public void Change_sword() // 검_무기  교체함수  / UI버튼에 연결됨
    {
        Musicplayer.clip = weapon_change;
        Musicplayer.Play();

        if (DataController.instance._gameData.stone_knife == true) // 검 아이템을 획득한 경우
        {
            animator.SetBool("pickaxe", false); // 곡괭이 애니메이션 비활성화
            weapon = State.sword; // 무기상태 검으로 변경
            Weapon[0].SetActive(true); // 검 아이템 활성화
            Weapon[1].SetActive(false); // 도끼 아이템 비활성화
            Weapon[2].SetActive(false); // 곡괭이 아이템 비활성화

            DataController.instance.gameData.player_Pow = (8 + 2 * DataController.instance.gameData.player_Level * (1 + DataController.instance._gameData.Damage_Up)) * 2;
            damage_text.text = string.Format("Damage : {0}", DataController.instance.gameData.player_Pow);
        }
    }

    public void Change_axe()// 도끼_무기  교체함수  / UI버튼에 연결됨
    {
        Musicplayer.clip = weapon_change;
        Musicplayer.Play();
        if (DataController.instance._gameData.ax == true) // 도끼 아이템을 획득한 경우
        {
            animator.SetBool("pickaxe", false);
            weapon = State.axe; // 무기상태를 도끼로 변경
            Weapon[0].SetActive(false); // 검 아이템 비활성화
            Weapon[1].SetActive(true); // 도끼 아이템 활성화
            Weapon[2].SetActive(false); // 곡괭이 아이템 비활성화
            DataController.instance.gameData.player_Pow = 8 + 2 * DataController.instance.gameData.player_Level * (1 + DataController.instance._gameData.Damage_Up); // 공격시 아이템에 따라 공격력 재설정
            damage_text.text = string.Format("Damage : {0}", DataController.instance.gameData.player_Pow);
        }
    }

    public void Change_pickaxe()// 곡괭이_무기  교체함수  / UI버튼에 연결됨
    {
        animator.SetBool("pickaxe", true);
        Musicplayer.clip = weapon_change;
        Musicplayer.Play();
        if (DataController.instance._gameData.pickax == true) // 곡괭이 아이템을 획득한 경우
        {
            weapon = State.pickaxe; // 무기 상태를 곡괭이로 변경
            Weapon[0].SetActive(false); // 검 아이템 비활성화
            Weapon[1].SetActive(false); // 도끼 아이템 비활성화
            Weapon[2].SetActive(true); // 곡괭이 아이템 활성화
            DataController.instance.gameData.player_Pow = 8 + 2 * DataController.instance.gameData.player_Level * (1 + DataController.instance._gameData.Damage_Up); // 공격시 아이템에 따라 공격력 재설정
            damage_text.text = string.Format("Damage : {0}", DataController.instance.gameData.player_Pow);
        }
    }

    public void Customiz_button() // 커스터마이징 버튼 연결
    {
        if (status == false)
        {
            status = true; // 커스터마이지 상태로 전환
            status_panel.SetActive(true); // 커스터마이징 창 활성화
            customizing_camera.enabled = true; // 커스터마이징 카메라 활성화
            main_cam.enabled = false; // 메인카메라 비활성화
        }
        else
        {
            status = false; // 커스터마이징이 아닌상태로 전환
            status_panel.SetActive(false); // 커스터마이징 창 비활성화
            main_cam.enabled = true; // 메인카메라 활성화
            customizing_camera.enabled = false; // 커스터마이징 카메라 비활성화
        }
    }

}
