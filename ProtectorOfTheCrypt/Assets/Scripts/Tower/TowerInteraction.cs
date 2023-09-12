/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerInteraction : Interactable
{
    private GameObject dice;

    private void Start()
    {
        dice = transform.parent.GetChild(0).gameObject;
        isInteractable = true;
    }
    public override void Interact()
    {
        base.Interact();
        DiceManager.Instance.currentDice = dice;
        DiceManager.Instance.RerollDicePopUp();
    }
}*/
