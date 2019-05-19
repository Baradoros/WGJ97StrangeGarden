using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Data container for the plants that can have some special effect on the terrain.
/// </summary>
public abstract class GardenFlower : ScriptableObject
{
    // TODO: Garden Flower Stuff
    [SerializeField]
    Sprite _sprite;
    public Sprite sprite { get { return this._sprite; } }

    #region Methods
    

    #endregion
}
