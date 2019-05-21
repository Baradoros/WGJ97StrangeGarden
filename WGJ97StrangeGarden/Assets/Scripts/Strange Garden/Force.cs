using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Force : MonoBehaviour
{
    [SerializeField]
    Vector3 _coords;

    public Vector3 coords                   { get { return _coords; } set { _coords = value; } }

    public void ApplyTo(Rigidbody rigidbody)
    {
        rigidbody.AddForce(coords);
    }

    public void ApplyTo(Rigidbody2D rigidbody)
    {
        rigidbody.AddForce(coords);
    }
}
