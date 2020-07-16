using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer I;

    public AudioMixer gameMixer;

    private void Awake()
    {
        I = this;
    }
}
