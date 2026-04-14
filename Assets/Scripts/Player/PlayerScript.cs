using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody2D rb;
    float _currentTime = 0;
    public LayerMask groundLayer;
    public float horizontalSpeed = 10f;
    public float jumpSpeed = 10f;
    public float rightClickSpeedUp = 10f;
    public float yvelDiveSpeed = 30f;
    public Vector3 pos;
    public float xvel, yvel;
    public float speedLimit;

    bool IsGrounded()
    {
        Vector2 positionDown = transform.position;
        Vector2 directionDown = Vector2.down;
        float distanceDown = 1f;
       
        RaycastHit2D hitDown = Physics2D.Raycast(positionDown, directionDown, distanceDown, groundLayer);

        if (hitDown.collider != null)
        {
            return true;
        }

        return false;

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;

        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;

        if (xvel >= speedLimit)
        {
            xvel = speedLimit;
        }
        _currentTime = _currentTime - Time.deltaTime;

        // Ball forward velocity
        if (Input.GetKeyDown(KeyCode.Mouse1) == false && IsGrounded()) // Only go forward at set velocity if player is not trying to speed up and is grounded
        {
            //xvel = horizontalSpeed;
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);

        // Makes the ball speed up, right click, grounded
        if (Input.GetKey(KeyCode.Mouse1) && IsGrounded())
        {
            rb.AddForce(transform.right * rightClickSpeedUp, ForceMode2D.Force);
            print("Speeding up");
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);

        // Makes the ball Jump, left click, grounded
        if (Input.GetKey(KeyCode.Mouse0) && IsGrounded())
        {
            yvel = jumpSpeed;
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);

        // Makes the ball spin, left click, air
        // Depends on whether I add skis


        // Makes the ball dive down, right click, air
        if (Input.GetKey(KeyCode.Mouse1) && !IsGrounded ())
        {
            rb.AddForce(-transform.up * yvelDiveSpeed, ForceMode2D.Force); //Dives the player down
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);


    }
}

