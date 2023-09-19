using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public GameObject CreditsPanel;
    public MainMenuScript MainRef;

    // Update is called once per frame
    void Update()
    {

    }
    public void Back()
    {
        CreditsPanel.SetActive(false);              //Make Credits Panel Inactive
        MainRef.MainPanel.SetActive(true);          //Make Main Menu Active
    }
}
