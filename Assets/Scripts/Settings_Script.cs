using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings_Script : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMPro.TMP_Dropdown resDropdown;
    private Resolution[] resolutions;
    private int currentRes = 0;
    
    void Start()
    {
        resolutions = Screen.resolutions;
        resDropdown.ClearOptions();
        List<string> reso = new List<string>();

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            reso.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentRes = i;
            }
        }

        resDropdown.AddOptions(reso);
        resDropdown.value = currentRes;
        resDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetResolution(int index)
    {
        Resolution resolution = resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}