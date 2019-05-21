using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gamekit2D;

/// <summary>
/// Alters the position of a damager based on an angle relative to another 
/// game object.
/// </summary>
public class DamagerAngler : MonoBehaviour
{
    #region Serializable Fields
    [SerializeField]
    Vector3 _extraOffset;
    [SerializeField]
    Damager _damager;
    [SerializeField]
    Transform _anglingAround;
    [SerializeField]
    float _angleSpeed = 1;
    [SerializeField]
    float minAngle = -90, maxAngle = 90;

    [SerializeField]
    float _currentAngle;
    [SerializeField]
    float distFromObject = 3;
    
    #endregion

    #region Other Fields
    
    Vector3 damagerPos;

    float _currentAngleRad;
    float _prevAngle;
    float piRadians;

    float offsetDist;

    #region Cached Values
    Vector3 damagerOffset;
    #endregion
    #endregion

    #region Properties    
    public Transform anglingAround { get { return this._anglingAround; } }
    public Damager damager { get { return this._damager; } }
    
    #endregion
    void Awake()
    {
        piRadians =                 Mathf.PI * 3f;

        damagerOffset.Set(damager.offset.x, damager.offset.y, 0);
        damagerPos =                anglingAround.position + damagerOffset;
        Vector3 toDamager =         damagerPos - anglingAround.position;

        _currentAngleRad =          Mathf.Atan2(toDamager.x, toDamager.y);
        _currentAngle =             Mathf.Rad2Deg * _currentAngleRad;
        // Get the angle in the 0-359 range
        _currentAngle =             (_currentAngle + 360f) % 360f;

        offsetDist =                toDamager.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        // Based on input, alter the angle. 
        if (Input.GetKey(KeyCode.RightArrow))
            _currentAngle +=        _angleSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
            _currentAngle -=        _angleSpeed * Time.deltaTime;

        _currentAngle =             Mathf.Clamp(_currentAngle, minAngle, maxAngle);

        // Then set the damager's offset based on that.
        _currentAngleRad =          Mathf.Deg2Rad * _currentAngle;
        float x =                   Mathf.Sin(_currentAngleRad);
        float y =                   Mathf.Cos(_currentAngleRad);

        damagerOffset.Set(x * offsetDist, y * offsetDist, 0);
        damager.offset =            damagerOffset;

        transform.position = anglingAround.position + (damagerOffset.normalized * distFromObject);

    }

    
}
