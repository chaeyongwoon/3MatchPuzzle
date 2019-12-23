using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow_cam : MonoBehaviour {

    public GameObject Player;

    private Vector3 pos;
    public float x, y,z;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        pos = new Vector3(x,y,z);


        transform.position = Player.transform.position+pos;
	}
}
