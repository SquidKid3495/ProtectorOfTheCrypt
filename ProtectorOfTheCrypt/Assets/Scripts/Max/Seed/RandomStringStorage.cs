using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "RandomStringStorage", menuName = "Custom/RandomStringStorage")]
public class RandomStringStorage : ScriptableObject
{
    public List<string> randomStrings = new List<string>();

    public void AddRandomString(string randomString)
    {
        randomStrings.Add(randomString);
    }
}