using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Portal1 : MonoBehaviour
{
    public Lobby_Manager lm;

   
    private void UPdate()
    {
        if (!lm)
        {
           lm= GameObject.FindGameObjectWithTag("Lobby_Manager").GetComponent<Lobby_Manager>();
        }
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /// 자신의 태그에 맞는 스테이지 입장판넬 활성화 ///
        if (collision.CompareTag("Player")) 
        {
            if (CompareTag("To_stage1"))
            {
                lm.Open_portal_panel(0);
            }

            else if (CompareTag("To_stage2"))
            {
                lm.Open_portal_panel(1);
            }

            else if (CompareTag("To_stage_infinite"))
            {
                lm.Open_portal_panel(2);
            }

        }
    }


}
