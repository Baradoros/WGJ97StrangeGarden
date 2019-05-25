using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseMenu : Menu
{
    #region Serializable Fields
    [SerializeField]
    [Tooltip("Input axis that can bring up this menu.")]
    string _axisToBringUp =                         "pause";
    
    [SerializeField]
    Button _resumeGameButton, _titleScreenButton, _settingsButton;

    [SerializeField]
    UnityEvent _ResumeGame, _GoToTitleScreen, _GoToSettings;
    
    #endregion

    #region Properties
    public string axisToBringUp                     { get { return _axisToBringUp; } }
    public UnityEvent ResumeGame                    { get { return _ResumeGame; } }
    public UnityEvent GoToTitleScreen               { get { return _GoToTitleScreen; } }
    public UnityEvent GoToSettings                  { get { return _GoToSettings; } }

    #endregion

    #region Methods
    protected override void Awake()
    {
        base.Awake();
        bool refsEnsured =                          EnsureButtonRefs();
        if (refsEnsured)
            ListenForButtonClicks();
    }

    void Update()
    {
        // Let the user show and hide the pause menu at will
        bool pressedPauseButton =                   Input.GetButtonDown(axisToBringUp);

        if (!isShowing && pressedPauseButton) 
            Show();
        else if (isShowing && pressedPauseButton)
        {
            // Treat it as if the Resume Game button was pressed
            OnResumeButtonClicked();
        }

    }

    void OnResumeButtonClicked()
    {
        ResumeGame.Invoke();
    }

    void OnTitleScreenButtonClicked()
    {
        GoToTitleScreen.Invoke();
    }

    void OnSettingsButtonClicked()
    {
        GoToSettings.Invoke();
    }

    #region Helpers
    bool EnsureButtonRefs()
    {
        if (_resumeGameButton == null || _titleScreenButton == null || _settingsButton == null)
        {
            Debug.LogError(this.name + " needs refs to the Resume Game, Title Screen, and Settings buttons!");
            return false;
        }

        return true;
    }

    void ListenForButtonClicks()
    {
        _resumeGameButton.onClick.AddListener(OnResumeButtonClicked);
        _titleScreenButton.onClick.AddListener(OnTitleScreenButtonClicked);
        _settingsButton.onClick.AddListener(OnSettingsButtonClicked);
    }

    #endregion


    #endregion
}
