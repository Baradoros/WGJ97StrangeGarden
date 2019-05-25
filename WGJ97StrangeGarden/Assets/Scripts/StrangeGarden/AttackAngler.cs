using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TeaspoonTools;


public class AttackAngler : MonoBehaviour2D
{
    #region Serializable Fields
    [Header("Essential components")]
    [SerializeField] Transform _orbiter;
    [SerializeField] Transform _orbitee;

    [Header("Settings for the angling")]
    [SerializeField] bool _useArrowKeys =       true;
    [SerializeField] bool _useMouse =           true;
    [SerializeField] float _minAngle =           -90;
    [SerializeField] float _maxAngle =          90;
    [Tooltip("Distance between the orbiter and orbitee.")]
    [SerializeField] float _distance =          2;
    [Tooltip("How many degrees around the orbitee, that the orbiter can move when directed by the arrow keys.")]
    [SerializeField] float _orbitSpeed =        160;

    [Header("Just for viewing")]
    [SerializeField] float _currentAngle; // In degrees
    #endregion

    #region Properties

    public Transform orbiter                    { get { return _orbiter; } }
    public Transform orbitee                    { get { return _orbitee; } }
    public bool useArrowKeys                    { get { return _useArrowKeys; } }
    public bool useMouse                        { get { return _useMouse; } }
    public float minAngle                       { get { return _minAngle; } }
    public float maxAngle                       { get { return _maxAngle; } }
    public float distance 
    {
        get                                     { return _distance; }
        set                                     { _distance = value; }
    }

    public float currentAngle                   
    { 
        get                                     { return _currentAngle; } 
        protected set                           { _currentAngle = value; }
    }
    #endregion

    #region Methods
    protected override void Awake()
    {
        base.Awake();
        Vector3 toOrbiter = orbiter.position - orbitee.position;
        UpdateAngle(toOrbiter);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Time.timeScale == 0)
            return;
            
        HandleArrowKeyControls();
        HandleMouseControls();
        //KeepAngleWithinLimits();
    }

    void LateUpdate()
    {
        UpdateOrbiterPosition();
    }

    void HandleArrowKeyControls()
    {
        if (!_useArrowKeys)
            return;

        if (Input.GetKey(KeyCode.RightArrow))
            currentAngle +=                     _orbitSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
            currentAngle -=                     _orbitSpeed * Time.deltaTime;


    }

    void HandleMouseControls()
    {
        if (!_useMouse)
            return;

        // Based on the mouse and orbitee positions, set the angle.
        // This needs the camera to be orthographic.
        Vector3 mousePos =                      Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 toMouse =                       mousePos - orbitee.position;
        UpdateAngle(toMouse);
    }

    void KeepAngleWithinLimits()
    {
        // TODO: fix the algorithm
        // Make sure the angle is within the bounds of both the positive (and negative) equivalents
        // of the angle limiters. Note that by "equivalent", we mean equivalent on the unit circle, not 
        // necessarily just in the sign.

        float reversedAngle =                   0;

        if (currentAngle > maxAngle)
        {
            reversedAngle =                     currentAngle - 360f;
            if (reversedAngle < minAngle)
            {
                float sign =                    1;

                if (useMouse)
                    sign =                      Mathf.Sign(orbiter.position.x - orbitee.position.x);
                currentAngle =                  Mathf.Clamp(reversedAngle * sign, minAngle, maxAngle);
            }
        }

        /*
        if (currentAngle < minAngle)
        {
            reversedAngle =                     currentAngle + 360f;
            if (reversedAngle > maxAngle)
                currentAngle = maxAngle;
        }

        */

    }

    void UpdateAngle(Vector3 vec)
    {
        // Based on the passed vector, decide the angle it'd make if placed on the unit circle, starting at the 
        // origin
        float newAngle =                        Mathf.Atan2(vec.x, vec.y) * Mathf.Rad2Deg;

        // Keep the angle in the 0-359 range
        newAngle =                              (newAngle + 360f) % 360f;
        currentAngle =                          newAngle;

    }

    void UpdateOrbiterPosition()
    {
        // Based on the angle, set the orbiter's position
        float angleRads =                   Mathf.Deg2Rad * currentAngle;
        float xOffset =                     Mathf.Sin(angleRads);
        float yOffset =                     Mathf.Cos(angleRads);
        Vector3 offset =                    new Vector3(xOffset, yOffset, 0) * distance;

        orbiter.position =                  orbitee.position + offset;
    }

    #endregion
}
