using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DataController : MonoBehaviour
{

    static GameObject _container;
    static GameObject Container
    {
        get
        {
            return _container;
        }
    }

    static DataController _instance;
    public static DataController instance // 싱글턴으로 사용하기위해 인스턴스화
    {
        get
        {
            if (!_instance)
            {
                _container = new GameObject();
                _container.name = "DataController";
                _instance = _container.AddComponent(typeof(DataController)) as DataController;
                DontDestroyOnLoad(_container);
            }
            return _instance;
        }
    }

    public string GameDataFileName = "datafile.json"; // 게임 데이터를 저장할 파일의 이름

    public GameData _gameData;
    public GameData gameData
    {
        get
        {
            if (_gameData == null)
            {
                LoadGameData();
                SaveGameData();

            }
            return _gameData;
        }
    }

    public void LoadGameData() // 게임데이터 로드
    {
        string filePath = Application.persistentDataPath + GameDataFileName; // 파일 경로
        if (File.Exists(filePath)) 
        {
            Debug.Log("불러오기 성공!");
            Debug.Log(filePath);
            string FromJsonData = File.ReadAllText(filePath);           // 게임데이터 파일을 Json으로 읽어들임
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);   // Json으로 읽어온 데이터를 적용
        }
        else
        {
            Debug.Log("새로운 파일 생성");
            _gameData = new GameData();
            Initialize_Data();
        }
    }
    public void SaveGameData() // 게임데이터 저장
    {
        string ToJsonData = JsonUtility.ToJson(gameData);                       // 게임 데이터를 Json을 통해 문자열 데이터저장
        string filePath = Application.persistentDataPath + GameDataFileName;    // 게임데이터 파일 경로와 파일이름
        File.WriteAllText(filePath, ToJsonData);                                // Json 으로 생성된 문자열 데이터를 게임데이터 파일에 저장
        Debug.Log("저장 완료");
    }
   

    public void Initialize_Data() // 게임 데이터 초기화
    {
        gameData.Damage_level = 1;      //플레이어 모든 능력치를 1레벨로 초기화
        gameData.Max_health_level = 1;
        gameData.Healing_level = 1;
        gameData.Defend_level = 1;
        gameData.Reload_level = 1;

        gameData.Money = 0;
        gameData.Is_shoot = false;

        gameData.Max_health = 100 + 10f * DataController.instance.gameData.Max_health_level; // 플레이어의 체력,공격력,방어력,체력회복,공격속도를 레벨에 맞게 산정
        gameData.Current_health = DataController.instance.gameData.Max_health;
        gameData.Damage = 10 * DataController.instance.gameData.Damage_level;
        gameData.Defend = 1 + 1f * DataController.instance.gameData.Defend_level;
        gameData.Healing = 0 + 0.2f * DataController.instance.gameData.Healing_level;
        gameData.Reload = 0.8f - 0.1f * DataController.instance.gameData.Reload_level;
        gameData.Reload_term = 0;
    }

}
