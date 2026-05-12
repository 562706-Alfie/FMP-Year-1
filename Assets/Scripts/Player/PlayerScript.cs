using Ilumisoft.HealthSystem.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    // With the new input system, don't really need to make a new input action thingy, 

    Rigidbody2D rb;

    public float jumpSpeed, rightClickSpeedUp, rightClickDiveSpeed, xvel, yvel, speedLimit, timeUntilXvelIncrease, diveSpeedIncreaseAmount, speedUpIncreaseAmount, timeUnitilPlayerControl;
    float timeUntilXvelIncreaseSave;
    public bool inputSpeedUp, inputDiveDown, inputBallJump, menuOpen;
    bool regenHealth, playerEntrance, setMaxHealth;

    public LayerMask groundLayer;
    public float xpos, ypos;

    public GameObject shadow, snowGroundParticleGenerator, pauseScreen;

    public GameManager gameManager;
    public ButtonScript ButtonScript;
    public HealthBar HealthBar;

    InputAction movement;
    InputAction menu;

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
        //xvel = 10;
        //yvel = 20;

        movement = InputSystem.actions.FindAction("Movement");
        menu = InputSystem.actions.FindAction("menu");

        menuOpen = false;
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
        if (timeUnitilPlayerControl <= 0)
        {
            // Makes the ball speed up, right click, grounded
            if (movement.IsPressed() && IsGrounded())
            {
                inputSpeedUp = true;
            }

            if (!movement.IsPressed() || !IsGrounded())
            {
                inputSpeedUp = false;
            }

            rb.linearVelocity = new Vector3(xvel, yvel, 0);

            // Makes the ball dive down, right click, air
            if (movement.IsPressed() && !IsGrounded())
            {
                inputDiveDown = true;
            }

            if (!movement.IsPressed() || IsGrounded())
            {
                inputDiveDown = false;
            }

            rb.linearVelocity = new Vector3(xvel, yvel, 0);

            if (menu.IsPressed() && menuOpen == false)
            {
                ButtonScript.PauseButton();
                ButtonScript.PlaySelect();
                pauseScreen.SetActive(true);
                menuOpen = true;
                print("Opening Menu");
            }
        }
    }

    public void FixedUpdate()
    {
        // Timer until player can use controls at the start of a game
        if (gameManager.finishedLoading == true)
        {
            timeUnitilPlayerControl -= Time.deltaTime;
            if (setMaxHealth == false)
            {
                gameManager.currentHealth = gameManager.maxHealth;
                HealthBar.SetMaxHealth(gameManager.maxHealth);
                setMaxHealth = true;
            }
        }

        // Boosts the player up at the start of the game AND if the game has finished loading
        if (playerEntrance == false && gameManager.finishedLoading == true)
        {
            xvel = 10;
            yvel = 20;
            playerEntrance = true;
        }
        
        if (playerEntrance == false && gameManager.finishedLoading == false)
        {
            xvel = 0;
            yvel = 0;
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

        // Makes the ball speed up, right click, grounded
        if (inputSpeedUp == true)
        {
            rb.AddForce(transform.right * rightClickSpeedUp/* * Time.deltaTime*/, ForceMode2D.Force); 
            //rb.linearVelocity = new Vector2(xvel * rightClickSpeedUp, rb.linearVelocity.y);           // NEW
        }

        rb.linearVelocity = new Vector3(xvel, yvel, 0);

        // Makes the ball dive down, right click, air
        if (inputDiveDown == true)
        {
            rb.AddForce(-transform.up * rightClickDiveSpeed, ForceMode2D.Force); //Dives the player down
        }

        // Regenerates health continuously
        if (regenHealth == true)
        {
            HealthBar.RegenerateHealth();
            //AudioManager.instance.Play("Healing");
            if (gameManager.currentHealth >= gameManager.maxHealth)
            {
                regenHealth = false;
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "respawnSquare")
        {
            ButtonScript.OpenDeathPanel();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "coin")
        {
            //print("Coin collided");
            gameManager.currentScore += 1;
            AudioManager.instance.Play("Coin");
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "healthRegen")
        {
            if(gameManager.currentHealth < gameManager.maxHealth)
            {
                regenHealth = true;
            }
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "healthPack")
        {
            //AudioManager.instance.Play("Coin");
            //gameManager.currentHealth += gameManager.healthPackHealthAmount;
            HealthBar.HealthPack();
            Destroy(collision.gameObject);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if(other.gameObject.tag == "")
        print("Collided with cloud");
    }
}

