using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret4 : MonoBehaviour {

    public GameObject Bullet;
    public Transform FirePos;
    public Transform target;

    public bool lockon = false;
    public BoxCollider box_col;
    public float term = 0;
    public float fire_term = 1f;
    // Use this for initialization
    void Start()
    {
        StartCoroutine(Shooting());
        lockon = false;
        box_col = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        if (target)
        {
            transform.LookAt(target);
        }
        else if (!target)
        {
            lockon = false;
            box_col.enabled = false;
        }
        term += Time.deltaTime;

        if (term >= 1f)
        {
            term = 0;
            box_col.enabled = true;

        }
    }

    IEnumerator Shooting()
    {
        while (true)
        {
            if (lockon == true)
            {
                Instantiate(Bullet, FirePos.transform.position, transform.rotation);
            }
                yield return new WaitForSeconds(fire_term);
        }

    }

    void OnTriggerEnter(Collider col)
    {

        if (lockon == false)
        {
            if (col.CompareTag("defense_enemy"))
            {
                lockon = true;
                target = col.transform;
                transform.LookAt(target);
            }
        }
    }
    void OnTriggerExit(Collider col)
    {/*
        if (col.gameObject == target.gameObject)
        {
            lockon = false;
            target = null;
        }*/
    }
}