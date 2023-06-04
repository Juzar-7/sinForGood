using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class Prompts : MonoBehaviour
{
    public static Prompts prompts;
    public GameObject notificationTab;
    public TextMeshProUGUI Notification;
    // Start is called before the first frame update
    void Awake()
    {
        prompts = this;
    }

    public void Notify(string message)
    {
        notificationTab.SetActive(true);
        Notification.text = message;
    }

    public void Close()
    {
        notificationTab.SetActive(false);
    }
}
