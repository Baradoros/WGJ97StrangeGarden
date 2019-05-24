using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncVolumeToVariable : MonoBehaviour
{
    [SerializeField] FloatVariable variable;
    [SerializeField] AudioSource audioSource;

    // Update is called once per frame
    void Update()
    {
        audioSource.volume =                    variable.value;
    }
}
