using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_smooth_follow : MonoBehaviour {


    public GameObject Player;
    public float Speed = 4f;
    private Vector3 target_pos;
    private float CameraZ = -10;

	// Use this for initialization
	void Update () {
        target_pos = new Vector3(           // 타겟 오브젝트 (플레이어)의 포지션 값 실시간 확인
            Player.transform.position.x,
            Player.transform.position.y+3f,
            CameraZ);
	}
	

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(      // 타겟 오브젝트 (플레이어)와의 거리를 선형보간 하여 부드럽게 따라다님
            transform.position,
            target_pos,
            Time.deltaTime * Speed);
        
    }

}
