using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D player;

    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        player.AddForce(new Vector2(Input.GetAxis("Horizontal") * 2 , Input.GetAxis("Vertical" ) * 2 ) );
    }
}
