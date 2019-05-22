using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DisplayCountdown : MonoBehaviour
{
    GameObject displayOn;

    ITimerHandler countdown;

    void Awake()
    {
        // Make sure that the object to display on has a timer handler
        countdown =                             displayOn.GetComponent<ITimerHandler>();

        if (countdown == null)
        {
            Debug.LogError(this.name + " needs a ref to a game object with a component that implements ITimerHandler.");
        }

    }
    
}
