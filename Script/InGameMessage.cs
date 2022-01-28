using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMessage : MonoBehaviour
{
    public GameObject MessagePanel;

    public void OpenMessagePanel(string message)
    {
        Debug.Log("Message Opened");
        MessagePanel.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = message;
        MessagePanel.SetActive(true);
        
    }

    public void CloseMessagePanel()
    {
        MessagePanel.SetActive(false);
    }


}
