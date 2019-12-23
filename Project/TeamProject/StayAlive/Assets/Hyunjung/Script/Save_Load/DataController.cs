using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
    public static DataController instance
    {
        get
        {
            if (!_instance) // 게임 시작시 현재 인스턴스 값이 없다면 이 게임 오브젝트를 인스턴스로 선언 / 싱글턴 패턴에 사용하기 위함
            {
                _container = new GameObject(); // 데이터를 관리해줄 게임 오브젝트 생성
                _container.name = "DataController"; // 게임 오브젝트의 이름을 DataController 로 설정
                _instance = _container.AddComponent(typeof(DataController)) as DataController; // 해당 스크립트를 컴포넌트로 추가
                DontDestroyOnLoad(_container); // 씬이 전환되어도 삭제되지 않도록 설정                            
            }
            return _instance;
        }
    }

    public string GameDataFileName = "datafile.json"; // 저장될 데이터 파일의 last 이름

    public GameData _gameData;
    public GameData gameData // 데이터 저장에 관련된 게임 변수들
    {
        get
        {
            if (_gameData == null) // 현재 게임 데이터가 없다면 기존 데이터를 불러오고 저장
            {
                LoadGameData();
                SaveGameData();
            }
            return _gameData;
        }
    }

    public void LoadGameData()
    {
        string filePath = Application.persistentDataPath + GameDataFileName; // 게임 데이터파일은 설정된 경로 + 데이터파일 이름으로 생성
        if (File.Exists(filePath)) // 저장된 파일이 있는지 판별
        {
            Debug.Log("불러오기 성공!"); // 저장된 데이터 파일이 있다면 불러오기
            Debug.Log(filePath);
            string FromJsonData = File.ReadAllText(filePath);
            _gameData = JsonUtility.FromJson<GameData>(FromJsonData);

        }
        else
        {
            Debug.Log("새로운 파일 생성");
            _gameData = new GameData(); // 저장된 데이터 파일이 없다면 새 데이터 파일 생성
            Data_Initialize(); // 데이터 초기화
        }
    }
    public void SaveGameData()
    {
        string ToJsonData = JsonUtility.ToJson(gameData); // 게임 데이터내용을 Json 파일로 변환
        string filePath = Application.persistentDataPath + GameDataFileName; // 데이터 파일 생성
        File.WriteAllText(filePath, ToJsonData); // Json 파일을 Txt 파일로 변환
        Debug.Log("저장 완료");
    }

    private void Start()
    {
        LoadGameData();         // 게임 시작시 기존 데이터파일 불러오기
    }


    public void Data_Initialize()
    { // 데이터파일 초기화함수
        Debug.Log("new game");
        _gameData.player_Level = 1;
        _gameData.player_Hp = 100;
        _gameData.player_Hungry = 100;
        _gameData.player_Exp = 0;
        _gameData.player_Pow = 10;
        _gameData.player_MaxHp = 100;
        _gameData.player_MaxHungry = 100;
        _gameData.player_MaxExp = 100;
        _gameData.day_of_game = 0;

        _gameData.stone_count = 0;
        _gameData.leaf_count = 0;
        _gameData.tree_count = 0;
        _gameData.herb_count = 0;
        _gameData.bone_count = 0;
        _gameData.skin_count = 0;
        _gameData.big_tree_count = 0;
        _gameData.meat_count = 0;
        _gameData.rope_count = 0;

        _gameData.ripe_meat_count = 0;

        _gameData.leaf_dress_count = 0;
        _gameData.skin_dress_count = 0;

        _gameData.stone_knife = false;
        _gameData.ax = false;
        _gameData.pickax = false;

        _gameData.house_0 = true;
        _gameData.house_1 = false;
        _gameData.house_2 = false;
        _gameData.house_3 = false;
        _gameData.fense_HP_1 = 10000;
        _gameData.fense_HP_2 = 10000;
        _gameData.fense_HP_3 = 10000;
        _gameData.fense_HP_4 = 10000;
        _gameData.fense_HP_5 = 10000;
        _gameData.house_HP = 1000;

        _gameData.Day = 0;
        _gameData.Hour = 12;
        _gameData.Minute = 0;

        _gameData.head = 1;
        _gameData.body = 1;
        _gameData.arms = 1;
        _gameData.legs = 1;
        _gameData.hands = 1;
        _gameData.foots = 1;

        _gameData.Raft = false;
        _gameData.Sos_sign = false;
        _gameData.Rescue_probability = 0;

        _gameData.nomalpet1 = false;
        _gameData.nomalpet2 = false;
        _gameData.legendpet = false;

        _gameData.Damage_Up = 0;
        _gameData.Spead_Up = 0;
        _gameData.Health_Up = 1;
        _gameData.hungry_val = 0.3f;
    }

    public void NewGameButton()
    { // 새 게임 시작 버튼
        Data_Initialize(); // 기존 데이터를 초기값으로 변경
        SceneManager.LoadScene("Test");
    }

    public void Cheatkey()
    {
        _gameData.stone_count += 200;
        _gameData.leaf_count += 200;
        _gameData.tree_count += 200;
        _gameData.herb_count += 200;
        _gameData.bone_count += 200;
        _gameData.skin_count += 200;
        _gameData.big_tree_count += 200;
        _gameData.meat_count += 200;
        _gameData.rope_count += 200;
        _gameData.ripe_meat_count += 200;
    }

    public void Cheatpet()
    {
        _gameData.nomalpet1 = true;
        _gameData.nomalpet2 = true;
        _gameData.legendpet = true;
    }
}


