using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TeaspoonTools.UI;

public class ScoreDisplay : UIElementController
{
    #region Serializable Fields
    [SerializeField] UnityEvent _ScoreChange;
    [SerializeField]
    int _score;

    [SerializeField]
    Text _displayOn;
    #endregion

    #region Properties
    public UnityEvent ScoreChange { get { return _ScoreChange; } }
    public int score 
    { 
        get { return _score; } 
        set
        {
            _score =                        value;
            Refresh();
            ScoreChange.Invoke();
        }
        
    }

    public Text displayOn
    {
        get                                 { return _displayOn; }
    }

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();
        Refresh();
    }

    public void Refresh()
    {
        if (displayOn != null)
            displayOn.text =                    "Score: " + score.ToString();
        else
            Debug.LogWarning(this.name + " needs a text field to display the score on!");
    }
    #endregion
}
