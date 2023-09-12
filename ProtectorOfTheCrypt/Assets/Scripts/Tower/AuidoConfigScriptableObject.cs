using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Audio Config", menuName = "Towers/AudioConfig", order = 5)]
public class AuidoConfigScriptableObject : ScriptableObject
{
    [Range(0,1f)] public float Volume = 1;
    public AudioClip[] FireClips;
    public AudioClip BuildingClip;

    public void PlayShootingClip(AudioSource AudioSource)
    {
        AudioSource.PlayOneShot(FireClips[Random.Range(0, FireClips.Length)], Volume);
    }

    public void PlayBuildingClip(AudioSource AudioSource)
    {
        AudioSource.PlayOneShot(BuildingClip, Volume);
    }
}
