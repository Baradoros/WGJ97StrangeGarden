using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SettingsMenu : Menu
{
    [SerializeField]
    Slider _musicVolumeSlider, _sfxVolumeSlider;

    [SerializeField]
    Slider.SliderEvent _MusicVolumeChanged, _SFXVolumeChanged;

    public Slider.SliderEvent MusicVolumeChanged        { get { return _MusicVolumeChanged; } }
    public Slider.SliderEvent SFXVolumeChanged          { get { return _SFXVolumeChanged; } }

    protected override void Awake()
    {
        base.Awake();
        bool slidersEnsured =                           EnsureSliderRefs();

        if (slidersEnsured)
            ListenForSliderChanges();
        
    }

    bool EnsureSliderRefs()
    {
        if (_musicVolumeSlider == null || _sfxVolumeSlider == null)
        {
            Debug.LogError(this.name + " needs refs to the music and sfx volume sliders!");
            return false;
        }

        return true;
    }
    void ListenForSliderChanges()
    {
        _musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);
        _sfxVolumeSlider.onValueChanged.AddListener(OnSFXVolumeChanged);
    }

    void OnMusicVolumeChanged(float newVolume)
    {
        MusicVolumeChanged.Invoke(newVolume);
    }

    void OnSFXVolumeChanged(float newVolume)
    {
        SFXVolumeChanged.Invoke(newVolume);
    }
}
