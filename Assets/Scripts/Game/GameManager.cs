using Ilumisoft.HealthSystem;
using Ilumisoft.HealthSystem.UI;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    // Use this script to manage the timer, score, and player health

    public int damageThreshold, randomNumber, collectableSpawnChance, coinSpawnAirChance, currentScore;
    int difficulty, smoothLandingMultiplier, globalCoinsToSpawn;
    public float xvelPrevious, damageValue = 100, maxHealth = 100, currentHealth, speedCheckDelay, timerHealthRegenerate, rateHealthRegenerate, timer, tileManagerDifference, healthPackHealthAmount, SmoothLandingPopUpTimer;
    float healthRegenerateTimerReturn;
    public Vector2 tileManagerOldPos, tileManagerNewPos;
    public bool damageTaken, inAir, playHealingSFX, finishedLoading;
    bool deathPanelOpen = false, hasSmoothLandingMultiplierTextPoppedUp, hasBadLandingPoppedUp, hasText1PoppedUp, hasText2PoppedUp, coinSpawnDelay, coinSpawnChance;

    public static GameManager GameManagerinstance;

    public GameObject coin;
    public GameObject healthRegenCollectable;
    public GameObject healthPackCollectable;
    public GameObject PopUpTextLocation;
    public GameObject PopUpText;
    public GameObject tileManager;
    public GameObject Loading, PauseButton;

    public HealthBar healthBar;
    public ButtonScript ButtonScript;
    public PlayerScript playerScript;
    public TileManager TileManager;
    public Sound[] sound;

    public TextMeshProUGUI timerText, scoreText, gameOverScoreText, gameOverTimerText;

    public CollectableSpawner CollectableSpawner;

    //Turning "damageThreshold" up decreases the chance of smaller jumps dealing damage
    // coinSpawnChance, lower is higher chance

    void Start()
    {
        finishedLoading = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        damageTaken = false;
        inAir = false;;
        coinSpawnAirChance = collectableSpawnChance / 2; // Gives half a chance for coins to spawn in the air instead
        tileManagerOldPos = tileManager.transform.position;
        playHealingSFX = true;
        difficulty = PlayerPrefs.GetInt("Difficulty");
        damageThreshold = difficulty;
        smoothLandingMultiplier = 0;
        hasSmoothLandingMultiplierTextPoppedUp = false;
        //hasBadLandingPoppedUp = false;
        SmoothLandingPopUpTimer = 0f;
        hasText1PoppedUp = false;
        hasText2PoppedUp = false;
        coinSpawnChance = false;
    }

    void Update()
    {
        // Timer
        if (finishedLoading == true)
        {
            timer += Time.deltaTime;
            timerText.text = "Time: " + timer.ToString("0.00") + "s";
            gameOverTimerText.text = "Time: " + timer.ToString("0.00") + "s";
        }

        // Best Time
        if (timer > PlayerPrefs.GetFloat("BestTime"))
        {
            PlayerPrefs.SetFloat("BestTime", timer);
        }

        // Score
        scoreText.text = ("Score: ") + currentScore.ToString();
        gameOverScoreText.text = ("Score: ") + currentScore.ToString();

        // Best Score
        if (currentScore > PlayerPrefs.GetInt("BestScore"))
        {
            PlayerPrefs.SetInt("BestScore", currentScore);
        }

        // Smooth landing timer
        SmoothLandingPopUpTimer += Time.deltaTime;

        // If health is at 0, Open death panel
        if (currentHealth < 0 && deathPanelOpen == false)
        {
            ButtonScript.OpenDeathPanel();
            deathPanelOpen = true;
        }

        // Gives score and displays text based on the height of the player
        // First text
        if (playerScript.ypos > 47f && hasText1PoppedUp == false)
        {
            GameObject popUp = Instantiate(PopUpText, PopUpTextLocation.transform.position, Quaternion.identity);
            popUp.GetComponentInChildren<TMP_Text>().text = ("SKY HIGH!                                                          +2");
            currentScore += +2;
            hasText1PoppedUp = true;
        }
        if (playerScript.ypos < 46f)
        {
            hasText1PoppedUp = false;
        }

        // Second Text
        if (playerScript.ypos > 110f && hasText2PoppedUp == false)
        {
            GameObject popUp = Instantiate(PopUpText, PopUpTextLocation.transform.position, Quaternion.identity);
            popUp.GetComponentInChildren<TMP_Text>().text = ("ABOVE THE CLOUDS!                                        +4");
            currentScore += +4;
            hasText2PoppedUp = true;
        }
        if (playerScript.ypos < 109f)
        {
            hasText2PoppedUp = false;
        }

        // Smooth landing
        if (damageValue < 0 && speedCheckDelay > 0.05 && hasSmoothLandingMultiplierTextPoppedUp == false && playerScript.IsGrounded() && SmoothLandingPopUpTimer >= 1f)
        {
            SmoothLandingPopUpTimer = 0f; // Prevents multiple pop ups when bouncing up and down off the ground too quickly
            smoothLandingMultiplier += 1; // +1 to the "multiplier"
            GameObject popUp = Instantiate(PopUpText, PopUpTextLocation.transform.position, Quaternion.identity);
            popUp.GetComponentInChildren<TMP_Text>().text = ("SMOOTH LANDING!                                        +" + smoothLandingMultiplier);
            currentScore += smoothLandingMultiplier;
            hasSmoothLandingMultiplierTextPoppedUp = true; // Stops multiple from coming up(every 1 second) when staying on the ground
            speedCheckDelay = 0; // Needs to be here, not in damage check
        }
        if (!playerScript.IsGrounded())
        {
            hasSmoothLandingMultiplierTextPoppedUp = false;
        }
        if (damageValue > 0)
        {
            smoothLandingMultiplier = 0;
        }

        // Pop Up for speed and getting a certain score?


        /*
        if (damageValue > 0 && hasBadLandingPoppedUp == false && playerScript.IsGrounded())
        {
            smoothLandingMultiplier = 0;
            GameObject popUp = Instantiate(PopUpText, PopUpTextLocation.transform.position, Quaternion.identity);
            popUp.GetComponentInChildren<TMP_Text>().text = ("Bad Landing. . .                   ");
            hasBadLandingPoppedUp = true;
        }
        if (!playerScript.IsGrounded())
        {
            hasBadLandingPoppedUp = false;
        }
        */




        //Timer unitl health regenerates
        /*
        if (timerHealthRegenerate < 0 && currentHealth <= 100)
        {
            healthBar.RegenerateHealth();
            if (playHealingSFX == true && currentHealth < maxHealth)
            {
                AudioManager.instance.Play("Healing");
                playHealingSFX = false;
            }

            if (currentHealth >= maxHealth)
            {
                playHealingSFX = true;
            }

            //Do the healing sfx looping and ending in playerscript since theres already stuff there
        }
        */

        /*
        Sound.sound.loop = false;
        s.loop = true;

        foreach (Sound s in sound)
        {
            if (s.name == "MainTheme")
            {
                s.loop = true;   
                s.source.loop = s.loop;
            }
        }
        */

        // When the player hits the ground, need to compare speed they were going(xvelPrevious) vs speed they are now(xvel). Remove health based on that difference
        if (!playerScript.IsGrounded())
        {
            if (inAir == false)
            {
                xvelPrevious = playerScript.xvel;
                damageTaken = false;
                inAir = true;
            }
        }

        if (playerScript.IsGrounded() && damageTaken == false)
        {
            speedCheckDelay = speedCheckDelay + Time.deltaTime;
            if(speedCheckDelay > 0.05)
            {
                damageValue = xvelPrevious - playerScript.xvel - damageThreshold;
                damageTaken = true;
                inAir = false;
                if(damageValue > 0)
                {
                    // Take health away eqaul to speed lost
                    int roundedDamageValue = Convert.ToInt32(Mathf.Round(damageValue));
                    takeDamage(roundedDamageValue);
                }
                else
                {
                    if (damageValue < -30)
                    {
                        // Add 1 to score, encourages smooth landing
                        //currentScore += 1;
                    }
                }
            }
        }
    }


    void FixedUpdate()
    {
        // This allows tiles to be generated at the start of the game, and prevents player movement until it is done
        if (finishedLoading == false) // Bigger number means more tiles will be spawned in
        {
            Loading.SetActive(true);
            PauseButton.SetActive(false);
            if (TileManager.tilesSpawned > 6)
            {
                Loading.SetActive(false);
                PauseButton.SetActive(true);
                finishedLoading = true;
            }
        }

        //Generates a random number between 0 and coinSpawnChance, spawns in coins and abilites depepnding on if the right number is chosen
        tileManagerNewPos.x = tileManager.transform.position.x;
        tileManagerDifference = tileManagerNewPos.x - tileManagerOldPos.x;
        if (tileManagerDifference > 1)
        {
            randomNumber = UnityEngine.Random.Range(0, collectableSpawnChance);
            if (randomNumber <= 10)
            {
                print("Single Coin");
                CoinSpawner(1);
            }

            if (randomNumber >= 11 && randomNumber < 16)
            {
                print("10 Coins");
                CoinSpawner(10);
            }

            if(randomNumber == 17)
            {
                print("Health Regen");
                HealthRegenSpawner();
            }

            if (randomNumber == 18)
            {
                print("Health Pack");
                HealthPackSpawner();
            }
            coinSpawnChance = !coinSpawnChance;
            coinSpawnDelay = true;
            if (coinSpawnChance == false && globalCoinsToSpawn > 0)
            {
                CoinSpawner(globalCoinsToSpawn);
            }
            tileManagerOldPos = tileManagerNewPos;
            tileManagerDifference = 0;
        }
    }

    public void CoinSpawner(int coinsToSpawn)
    {
        if(coinSpawnDelay == true && coinsToSpawn > 0)
        {
            coinsToSpawn -= 1;
            globalCoinsToSpawn = coinsToSpawn;
            CollectableSpawner.spawnPoint = Instantiate(coin);
            print(coinsToSpawn);
            coinSpawnDelay = false;
        }
    }

    public void HealthRegenSpawner()
    {
        CollectableSpawner.spawnPoint = Instantiate(healthRegenCollectable);
    }
    public void HealthPackSpawner()
    {
        CollectableSpawner.spawnPoint = Instantiate(healthPackCollectable);
    }

    public void takeDamage(int damage)
    {
        timerHealthRegenerate = healthRegenerateTimerReturn;
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        AudioManager.instance.Play("Hit");
    }
}

//        clone.transform.position = CollectableSpawner.spawnerDown.point;
