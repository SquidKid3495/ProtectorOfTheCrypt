using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIButtons : MonoBehaviour
{
    public GameObject UIButton;
    public GameObject TowerSelection;

    // Opens the Tower Selection Screen and Disables the In Game UI Buttons 
    public void OpenTowerSelection()
    {
        UIButton.SetActive(false);
        TowerSelection.SetActive(true);
    }

}
