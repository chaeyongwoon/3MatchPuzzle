using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInCave : MonoBehaviour {
    public static PlayerInCave instance;
    public enum State
    {
        InIsland,
        InCave        
    }
    public State playerPos;

	// Use this for initialization
	void Start () {
        PlayerInCave.instance = this;
        playerPos = State.InIsland;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
