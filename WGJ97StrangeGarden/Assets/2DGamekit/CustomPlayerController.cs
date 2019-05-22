using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomPlayerController : MonoBehaviour
{
    Rigidbody2D player;
    public float fallMultiplier;
    public float lowJumpMultiplier;
    public float jumpVelocity;

    public float runSpeed;

    // Start is called before the first frame update
    void Awake()
    {
        player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Old code
        //Vector2 move = new Vector2(Input.GetAxis("Horizontal") * 20, 0);
        // player.AddForce(move);

        // get the direction it must walk in:
        var speed = new Vector2(Input.GetAxis("Horizontal"), 0).normalized;
        // convert from local to world space and multiply by horizontal speed:
        speed = runSpeed * transform.TransformDirection(speed);
        // keep rigidbody vertical velocity to preserve gravity action:
        speed.y = player.velocity.y;
        // set new rigidbody velocity:
        player.velocity = speed;

        //Jumping
        if (Input.GetButtonDown("Jump"))
        {
            player.velocity = Vector2.up * jumpVelocity;
        }

        //If the player reaches the top of their jump apply the normal fall multiplier
        if (player.velocity.y < 0)
        {
            player.velocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.fixedDeltaTime;
        }
        //If the player lets go of the jump button prematurely then they are hit with the low jump multiplyer plus gravity designed to pull them down faster
        else if (player.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            player.velocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.fixedDeltaTime;
        }

    }
}
