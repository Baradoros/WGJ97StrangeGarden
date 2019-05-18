using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TeaspoonTools.UI;

/// <summary>
/// Controls the part of the UI that displays a garden seed on an Image component.
/// </summary>
public class SeedDisplayHUD : UIElementController
{
    #region Serializable Fields
    [SerializeField]
    GardenSeed _seed;
    [SerializeField]
    [Tooltip("Image component displaying the seed's sprite.")]
    Image _displayOn;

    #endregion

    #region Properties
    public GardenSeed seed                  
    { 
        get { return _seed; } 
        set 
        {
            // Make sure to refresh the image when the seed to display changes.
            _seed =         value;

            Refresh();
        }
    }
    public Image displayOn                  { get { return _displayOn; } }
    

    #endregion


    #region Methods

    protected override void Awake()
    {
        base.Awake();
        Refresh();
    }

    public void Refresh()
    {
        if (seed != null)
            displayOn.sprite =              seed.sprite;
        else
            Debug.LogWarning(this.name + " has no garden seed to display the image of!");

    }

    #endregion
}
