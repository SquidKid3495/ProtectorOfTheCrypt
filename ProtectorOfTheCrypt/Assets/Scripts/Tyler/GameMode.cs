using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMode : MonoBehaviour
{
    public virtual bool CheckGameLost() { return true; }
    public virtual bool CheckGameWon() { return true; }
    
}
