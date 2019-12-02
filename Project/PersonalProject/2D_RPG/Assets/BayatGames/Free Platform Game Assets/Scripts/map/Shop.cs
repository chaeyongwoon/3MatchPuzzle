using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public Game_Manager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("Game_Manager").GetComponent<Game_Manager>();
    }

    public void Shop_open()
    {
        gm.open_shop();
    }
}
