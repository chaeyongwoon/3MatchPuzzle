using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public enum State
    {
        Init,               // 일반블럭 초기 상태
        Stop,               // 팽이블럭 초기상태
        Spin,               // 팽이블럭 스핀 활성화 상태
        Match               // 일반블럭,팽이블럭 매치 상태
    }
    public State mState;

    public int _type;       // 블럭의 타입 (0~3은 일반 블럭, 4 는 팽이블럭)
    public int x, y;

  

    public void SetPos(int _x, int _y)
    {
        transform.position = new Vector3(_x, _y, 0);
        x = _x;
        y = _y;
    }


    private void Update()
    {
        if (_type == 4)                  // 팽이 블럭일 경우
        {
            if (mState == State.Spin)   // 상태가 Spin 상태일경우
            {
                transform.Rotate(new Vector3(0, 0, 300f * Time.deltaTime)); // 블럭 오브젝트를 회전시킨다
            }
        }

        x = (int)transform.position.x;
        y = (int)transform.position.y;
        transform.name = string.Format("({0},{1})",x,y); // 블럭의 이름을 현재 위치값으로 설정. 팽이블럭의 활성화 판단에서 오브젝트를 찾기위해 사용됩니다.

    }          
}
