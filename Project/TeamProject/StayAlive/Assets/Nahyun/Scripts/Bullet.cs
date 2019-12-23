using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed = 1000.0f;

    // Use this for initialization
    void Start()
    { // 타워 총알함수
        GetComponent<Rigidbody>().AddForce(transform.forward * speed); // 앞을 향해 설정된 속도로 날아간다
        Destroy(this.gameObject, 2f);
    }
}
