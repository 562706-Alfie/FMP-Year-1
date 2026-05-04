using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody2D rb;

    public float jumpSpeed, rightClickSpeedUp, rightClickDiveSpeed, xvel, yvel, speedLimit, timeUntilXvelIncrease, diveSpeedIncreaseAmount, speedUpIncreaseAmount;
    float timeUntilXvelIncreaseSave;
    public bool inputSpeedUp, inputDiveDown, inputBallJump;

    public LayerMask groundLayer;
    public float xpos, ypos;

    public GameObject shadow, snowGroundParticleGenerator;

    public GameManager gameManager;
    public ButtonScript ButtonScript;


    public bool IsGrounded()
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
        timeUntilXvelIncreaseSave = timeUntilXvelIncrease;
    }

    // Update is called once per frame
    void Update()
    {
        xpos = transform.position.x;
        ypos = transform.position.y;

        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;

        if (xvel >= speedLimit)
        {
            xvel = speedLimit;
        }

        {
            
        }

        // After X seconds, increases the dive speed and speed up speed of the player
        timeUntilXvelIncrease = timeUntilXvelIncrease - Time.deltaTime;
        if (timeUntilXvelIncrease < 0)
        {
            print("Speeding Up");
            rightClickDiveSpeed = rightClickDiveSpeed + diveSpeedIncreaseAmount;
            rightClickSpeedUp = rightClickSpeedUp + speedUpIncreaseAmount;
            timeUntilXvelIncrease = timeUntilXvelIncreaseSave;
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

        // If player is on the ground, enable SnowGroundParticleGenerator, which spawns small snow particles on the ground
        if (IsGrounded())
        {
            snowGroundParticleGenerator.SetActive(true);
        }
        else
        {
            snowGroundParticleGenerator.SetActive(false);
        }
        
        // Movement 
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "respawnSquare")
        {
            AudioManager.instance.Play("Death");
            ButtonScript.OpenDeathPanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            AudioManager.instance.Play("Coin");
            Destroy(collision.gameObject);
            gameManager.currentScore += 1;
        }
    }

}

