using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TeaspoonTools.UI;

public abstract class Menu : UIElementController
{
    #region Serializable Fields
    [SerializeField]
    protected GameObject _uiControls;
    [SerializeField]
    UnityEvent _Shown, _Hidden;
    #endregion

    #region Properties

    public UnityEvent Shown                     { get { return _Shown; } }
    public UnityEvent Hidden                    { get { return _Hidden; } }
    public bool isShowing                       { get; protected set; }
    public bool isShowable                      { get; set; } = true;

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();
        EnsureUIControls();
    }

    public void Show()
    {
        if (!isShowable)
            return;

        _uiControls.SetActive(true);
        isShowing =                             true;
        Shown.Invoke();
    }

    public void Hide()
    {
        _uiControls.SetActive(false);
        isShowing =                             false;
        Hidden.Invoke();
    }

    void EnsureUIControls()
    {
        if (_uiControls == null)
            Debug.LogError(this.name + " needs a ref to its UI controls!");
    }

    #endregion
}
