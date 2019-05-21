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
    [Header("Essential Components")]
    [SerializeField]
    Damager _damager;

    [SerializeField]
    Transform _anglingAround;

    [SerializeField]
    Transform _angleMarker;

    [Header("Properties of the angling")]
    [SerializeField]
    [Tooltip("Whether or not the right and left arrow keys can change the angle.")]
    bool _useArrowKeys =                            true;

    [SerializeField] 
    [Tooltip("Whether or not the angle is set based on the mouse position.")]
    bool _useMouse =                                true;

    [SerializeField]
    [Tooltip("How fast the angle changes when using the arrow keys.")]
    float _angleSpeed =                             1;

    [SerializeField]
    float minAngle = -90, maxAngle = 90;

    [SerializeField]
    float distFromObject =                          3;

    [Header("Just for viewing")]

    [SerializeField]
    float _currentAngle;
    
    #endregion

    #region Other Fields
    // These assist with setting the angle
    Vector3 damagerPos;
    

    #region Cached Values
    Vector3 damagerOffset;
    float piRadians;
    #endregion
    #endregion

    #region Properties    
    public Transform anglingAround { get { return this._anglingAround; } }
    public Damager damager { get { return this._damager; } }
    public float currentAngle
    {
        get { return _currentAngle; }
        set { _currentAngle = value; }
    }
    
    #endregion
    void Awake()
    {
        EnsureEssentialComponents();

        piRadians =                 Mathf.PI * 3f;

        // Set the initial angle based on the damager's position relative to whatever
        // game object the damager is on
        damagerOffset.Set(damager.offset.x, damager.offset.y, 0);
        damagerPos =                anglingAround.position + damagerOffset;

        // Remember, vec A - vec B = vec C. Vec C points from B, to A!
        Vector3 toDamager =         damagerPos - anglingAround.position;

        UpdateAngle(toDamager);
    }

    // Update is called once per frame
    void Update()
    {
        HandleArrowKeyControls();
        HandleMouseControls();
        UpdateDamagerOffset();
        UpdateMarkerPosition();
    }

    #region Helpers
    void HandleArrowKeyControls()
    {
        if (!_useArrowKeys)
            return;

        if (Input.GetKey(KeyCode.RightArrow))
            _currentAngle +=        _angleSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
            _currentAngle -=        _angleSpeed * Time.deltaTime;

        _currentAngle =             Mathf.Clamp(_currentAngle, minAngle, maxAngle);
    }

    void HandleMouseControls()
    {
        if (!_useMouse)
            return;

        // Get a vector going from the player to the mouse pos, and set the angle based on that.
        Vector3 mousePos =          Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        Vector3 toMouse =           mousePos - anglingAround.position;
        UpdateAngle(toMouse);

        Debug.DrawLine(anglingAround.position, toMouse, Color.green, Time.deltaTime);

    }

    void UpdateDamagerOffset()
    {
        float xOff =                Mathf.Sin(Mathf.Deg2Rad * _currentAngle);
        float yOff =                Mathf.Cos(Mathf.Deg2Rad * _currentAngle);

        damagerOffset.Set(xOff, yOff, 0);
        //damagerOffset =             damagerOffset.normalized * distFromObject;
        damager.offset =            damagerOffset;
    }

    void UpdateMarkerPosition()
    {
        _angleMarker.position =         anglingAround.position + (damagerOffset.normalized * distFromObject);
    }

    /// <summary>
    /// Update the rotation angle based on how the passed direction vector fits into the unit circle.
    /// </summary>
    void UpdateAngle(Vector3 dir)
    {
        float angleInRads =         Mathf.Atan2(dir.x, dir.y);
        _currentAngle =             Mathf.Rad2Deg * angleInRads;
        // Make sure the the angle is in the 0-359 range
        _currentAngle =             (_currentAngle + 360f) % 360f;

        // Then clamp that to this instance's angle range
        _currentAngle =             Mathf.Clamp(_currentAngle, minAngle, maxAngle);
    }

    void EnsureEssentialComponents()
    {
        if (_damager == null || _anglingAround == null || _angleMarker == null)
        {
            string message =                    this.name + @" needs refs to a damager, angle marker, 
            and object to set the angle around to work!";
            Debug.LogError(message);
        }
    }
    #endregion
}
