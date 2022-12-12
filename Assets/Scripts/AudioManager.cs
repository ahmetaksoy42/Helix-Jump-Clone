using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioType
{
    Bounce,
    Pass,
    Boom
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bounceSound, passedRingSound,boomSound;

    public static AudioManager Instance { get; set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PlayAudio(AudioType audioType)
    {
        switch (audioType)
        {
            case AudioType.Bounce:
                bounceSound.Play();
                break;
            case AudioType.Pass:
                passedRingSound.Play();
                break;
            case AudioType.Boom:
                boomSound.Play();
                break;
            default:
                break;
        }
    }
}
