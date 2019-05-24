using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveToggler : MonoBehaviour
{
    [SerializeField] GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleActive()
    {
        target.SetActive(!target.activeSelf);
    }
}
