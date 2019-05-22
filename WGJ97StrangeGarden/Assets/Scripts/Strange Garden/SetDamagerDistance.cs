using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;

public class SetDamagerDistance : MonoBehaviour
{
    [SerializeField]
    Damager _damager;

    [SerializeField]
    float _distance;

    public float distance               { get { return _distance; } set { _distance = value; } }

    void LateUpdate() // So this executes after normal changes to the damager's offset
    {
        _damager.offset.Normalize();
        _damager.offset *=              distance;
    }

    
}
