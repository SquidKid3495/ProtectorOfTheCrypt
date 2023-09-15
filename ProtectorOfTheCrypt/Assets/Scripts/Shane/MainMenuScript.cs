using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void StoryMode()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void TimedMode()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Settings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void CreditsScreen()
    {
        SceneManager.LoadScene("CreditsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
