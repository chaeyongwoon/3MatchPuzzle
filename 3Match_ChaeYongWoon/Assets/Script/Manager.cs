using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public enum State
    {
        Create,
        Down,
        Move,
        Wait,
        Check,
    }
    public State GmState;

    public GameObject blockprefab;           // 생성할 큐브 오브젝트의 프리팹
    int Width = 7, Height = 7;               // 블럭의 보드의 크기는 특정 모양제작을 위해 7*7 크기로 고정했습니다
    public Material[] mat;                  
    public int[,] Blockboard;



    private GameObject[] m_blocks;

    private Vector3 MouseStartPos;          // 클릭시 마우스 시작지점
    private Vector3 MouseEndPos;            // 드래그 끝난 지점
    private Vector3 MouseOffset;            // 마우스 이동거리 체크
    private Vector3 StartPos1, StartPos2;   // 첫번째 선택블럭 시작지점, 두번째 타겟블럭 시작지점
    private Vector3 EndPos1, EndPos2;       // 첫번째 선택블럭 이동지점, 두번째 타겟블럭 이동지점

    public GameObject SelectBlock;          // 클릭으로 선택된 첫번째 블럭
    public GameObject TargetBlock;          // 드래그로 선택된 두번째 블럭
 
    private bool bDrag = true;              // 드래그 가능확인 변수
    private bool bBlockReChange = false;    // 드래그로 블록 위치변경 후 리체크 변수
    private bool bReCheck = false;          // 블록 하단 이동 후 리체크 변수
    private bool iswall = false;
    private bool SpinCheck;                 // 팽이주변 체크확인 변수

    private float fMouseMoveDis = 30f;      // 마우스 이동거리 판정기준값
    private int count = 0;                  // 팽이 개수 카운트
    private int Type;                       // 블럭보드, 블럭 타입변수
    public int Score = 0;                   // 점수


    public Block tmpblock;                 // 팽이 주변 체크 임시블록 변수

    public Text[] text;                     // UI에서 해당위치의 보드(=블럭)값 표시
    public Text ScoreText;                  // 점수UI 텍스트


    private void Awake()
    {
        GmState = State.Create; // 블럭이 생성되도록 설정
        Blockboard = new int[Width, Height];

        for (int x = 0; x < Width; x++)             //보드 초기화
        {
            for (int y = 0; y < Height; y++)
            {
                Blockboard[x, y] = -1;             // 초기 빈 상태는 -1로 표시                
            }            
        }
                
        Blockboard[0, 0] = -2;                    // -2, -3 은 사용 안하는 벽을 표시                                                  
        Blockboard[0, 1] = -3;                    //매치판단은 보드의 값으로 판단하기 때문에 벽끼리 매치판단이 안되도록 체크패턴으로 값을 주었습니다
        Blockboard[0, 2] = -2;
        Blockboard[0, 4] = -3;
        Blockboard[0, 5] = -2;
        Blockboard[0, 6] = -3;
        Blockboard[1, 0] = -3;
        Blockboard[1, 1] = -2;
        Blockboard[1, 5] = -3;
        Blockboard[1, 6] = -2;
        Blockboard[2, 0] = -2;
        Blockboard[2, 6] = -3;
        Blockboard[4, 0] = -3;
        Blockboard[4, 6] = -2;
        Blockboard[5, 0] = -2;
        Blockboard[5, 1] = -3;
        Blockboard[5, 5] = -2;
        Blockboard[5, 6] = -3;
        Blockboard[6, 0] = -3;
        Blockboard[6, 1] = -2;
        Blockboard[6, 2] = -3;
        Blockboard[6, 4] = -2;
        Blockboard[6, 5] = -3;
        Blockboard[6, 6] = -2;
    }

    private void Update()
    {
        BlockMove();         // 블럭 하단이동 ( 하단, 좌측하단, 우측하단) 
        Createblock();       // 블럭 생성
        BlockCheck();        // 블럭(보드) 매치체크
        BlockDelete();       // 팽이 큐브 활성화 판단 + 매치된 블럭 삭제
        
        MouseClick();        // 마우스 클릭 처리
        DragToMoveBlock();   // 마우스로 블럭 이동

        TextOn();            // UI 블럭(=보드)값 표시
    }

    void Createblock()
    {
        if (GmState == State.Create)
        {
            GameObject block = Instantiate(blockprefab) as GameObject;
            Block sblock = block.GetComponent<Block>();


            if (count < 3)                                      // 팽이 큐브는 3개까지만 생성합니다
            {
                Type = Random.Range(0, mat.Length);
            }
            else
            {
                Type = Random.Range(0, mat.Length - 1);          // 팽이 큐브가 3개 생성되면 범위값을 줄여 '4'의 값인 팽이가 생성되지 않도록 했습니다
            }

            sblock._type = Type;
            sblock.GetComponent<Renderer>().material = mat[Type];

            int _x = (int)(Width / 2);                          // 블럭의 x위치는 정 중앙으로 설정했습니다. 후에 보드의 크기값을 변경할경우에도 그대로 사용할 수 있습니다 (x값은 홀수만) 

            Blockboard[_x, Height - 1] = sblock._type;          // 보드의 중앙 최상단
            sblock.SetPos(_x, Height - 1);
            sblock.mState = Block.State.Init;                   // 초기상태 설정
       

            if (Type == 4) // 블럭의 타입이 팽이일 경우
            {
                sblock.mState = Block.State.Stop;               // 팽이의 상태는 Stop으로 설정
                count += 1;                                     // 팽이 개수 카운트
            }

            GmState = State.Down;                               // 게임 매니저의 상태를 Down으로 변경시킨 후 블럭의 이동이 끝날때까지 새로 생성되지 않도록 합니다


        }
    }

    void BlockMove()
    {
        if (GmState == State.Down)                                     // 블럭이 생성 또는 삭제된 후 게임매니저가 이동상태일 경우
        {
            m_blocks = GameObject.FindGameObjectsWithTag("Block");     // "Block" 태그를 가진 모든 오브젝트를 담습니다 
            if (m_blocks == null)
            {
                return;
            }

            foreach (GameObject block in m_blocks)
            {
                Block sblock = block.GetComponent<Block>();                

                if (sblock.y == 0 || sblock.x == 0 || sblock.x == Width - 1) // 블럭의 위치가 끝 하단, 끝 좌측, 끝 우측일 경우
                {
                    GmState = State.Create;                                  // 매니저의 상태를 블럭 생성으로 변경
                    bReCheck = true;
                   

                }
                else if (Blockboard[sblock.x, sblock.y - 1] == -1)                       // 현재 블럭위치의 하단이 비어있는지 체크
                {
                    Blockboard[sblock.x, sblock.y - 1] = Blockboard[sblock.x, sblock.y]; // 하단 보드값을 자신의 보드값 변경
                    Blockboard[sblock.x, sblock.y] = -1;                                 // 자신의 기존 보드값 -1(빈공간)으로 변경
                    sblock.SetPos(sblock.x, sblock.y - 1);                               // 블럭 위치 변경
                    GmState = State.Down;                                                // 다시 이동판단을 하기위해 매니저 상태는 Down으로 설정 
                  


                }
                else if (Blockboard[sblock.x - 1, sblock.y - 1] == -1)              // 좌측 하단이 비어있는지 체크
                {
                    Blockboard[sblock.x - 1, sblock.y - 1] = Blockboard[sblock.x, sblock.y];
                    Blockboard[sblock.x, sblock.y] = -1;
                    sblock.SetPos(sblock.x - 1, sblock.y - 1);
                    GmState = State.Down;
             
                }

                else if (Blockboard[sblock.x + 1, sblock.y - 1] == -1)              // 우측 하단이 비어있는지 체크
                {
                    Blockboard[sblock.x + 1, sblock.y - 1] = Blockboard[sblock.x, sblock.y];
                    Blockboard[sblock.x, sblock.y] = -1;
                    sblock.SetPos(sblock.x + 1, sblock.y - 1);
                    GmState = State.Down;
         
                }
                else if (sblock.y == Height - 1)                                    // 위의 이동판단을 거친 후 블럭이 정 중앙 최상단 위치에 있을경우 
                {                                                                   // ( 가장 마지막에 생성된 블럭이며 이 위치에 블럭이 차면 모든 칸에 블럭이 찬 상태
                    GmState = State.Wait;                                           // 매니저의 상태를 Wait 대기상태로 변경
           
                }

                else
                { bReCheck = true; }
            }
        }
    }

    void BlockCheck()
    {
        if ((GmState == State.Wait) || (GmState == State.Check))                    // 매니저의 상태가 wait 이나 check 일 경우만 실행
        {
                                                                                // 가로 매치블럭 판단
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width - 2; x++)                             // for 문 안에서 x +2 값 까지 찾기때문에 전체 가로크기 WIdth 보다 2 작게 설정
                {
                    if (Blockboard[x, y] != 4)                                  // 팽이 블럭이 아닐경우만 매치판단
                    {
                        if (Blockboard[x, y] == Blockboard[x + 1, y])           // 현재 보드의 값이 오른쪽 보드와 같은지 판단
                        {
                            if (Blockboard[x, y] == Blockboard[x + 2, y])       // 현재 보드의 값이 오른쪽 + 오른쪽 보드와 같은지 판단
                            {
                                GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

                                foreach (GameObject block in blocks)
                                {
                                    Block sblock = block.GetComponent<Block>();

                                    if (sblock.y != y)
                                        continue;

                                    if (sblock.x == x && sblock.y == y)          // 보드의 x,y 위치와 같은 블럭을 찾아서 상태를 Match 상태로 변경
                                    {
                                        sblock.mState = Block.State.Match;
                                        continue;
                                    }
                                    if (sblock.x == x + 1 && sblock.y == y)     // 오른쪽 위치에 있던 블럭을 찾아서 상태를 Match 상태로 변경
                                    {
                                        sblock.mState = Block.State.Match;
                                        continue;
                                    }
                                    if (sblock.x == x + 2 && sblock.y == y)     // 오른쪽+오른쪽 위치에 있던 블럭을 찾아서 상태를 Match 상태로 변경
                                    {
                                        sblock.mState = Block.State.Match;
                                    }


                                }
                            }
                        }
                    }
                }
            }

            for (int x = 0; x < Width; x++)                                                   // 세로 매치블럭 판단
            {
                for (int y = 0; y < Height - 2; y++)                                          // for문 안에서 y+2값 까지 찾기때문에 전체높이 Height보다 2 작게 설정
                {
                    if (Blockboard[x, y] != 4)                                                // 팽이 큐브가 아닐경우만 매치판단
                    {
                        if (Blockboard[x, y] == Blockboard[x, y + 1])                         // 현재 보드값이 위쪽 보드와 같은지 판단
                        {
                            if (Blockboard[x, y] == Blockboard[x, y + 2])                     // 현재 보드값이 위쪽 + 위쪽 보드와 같은지 판단
                            {
                                GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

                                foreach (GameObject block in blocks)
                                {
                                    Block sblock = block.GetComponent<Block>();

                                    if (sblock.x != x)
                                        continue;

                                    if (sblock.x == x && sblock.y == y)                        // 보드의 x,y 위치와 같은 블럭을 찾아서 상태를 Match 상태로 변경
                                    {
                                        sblock.mState = Block.State.Match;
                                        continue;
                                    }
                                    if (sblock.x == x && sblock.y == y + 1)                    // 위쪽 위치에 있던 블럭을 찾아서 상태를 Match 상태로 변경
                                    {
                                        sblock.mState = Block.State.Match;
                                        continue;
                                    }
                                    if (sblock.x == x && sblock.y == y + 2)                    // 위쪽 +위쪽 위치에 있던 블럭을 찾아서 상태를 Match 상태로 변경
                                    {
                                        sblock.mState = Block.State.Match;

                                    }

                                }
                            }
                        }
                    }
                }
            }

            if (!MatchCheck())                              // 블럭의 상태가 Match 상태로 변경된 블럭이 있는지 판단
            {                                               // 매치된게 없을경우

                if (bBlockReChange)                         // 드래그로 위치변경 후 매치가 안됐을경우 true
                {
                    Vector3 TmpStartPos = StartPos1;            // 두개의 위치값을 스왑 해준다
                    StartPos1 = StartPos2;
                    StartPos2 = TmpStartPos;

                    Vector3 TmpEndPos = EndPos1;
                    EndPos1 = EndPos2;
                    EndPos2 = TmpEndPos;

                    ReChangeBlock();                            // 블럭 원래위치로 다시 이동
                    bBlockReChange = false;


                    SelectBlock = null;
                    TargetBlock = null;

                    bDrag = true;
                    GmState = State.Wait;
                }

                if (bReCheck)
                {
                    bReCheck = false;
                    bDrag = true;
                }

            }
            else
            {                                                   // 매치된게 있을경우

                SelectBlock = null;                             // 첫번째 선택블록(SelectBlock) 과 두번째 선택블록(TargetBlock)을 담은 변수 초기화
                TargetBlock = null;

            }
        }
    }

    bool MatchCheck()                                           // 블럭의 상태가 Match 상태로 변경된 블럭이 있는지 판단
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject block in blocks)
        {
            if (block.GetComponent<Block>().mState == Block.State.Match)
                return true;
        }

        return false;
    }

    void BlockDelete()                      // 블럭 삭제 함수
    {
        if ((GmState == State.Wait) || (GmState == State.Check))
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    if (Blockboard[x, y] == 4)                                              // 보드의 값이 팽이 블럭일 경우
                    {
                        string tmpname = string.Format("({0},{1})", x, y);
                        tmpblock = GameObject.Find(tmpname).GetComponent<Block>();          // 팽이 블럭의 이름을 찾아 임시블럭에 할당
                        SpinCheck = false;                                                  // 팽이 블럭이 매치된블럭에 둘러 싸여있을경우 한번에 연속으로 활성화 되지 않도록 변수설정

                        GameObject[] blocks1 = GameObject.FindGameObjectsWithTag("Block");

                        foreach (GameObject block1 in blocks1)
                        {
                            Block sblock1 = block1.GetComponent<Block>();
                            
                            if (sblock1.x == x + 1 && sblock1.y == y)                           // 팽이 블럭의 오른쪽블럭 판단
                            {
                                if (sblock1.mState == Block.State.Match)                        // 그 블럭의 상태가 Match인지 판단
                                {
                                    if ((SpinCheck == false) && (tmpblock.mState == Block.State.Stop))  // 팽이블럭이 Stop 상태이면 
                                    {                                                       
                                        tmpblock.mState = Block.State.Spin;                             // Spin 상태로 변경
                                        SpinCheck = true;                                               // 연속으로 변경되지 않도록 true 설정.
                                    }
                                    else if ((SpinCheck == false) && (tmpblock.mState == Block.State.Spin)) // 팽이블럭이 Spin 상태이면
                                    {
                                        tmpblock.mState = Block.State.Match;                                // 팽이블럭을 Match 상태로 변경. 이후에 다른 블럭들과 함께 삭제
                                        SpinCheck = true;
                                    }
                                }
                            }

                            if (sblock1.x == x - 1 && sblock1.y == y)                            // 팽이 블럭의 왼쪽블럭 판단
                            {
                                if (sblock1.mState == Block.State.Match)
                                {
                                    if ((SpinCheck == false) && (tmpblock.mState == Block.State.Stop))
                                    {
                                        tmpblock.mState = Block.State.Spin;
                                        SpinCheck = true;
                                    }
                                    else if ((SpinCheck == false) && (tmpblock.mState == Block.State.Spin))
                                    {
                                        tmpblock.mState = Block.State.Match;
                                        SpinCheck = true;
                                    }
                                }
                            }

                            if (sblock1.x == x && sblock1.y == y + 1)                            // 팽이 블럭의 위쪽블럭 판단
                            {
                                if (sblock1.mState == Block.State.Match)
                                {
                                    if ((SpinCheck == false) && (tmpblock.mState == Block.State.Stop))
                                    {
                                        tmpblock.mState = Block.State.Spin;
                                        SpinCheck = true;
                                    }
                                    else if ((SpinCheck == false) && (tmpblock.mState == Block.State.Spin))
                                    {
                                        tmpblock.mState = Block.State.Match;
                                        SpinCheck = true;
                                    }
                                }
                            }

                            if (sblock1.x == x && sblock1.y == y - 1)                             // 팽이 블럭의 아래쪽블럭 판단
                            {
                                if (sblock1.mState == Block.State.Match)
                                {
                                    if ((SpinCheck == false) && (tmpblock.mState == Block.State.Stop))
                                    {
                                        tmpblock.mState = Block.State.Spin;
                                        SpinCheck = true;
                                    }
                                    else if ((SpinCheck == false) && (tmpblock.mState == Block.State.Spin))
                                    {
                                        tmpblock.mState = Block.State.Match;
                                        SpinCheck = true;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            SpinCheck = true;






            GameObject[] blocks2 = GameObject.FindGameObjectsWithTag("Block");

            foreach (GameObject block2 in blocks2)
            {
                Block sblock2 = block2.GetComponent<Block>();           // 모든 블럭을 할당

                if (sblock2.mState == Block.State.Match)                // 블럭의 상태가 Match 상태인지 판단
                {
                    Blockboard[sblock2.x, sblock2.y] = -1;              // 해당 보드의 값을 -1(빈공간)으로 변경
                    Destroy(block2);                                    // 블럭 삭제
                    Score += 1;                                         // Score 1상승
                    GmState = State.Down;                               // 매니저를 블럭 이동상태로 변경
                    bBlockReChange = false;                             // 다시 위치이동 판단하는 변수  false 설정


                }
            }
        }
    }

    void MouseClick()
    {
        if (GmState == State.Wait || GmState == State.Move)
        {
            if (Input.GetMouseButtonDown(0))                                  // 좌클릭 눌렀을때 상태
            {
                GmState = State.Move;

                RaycastHit Hit;
                MouseStartPos = Input.mousePosition;
                Ray ray = Camera.main.ScreenPointToRay(MouseStartPos);

                if (Physics.Raycast(ray, out Hit, Mathf.Infinity))
                {
                    if (bDrag && Hit.collider.CompareTag("Block"))              // 닿은 오브젝트의 태그가 "Block"이면
                    {
                        SelectBlock = Hit.collider.gameObject;                  // 해당 오브젝트를 SelectBlock 으로 설정
                        StartPos1 = SelectBlock.transform.position;
                    }

                }
            }

            if (Input.GetMouseButton(0))                                        // 좌클릭 드래그 상태
            {
                MouseEndPos = Input.mousePosition;
                MouseOffset = MouseStartPos - MouseEndPos;

                if (bDrag && SelectBlock != null)                               // 드래그가 안되는 상태거나 선택된 블럭이 없는 상태인지 판단
                {
                    if (MouseOffset.x > fMouseMoveDis)                          // 마우스의 x좌표 이동거리가 일정거리 보다 클경우 ( fMouseMoveDis 의 값은 현재 30f 으로 설정 )
                    {
                        if (SelectBlock.transform.position.x > 0)               // 드래그 방향 판단 -> 좌측으로 드래그
                            MouseDirection(-1, 0);                          
                    }
                    if (MouseOffset.x < -fMouseMoveDis)
                    {
                        if (SelectBlock.transform.position.x < Width - 1)       // 드래그 방향 판단 -> 우측으로 드래그
                            MouseDirection(1, 0);
                    }
                    if (MouseOffset.y < -fMouseMoveDis)
                    {
                        if (SelectBlock.transform.position.x < Height - 1)      // 드래그 방향 판단 -> 상단으로 드래그
                            MouseDirection(0, 1);
                    }
                    if (MouseOffset.y > fMouseMoveDis)                          // 드래그 방향 판단 -> 하단으로 드래그
                    {
                        if (SelectBlock.transform.position.y > 0)
                            MouseDirection(0, -1);
                    }

                }
            }

            if (Input.GetMouseButtonUp(0))                            // 좌클릭을 떼었을 경우
            {
                GmState = State.Check;                                // 매니저 상태를 Check 상태로 변경

                if ((SelectBlock == null) || (TargetBlock == null))   // 비교할 블럭 2개중 하나라도 비어있을 경우
                {
                    GmState = State.Wait;
                }


                if (iswall)                                 // 마우스 드래그시 벽을 드래그했을경우
                {
                    bBlockReChange = false;                 // 위치 재이동 판단 false
                    
                    SelectBlock = null;                     //SelectBlock  null 설정
                    TargetBlock = null;                     //TargetBlock  null 설정 , 애초에 벽쪽으로 드래그하여 오브젝트가 할당이 안되었을테지만 같이 초기화

                    bDrag = true;                           // 다시 드래그 가능하도록 설정
                    GmState = State.Wait;
                    iswall = false;                         // 벽을 드래그 판별변수 false 설정

                }

            }
        }
    }

    void MouseDirection(float _x, float _y)
    {
        EndPos1 = new Vector3(StartPos1.x + _x, StartPos1.y + _y, 0);       // 첫번째 블록이 이동할 위치값 = TargetBlock의 위치값

        bDrag = false;

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");

        foreach (GameObject block in blocks)
        {
            Block sblock = block.GetComponent<Block>();

            if (sblock.x == EndPos1.x && sblock.y == EndPos1.y)
            {
                TargetBlock = block;                              // 타겟블럭 설정
            }
            else if ((Blockboard[(int)EndPos1.x, (int)EndPos1.y] == -2) || (Blockboard[(int)EndPos1.x, (int)EndPos1.y] == -3)) // 선택된 위치의 보드값이 -2, -3일경우 (벽) 
            {
                iswall = true;                                    // 벽 드래그 판단변수 true
                return;
            }

        }
        StartPos2 = EndPos1;                                     // 두번째블럭의 시작 위치값 = 첫번째 블록이 이동할 위치값 
        EndPos2 = StartPos1;                                     // 두번째블럭이 이동할 위치값 = 첫번째 블록의 시작 위치값
    }
    void DragToMoveBlock()                                       // 블럭 드래그 이동 함수
    {
        if (GmState == State.Move)
        {
            if (TargetBlock == null)
                return;


            if (TargetBlock.transform.position != StartPos1)        // 두번째블럭(TargetBlock)의 위치가 최종위치인지 판단
            {
                SelectBlock.transform.position = EndPos1;           // 첫번째블럭(SelectBlock) 위치 이동
                TargetBlock.transform.position = EndPos2;           // 두번째블럭(TargetBlock) 위치 이동
                bBlockReChange = true;                              // 블럭 체인지 판단변수 true 설정. 이후 매치판단을 하고 매치되는게 없다면 다시 원래 위치로 이동하게합니다
                SwitchBoard();                                      // 보드 값 변경함수 호출
            }
           

        }
    }


    void SwitchBoard()                                      // 블럭이 아닌 보드의 값 스왑함수 / ( 블럭과 보드의 값이 동시에 바뀌지않고 블럭 먼저 이동 후 보드의 값이 변경됩니다.) 
    {
        Block sBlock = SelectBlock.GetComponent<Block>();
        sBlock.x = (int)EndPos1.x;                          // 첫번째 블록의 최종위치 x
        sBlock.y = (int)EndPos1.y;                          // 첫번째 블록의 최종위치 y


        sBlock = TargetBlock.GetComponent<Block>();
        sBlock.x = (int)EndPos2.x;                          // 두번째 블럭의 최종위치 x
        sBlock.y = (int)EndPos2.y;                          // 두번째 블럭의 최종위치 y

        int Tmptype = Blockboard[(int)StartPos1.x, (int)StartPos1.y];                                    // 첫번째 블럭과 두번째 블럭의 type값 스왑
        Blockboard[(int)StartPos1.x, (int)StartPos1.y] = Blockboard[(int)EndPos1.x, (int)EndPos1.y];
        Blockboard[(int)EndPos1.x, (int)EndPos1.y] = Tmptype;
    }


    void ReChangeBlock()                                            // 매치되는게 없을경우 블럭을 다시 원래위치로 변경하는 함수
    {
        if (GmState == State.Check)
        {
            if (bBlockReChange && TargetBlock)                      
            {
                if (TargetBlock.transform.position != EndPos2)
                {
                    SelectBlock.transform.position = EndPos1;       // 서로 이동된 첫번째블럭과 두번째블럭을 다시 이동합니다
                    TargetBlock.transform.position = EndPos2;

                    SwitchBoard();                                  // 보드의 값도 다시 바꿔줍니다
                }
                else
                {
                    bBlockReChange = false;

                    SelectBlock = null;
                    TargetBlock = null;

                    bDrag = true;
                }
            }
        }
    }

    void TextOn()       // 보드(=블럭)의 값과 Score를 UI로 표시해줍니다
    {
        text[0].text = string.Format("{0}", Blockboard[3, 0]);
        text[1].text = string.Format("{0}", Blockboard[2, 1]);
        text[2].text = string.Format("{0}", Blockboard[3, 1]);
        text[3].text = string.Format("{0}", Blockboard[4, 1]);
        text[4].text = string.Format("{0}", Blockboard[1, 2]);
        text[5].text = string.Format("{0}", Blockboard[2, 2]);
        text[6].text = string.Format("{0}", Blockboard[3, 2]);
        text[7].text = string.Format("{0}", Blockboard[4, 2]);
        text[8].text = string.Format("{0}", Blockboard[5, 2]);
        text[9].text = string.Format("{0}", Blockboard[0, 3]);
        text[10].text = string.Format("{0}", Blockboard[1, 3]);
        text[11].text = string.Format("{0}", Blockboard[2, 3]);
        text[12].text = string.Format("{0}", Blockboard[3, 3]);
        text[13].text = string.Format("{0}", Blockboard[4, 3]);
        text[14].text = string.Format("{0}", Blockboard[5, 3]);
        text[15].text = string.Format("{0}", Blockboard[6, 3]);
        text[16].text = string.Format("{0}", Blockboard[1, 4]);
        text[17].text = string.Format("{0}", Blockboard[2, 4]);
        text[18].text = string.Format("{0}", Blockboard[3, 4]);
        text[19].text = string.Format("{0}", Blockboard[4, 4]);
        text[20].text = string.Format("{0}", Blockboard[5, 4]);
        text[21].text = string.Format("{0}", Blockboard[2, 5]);
        text[22].text = string.Format("{0}", Blockboard[3, 5]);
        text[23].text = string.Format("{0}", Blockboard[4, 5]);
        text[24].text = string.Format("{0}", Blockboard[3, 6]);

        ScoreText.text = string.Format("Score : {0}", Score); // 
    }


    public void Reset()
    {
        SceneManager.LoadScene("Main");
    }

    public void GameQuit()
    {
        Application.Quit();
    }


}
