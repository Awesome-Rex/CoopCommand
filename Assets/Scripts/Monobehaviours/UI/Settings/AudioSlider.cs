using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioSlider : MonoBehaviour
{
    public AudioMixer mixer;
    public string valName;

    public void Changed (float val)
    {
        mixer.SetFloat(valName, Mathf.Log10(val) * 20f);
    }

    private void Awake()
    {
        GetComponent<Slider>().minValue = 0.0001f;
        GetComponent<Slider>().maxValue = 1f;
    }
}
