using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Generic timer class. Will be used for Timed Mode
public class Timer : MonoBehaviour
{
    public float RunEvery = 5;
    public bool RunOnAwake = false;
    public bool loop = true;

    private void OnEnable()
    {
        if(RunOnAwake)
        {
            Invoke("Execute", 0);
        }

        if (loop)
        {
            InvokeRepeating("Execute", RunEvery, RunEvery);
        }
        else
        {
            Invoke("Execute", RunEvery);
        }
    }

    public void Execute()
    {
    }
}
