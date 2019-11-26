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
        target_pos = new Vector3(
            Player.transform.position.x,
            Player.transform.position.y+3f,
            CameraZ);
	}
	

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(
            transform.position,
            target_pos,
            Time.deltaTime * Speed);
        
    }

}
