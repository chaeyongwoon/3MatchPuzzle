using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dontdestroy : MonoBehaviour {

	void Start () {
        DontDestroyOnLoad(this.gameObject);
	}
	
}
