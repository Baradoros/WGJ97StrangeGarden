using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Data container for the various seed types in the game.
/// </summary>
[CreateAssetMenu(menuName = "StrangeGarden/GardenSeed", fileName = "NewGardenSeed")]
public class GardenSeed : ScriptableObject
{
    // TODO: Garden Seed Stuff
    #region Serializable Fields
    [SerializeField]
    Sprite _sprite;

    [SerializeField]
    GardenFlower _sproutsInto;

    #endregion

    #region Properties
    public Sprite sprite { get { return this._sprite; } }
    public GardenFlower sproutsInto { get { return this._sproutsInto; } }
    #endregion
}
