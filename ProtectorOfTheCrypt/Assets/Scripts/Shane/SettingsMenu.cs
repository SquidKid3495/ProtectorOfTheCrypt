using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject SettingsPanel;
    public GameObject PauseMenuRef;

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenSettings()
    {
        PauseMenuRef.GetComponent<PauseMenu>().PausePanel.SetActive(false);
        SettingsPanel.SetActive(true);
    }

    public void Back()
    {
        SettingsPanel.SetActive(false);
        PauseMenuRef.GetComponent<PauseMenu>().PausePanel.SetActive(true);
    }
}
