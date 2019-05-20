using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SeedLauncher : MonoBehaviour
{
    // Sound
    private AudioSource shootSound;
    // Touch Listener
    public bool isTouched = false;
    public bool isKeyPress = false;
    // Ready for Launch
    public bool isActive = true;

    // Timers
    private float pressTime = 0f;
    private float startTime = 0f;
    private int powerIndex;
    private float force = 0f;
    private SpringJoint2D springJoint;
    public float maxforce = 90f;
    public float maxPowerIndex = 8000f;

    void Start()
    {
        springJoint = GetComponent<SpringJoint2D>();
        springJoint.distance = 1f;
    }

    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown("space"))
            {
                isKeyPress = true;
            }

            if (Input.GetKeyUp("space"))
            {
                isKeyPress = false;
            }

            // keyboard press or touch hotspot
            if (isKeyPress == true && isTouched == false || isKeyPress == false && isTouched == true)
            {
                if (startTime == 0f)
                {
                    startTime = Time.time;
                }
            }

            // keyboard release 	
            if (isKeyPress == false && isTouched == false && startTime != 0f)
            {
                Debug.Log("launch this");
                Debug.Log(startTime);
                if(powerIndex * maxforce <= 8000)
                {
                    force = powerIndex * maxforce;
                }
                else
                {
                    force = 8000;
                }
                Debug.Log("launched at force = " + force);
                // reset timer & animation
                pressTime = 0f;
                startTime = 0f;
                while (powerIndex >= 0)
                {
                    powerIndex--;
                }
            }
        }

        // Start Press
        if (startTime != 0f)
        {
            pressTime = Time.time - startTime;
            powerIndex = (int)(pressTime * 15);
        }
    }

    void FixedUpdate()
    {
        if (force != 0)
        {
            springJoint.distance = 1f;
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * force);
            Debug.Log(Vector3.up * force);
            force = 0;
        }

        if (pressTime != 0)
        {
            springJoint.distance = .8f;
            GetComponent<Rigidbody2D>().AddForce(Vector3.down * 400);
        }
    }
}
