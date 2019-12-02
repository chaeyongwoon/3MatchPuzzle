using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Manager : MonoBehaviour {

    public GameObject[] obj;


	// Use this for initialization
	void Start () {
        DataController.instance.gameData.Color_num = 1;

        obj[0].SetActive(false);
        obj[1].SetActive(false);
        obj[2].SetActive(false);
        obj[3].SetActive(false);
    }
	
    public void New_start()
    {

        DataController.instance.gameData.Damage_level = 1;
        DataController.instance.gameData.Max_health_level = 1;
        DataController.instance.gameData.Healing_level = 1;
        DataController.instance.gameData.Defend_level = 1;
        DataController.instance.gameData.Reload_level = 1;

        DataController.instance.gameData.Money = 0;
        DataController.instance.gameData.Is_shoot = false;

        DataController.instance.gameData.Max_health = 100 + 10f * DataController.instance.gameData.Max_health_level;
        DataController.instance.gameData.Current_health = DataController.instance.gameData.Max_health;
        DataController.instance.gameData.Damage = 10 * DataController.instance.gameData.Damage_level;
        DataController.instance.gameData.Defend = 1 + 1f * DataController.instance.gameData.Defend_level;
        DataController.instance.gameData.Healing = 0 + 0.2f * DataController.instance.gameData.Healing_level;
        DataController.instance.gameData.Reload = 0.8f - 0.1f * DataController.instance.gameData.Reload_level;
        DataController.instance.gameData.Reload_term = 0;


        obj[0].SetActive(true);
        obj[1].SetActive(true);
        obj[2].SetActive(true);
        obj[3].SetActive(true);

        Game_Manager.instance.Start();


        SceneManager.LoadScene("1.tutorial");

    }

    public void Game_Load()
    {

        DataController.instance.LoadGameData();

        obj[0].SetActive(true);
        obj[1].SetActive(true);
        obj[2].SetActive(true);
        obj[3].SetActive(true);

        Game_Manager.instance.Start();
        SceneManager.LoadScene("2.town");
    }

    public void Exit_game()
    {
        // UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

}
