using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSave : MonoBehaviour
{
    public static GameSave I;

    public float musicVolume;
    public float SFXVolume;

    public float highScore;

    public void Save ()
    {
        if (PlayerPrefs.GetFloat("High Score") < GameplayControl.I.progress)
        {
            PlayerPrefs.SetFloat("High Score", GameplayControl.I.progress);
        }

        PlayerPrefs.SetFloat("Music Volume", musicVolume);
        PlayerPrefs.SetFloat("SFX Volume", SFXVolume);

        PlayerPrefs.Save();
    }

    public void Load ()
    {
        musicVolume = PlayerPrefs.GetFloat("Music Volume");
        SFXVolume = PlayerPrefs.GetFloat("SFX Volume");
    }
    
    public void Clear ()
    {
        PlayerPrefs.DeleteAll();
    }

    private void Awake()
    {
        I = this;
    }
}
