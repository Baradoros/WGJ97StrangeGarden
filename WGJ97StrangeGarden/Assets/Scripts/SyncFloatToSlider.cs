using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncFloatToSlider : MonoBehaviour
{
    [SerializeField] FloatVariable variable;
    [SerializeField] Slider slider;

    // Update is called once per frame
    void Update()
    {
        variable.value =                        slider.value;
    }
}
