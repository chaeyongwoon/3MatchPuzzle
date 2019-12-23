using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet_Sys : MonoBehaviour {

    public GameObject[] pet_image; //플레이어 상태창에 나오는 pet 이미지 (선택한pet의 그림을 넣어둠)
    public GameObject none_petima; //고른 펫이 없을때 사용
    public GameObject select_Pet_Popup; //데리고 다닐 펫을 선택하기 위해 뜨는 창
    //public GameObject lake_pet; //플레이어가 갖고있지 않은 펫을 선택했을 때
    public GameObject pet_first; // 플레이어를 따라다니는 펫1 (실제 오브젝트)
    public GameObject pet_second; 
    public GameObject legend_pet;


    void Awake()
    {
        DataController.instance.gameData.Pet = GameData.State.none;
    }
    

	void Start () {
        // 시작시 펫 비활성화
        pet_first.SetActive(false);
        pet_second.SetActive(false);
        legend_pet.SetActive(false);
        select_Pet_Popup.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
		if(DataController.instance.gameData.Pet == GameData.State.none) // 현재 선택된 펫이 없다면 펫 과 펫 능력 비활성화
        {
            none_petima.SetActive(true);
            pet_image[0].SetActive(false);
            pet_image[1].SetActive(false);
            pet_image[2].SetActive(false);
            pet_first.SetActive(false);
            pet_second.SetActive(false);
            legend_pet.SetActive(false);

            DataController.instance._gameData.Damage_Up = 0; // 데미지 증가값 0
            DataController.instance._gameData.Spead_Up = 0f; // 스피드 증가값 0
         
        }
        if(DataController.instance.gameData.Pet == GameData.State.nomalpet1) // 1번 펫 선택시
        {
            none_petima.SetActive(false);
            pet_image[0].SetActive(true);
            pet_image[1].SetActive(false);
            pet_image[2].SetActive(false);
            pet_first.SetActive(true);
            pet_second.SetActive(false);
            legend_pet.SetActive(false);

            DataController.instance._gameData.Damage_Up = 3; // 데미지 증가값 3
            DataController.instance._gameData.Spead_Up = 0f; // 스피드 증가값 0
         
        }
        if (DataController.instance.gameData.Pet == GameData.State.nomalpet2) // 2번펫 선택시
        {
            none_petima.SetActive(false);
            pet_image[0].SetActive(false);
            pet_image[1].SetActive(true);
            pet_image[2].SetActive(false);
            pet_first.SetActive(false);
            pet_second.SetActive(true);
            legend_pet.SetActive(false);

            DataController.instance._gameData.Damage_Up = 0; // 데미지 증가값 0
            DataController.instance._gameData.Spead_Up = 0.5f; // 스피드 증가값 0.5
          
        }
        if (DataController.instance.gameData.Pet == GameData.State.legendpet) // 레전드펫 선택시
        {
            none_petima.SetActive(false);
            pet_image[0].SetActive(false);
            pet_image[1].SetActive(false);
            pet_image[2].SetActive(true);
            pet_first.SetActive(false);
            pet_second.SetActive(false);
            legend_pet.SetActive(true);
            DataController.instance._gameData.Damage_Up = 3; // 데미지 증가값 3
            DataController.instance._gameData.Spead_Up = 0.5f; // 스피드 증가값 0.5
        
        }
    }
    public void NomalPet_first()
    {
        if (DataController.instance.gameData.nomalpet1 == true) // 1번 펫을 가지고 있다면 1번펫 착용
        {
            DataController.instance.gameData.Pet = GameData.State.nomalpet1; 
        }
        if(DataController.instance.gameData.nomalpet1 == false)
        {
            Debug.Log("선택한 펫을 갖고 있지 않습니다");
        }
    }
    public void NomalPet_second()
    {
        if (DataController.instance.gameData.nomalpet2 == true) // 2번펫을 가지고 있다면 2번펫 착용
        {
            DataController.instance.gameData.Pet = GameData.State.nomalpet2;
        }
        if (DataController.instance.gameData.nomalpet2 == false)
        {
            Debug.Log("선택한 펫을 갖고 있지 않습니다");
        }
    }
    public void Legend_Pet()
    {
        if (DataController.instance.gameData.legendpet == true) // 레전드 펫을 가지고 있다면 레전드펫 착용
        {
            DataController.instance.gameData.Pet = GameData.State.legendpet;
        }
        if (DataController.instance.gameData.legendpet == false)
        {
            Debug.Log("선택한 펫을 갖고 있지 않습니다");
        }
    }
    public void ClickPetImage() // 펫 선택창 활성화
    {
        select_Pet_Popup.SetActive(true);
    }
    public void ClosePet_Selected_popUP() // 펫 선택창 비활성화
    {
        select_Pet_Popup.SetActive(false);
    }
}
