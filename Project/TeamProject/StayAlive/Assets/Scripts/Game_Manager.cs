using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Game_Manager : MonoBehaviour
{
    public static Game_Manager instance;

    public Text Day_text, Day_night;

    public GameObject sun;

    public bool Seventh_day = false;

    public AudioSource Musicplayer;
    public AudioClip Day_music, Night_music, Cave_music, ship_horn;

    public bool Day_play, Night_play, Cave_play, In_island, In_cave, ship_play = false;
    public int a;
    public float b = 0;

    public Material In_Cave_mat, In_Island_mat;


    public enum State
    {
        Day,
        Night
    }

    public State day_state;

    // Use this for initialization
    void Start()
    {
        if (!instance)
        {
            instance = this;
        }


        day_state = State.Day;
        StartCoroutine("DayTime");
        Musicplayer = GetComponent<AudioSource>();

        In_island = true;

    }

    // Update is called once per frame
    private void Update()
    {
        sun.transform.RotateAround(Vector3.zero, Vector3.forward, 1.15f * Time.deltaTime);

        if (Seventh_day == false)
        {
            if (In_island == false)
            {
                In_cave = true;
                Day_play = false;
                Night_play = false;
                RenderSettings.skybox = In_Cave_mat;
                if (Cave_play == false)
                {
                    Cave_play = true;
                    Musicplayer.clip = Cave_music;
                    Musicplayer.Play();
                }
            }


            else if (In_island == true)
            {
                In_cave = false;
                if (day_state == State.Day)
                {
                    RenderSettings.skybox = In_Island_mat;
                    if (Day_play == false)
                    {
                        Cave_play = false;
                        Night_play = false;
                        Day_play = true;
                        Musicplayer.clip = Day_music;
                        Musicplayer.Play();
                    }
                }

                else if (day_state == State.Night)
                {
                    RenderSettings.skybox = In_Cave_mat;
                    if (Night_play == false)
                    {
                        Cave_play = false;
                        Day_play = false;
                        Night_play = true;
                        Musicplayer.clip = Night_music;
                        Musicplayer.Play();
                    }
                }
            }
        }

        else if (Seventh_day == true)
        {
            Sos();
        }
    }

    public IEnumerator DayTime()
    {
        while (true)
        {
            DataController.instance._gameData.Minute += 5;
            if (DataController.instance._gameData.Minute >= 60)
            {
                DataController.instance._gameData.Minute -= 60;
                DataController.instance._gameData.Hour += 1;
                if (DataController.instance._gameData.Hour >= 24)
                {
                    DataController.instance._gameData.Hour -= 24;
                    DataController.instance._gameData.Day += 1;
                }

                if (DataController.instance._gameData.Hour >= 6 && DataController.instance._gameData.Hour < 20)
                {
                    day_state = State.Day;
                    Day_night.text = string.Format("Day");
                }
                else
                {
                    Day_night.text = string.Format("Night");
                    day_state = State.Night;
                }
            }
            Day_text.text = string.Format("{0}Days {1}H {2}M",
                DataController.instance._gameData.Day,
                DataController.instance._gameData.Hour,
                DataController.instance._gameData.Minute);

            if (DataController.instance.gameData.Day >= 7)
            {
                if (DataController.instance._gameData.Hour >= 12)
                {
                    Seventh_day = true;
                    
                }

                if (DataController.instance._gameData.Hour >= 14)
                {
                    SceneManager.LoadScene("Settlement_ending");
                }
            }

            yield return new WaitForSeconds(1f);
        }
    }

    public void Sos()
    {
       
        if (ship_play == false)
        {
            ship_play = true;
            Musicplayer.clip = ship_horn;
            Musicplayer.Play();
        }
        
        b += Time.deltaTime;
        if (b >= 5f)
        {

            // 이 위치에 배 고동소리 추가
             a = Random.Range(0, 100);
            Debug.Log(a);
            if (a < DataController.instance.gameData.Rescue_probability)
            {
                SceneManager.LoadScene("Sos_ending");
            }
        }
    }




}
