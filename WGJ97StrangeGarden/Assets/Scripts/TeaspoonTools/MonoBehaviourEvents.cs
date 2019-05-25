using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace TeaspoonTools.Events
{
    public class BehaviourEvents 
    {
        public UnityEvent Destroyed                     { get; protected set; }
        public UnityEvent DestroyedImmediate            { get; protected set; }

        public UnityEvent Disabled                      { get; protected set; }
        public UnityEvent Enabled                       { get; protected set; }


    }


    public class MonoBehaviourEvents : BehaviourEvents
    {
        
        public EnumeratorEvent StartedCoroutine { get; protected set; }
        public EnumeratorEvent StoppedCoroutine { get; protected set; }

    }
}