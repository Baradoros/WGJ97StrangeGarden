using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDisableTrigger : MonoBehaviour
{
    [SerializeField]
    UnityEvent _Disable =                       new UnityEvent();
    public UnityEvent Disable
    {
        get                                     { return this._Disable; }
    }

    void OnDisable()
    {
        Disable.Invoke();
    }
}
