using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TeaspoonTools.UI;

public class FloatDisplayer : UIElementController
{
    [SerializeField] FloatReference toDisplay;
    [SerializeField] Text textField;

    private void Start()
    {
        toDisplay.ValueChanged.AddListener(OnValueChanged);
        Refresh();
    }

    protected void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        textField.text =                    toDisplay.value.ToString();
    }

    void OnValueChanged(float newValue)
    {
        Refresh();
    }

}
