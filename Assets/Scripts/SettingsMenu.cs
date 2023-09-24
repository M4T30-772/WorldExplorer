using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer AudioMixer;
    public Toggle ToggleAudio;
    public Slider AudioSlider;
    public float volume2 = 0f; 


    public void SetVolume(float volume)
    {
        AudioMixer.SetFloat("volume", volume);
        volume2 = volume;
    }

    public void ToggleAudioStateChanged(bool isAudioOn)
    {
        if (isAudioOn)
        {
            AudioMixer.SetFloat("volume", volume2);
        }
        else
        {
            AudioMixer.SetFloat("volume", -60f);
        }
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void LeaveGame()
    {
        Application.Quit();
    }
}
