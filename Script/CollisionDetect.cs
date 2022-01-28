using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDetect : MonoBehaviour
{   
    public PlayerMove player;
    public InGameMenu inGameMenu;
    public GameObject text_5sec;
    public GameObject area;
    GameLogic m_GameLogic;

    public InGameMessage inGameMessage;
    string message;

    public void Start()
    {
        m_GameLogic = area.GetComponent<GameLogic>();
    }

    // For player's collision (CharacterController utilizes this method)
    private void OnControllerColliderHit(ControllerColliderHit other)
    {
        // If collide with doctor
        if(other.gameObject.tag == "Doctor")
        {
            player.Death();
            inGameMenu.Invoke("DeathMenu", 2);
        }
        // If collide with door and player has key
        else if(other.gameObject.name == "BSGSecurityDoor")
        {  
            
            StartCoroutine("MessagePanel", "- Open Door -");
            if (Input.GetKeyDown(KeyCode.F) && player.isDead == false)
            {
                if (player.hasKey)
                {
                    inGameMenu.Invoke("WinMenu",1);
                }
                else
                {
                    message = "You haven't found the key! Careful! Doctor is behind you!";
                    StartCoroutine("WaitForSec", message);
                }
            }
        }
        // If collide with vaccine
        else if(other.gameObject.tag == "goal")
        {
            StartCoroutine("MessagePanel", "- Pick up -");
            
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(other.gameObject);
                //m_GameLogic.ReduceSyringeCount();
                message = "You had found a vaccine. Keep it safe!";
                if (m_GameLogic.GetRemainingSyringeCount() == 0)
                {
                    message = "You had found all vaccine. Find the key to escape!";  
                }
                StartCoroutine("WaitForSec", message);
            }
            
        }   
        // If collide with key
        else if(other.gameObject.tag == "Key")
        {
            StartCoroutine("MessagePanel", "- Pick up -");
            if (Input.GetKeyDown(KeyCode.F))
            {
                // Found the key
                player.hasKey =true;
                Destroy(other.gameObject);
                message = "You had picked up the key. Find the door now!";
                StartCoroutine("WaitForSec", message);
            }
        }

        
    }

    // For doctor's collision (BoxCollider utilizes this method)
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player.Death();
            inGameMenu.Invoke("DeathMenu", 3);
        }
    }

    IEnumerator WaitForSec(string message)
    {
        text_5sec.SetActive(true);
        text_5sec.GetComponent<TMPro.TextMeshProUGUI>().text = message; 
        Debug.Log("waiting");
        yield return new WaitForSeconds(3);
        text_5sec.SetActive(false);
    }

    IEnumerator MessagePanel(string message)
    {
        inGameMessage.OpenMessagePanel(message); 
        yield return new WaitForSeconds(0.3f);
        inGameMessage.CloseMessagePanel();
    }

    
}
