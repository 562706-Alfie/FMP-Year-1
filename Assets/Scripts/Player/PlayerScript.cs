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
    public float jumpSpeed, rightClickSpeedUp, rightClickDiveSpeed, xvel, yvel, speedLimit;
    public bool inputSpeedUp, inputDiveDown, inputBallJump;
    public LayerMask groundLayer;
    public Vector3 pos;
    public GameObject shadow;

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

        //Draws a shadow directly underneath the player
        Vector2 shadowPositionDown = transform.position;
        Vector2 shadowDirectionDown = Vector2.down;
        float shadowDistanceDown = 999f;

        RaycastHit2D shadowDown = Physics2D.Raycast(shadowPositionDown, shadowDirectionDown, shadowDistanceDown, groundLayer);

        if(shadowDown.collider != null)
        {
            shadow.transform.position = shadowDown.point;
        }

        // Makes the ball speed up, right click, grounded
        if (Input.GetKey(KeyCode.Mouse1) && IsGrounded())
        {
            inputSpeedUp = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) || !IsGrounded())
        {
            inputSpeedUp = false;
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);


        // Makes the ball Jump, left click, grounded
        if (Input.GetKey(KeyCode.Mouse0) && IsGrounded())
        {
            inputBallJump = true;
            //yvel = jumpSpeed; // Doesn't work in FixedUpdate for some reason
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) || !IsGrounded())
        {
            inputBallJump = false;
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);

        // Makes the ball spin, left click, air
        // Depends on whether I add skis


        // Makes the ball dive down, right click, air
        if (Input.GetKey(KeyCode.Mouse1) && !IsGrounded ())
        {
            inputDiveDown = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse1) || IsGrounded())
        {
            inputDiveDown = false;
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);
    }

    public void FixedUpdate()
    {
        // Makes the ball speed up, right click, grounded
        if (inputSpeedUp == true)
        {
            rb.AddForce(transform.right * rightClickSpeedUp/* * Time.deltaTime*/, ForceMode2D.Force);
        }

        // Makes the ball Jump, left click, grounded
        if (inputBallJump == true)
        {
            yvel = jumpSpeed;
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);

        // Makes the ball dive down, right click, air
        if (inputDiveDown == true)
        {
            rb.AddForce(-transform.up * rightClickDiveSpeed, ForceMode2D.Force); //Dives the player down
        }
    }
}

