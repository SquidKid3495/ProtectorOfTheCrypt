using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SettingsPanel;
    public PauseMenu PauseMenuRef;
    public MainMenuScript MainReference;

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenSettings()
    {
        PauseMenuRef.PausePanel.SetActive(false);                                   //Make Pause Menu Inactive
        SettingsPanel.SetActive(true);                                              //Open Settings Panel
    }

    public void BackToPause()
    {
        SettingsPanel.SetActive(false);                                             //Make Settings Panel Inactive
        PauseMenuRef.PausePanel.SetActive(true);                                    //Make Pause Menu Active
    }

    public void BackToMain()
    {
        SettingsPanel.SetActive(false);                                             //Make Settings Panel Inactive
        MainReference.MainPanel.SetActive(true);                                    //Make Main Menu Active
    }
}
