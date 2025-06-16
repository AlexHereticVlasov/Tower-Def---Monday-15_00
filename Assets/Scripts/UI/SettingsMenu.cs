using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public sealed class SettingsMenu : MonoBehaviour
{
    private const string ResolutionKey = "Resolution";

    [SerializeField] private TMP_Dropdown _resolutionBar;
    [SerializeField] private TMP_Dropdown _qulityBar;

    private Resolution[] _resolutions;

    private void Start()
    {
        _resolutionBar.ClearOptions();
        _resolutions = Screen.resolutions;
        List<string> options = new();
        int currentResolutionIndex = 0;


        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = $"{_resolutions[i].width} x {_resolutions[i].height}";
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width &&
                _resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        _resolutionBar.AddOptions(options);
        _resolutionBar.RefreshShownValue();
        Load(currentResolutionIndex);
    }

    public void Save()
    {
        PlayerPrefs.SetInt(ResolutionKey, _resolutionBar.value);
        PlayerPrefs.SetInt("Quality", _qulityBar.value);
        PlayerPrefs.SetInt("Fullscreen", System.Convert.ToInt32(Screen.fullScreen));
    }

    private void Load(int currentResolutionIndex)
    {
        _resolutionBar.value = PlayerPrefs.HasKey(ResolutionKey) ? 
                               PlayerPrefs.GetInt(ResolutionKey) : 
                               currentResolutionIndex;

        _qulityBar.value = PlayerPrefs.HasKey("Quality") ?
                           PlayerPrefs.GetInt("Quality") : 3;

        Screen.fullScreen = PlayerPrefs.HasKey("Fullscreen") ?
                            System.Convert.ToBoolean(PlayerPrefs.GetInt("Fullscreen")) :
                            true;
    }

    public void SetFullScreen(bool isFull) => Screen.fullScreen = isFull;

    public void SetQuality(int index) => QualitySettings.SetQualityLevel(index);

    public void SetResolution(int index)
    {
        var resolution = _resolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }


}
