using Ilumisoft.HealthSystem;
using Ilumisoft.HealthSystem.UI;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Use this script to manage the timer, score, and player health

    public int damageThreshold, randomNumber, collectableSpawnChance, coinSpawnAirChance, currentScore;
    public int difficulty;
    public float xvelPrevious, damageValue = 100, maxHealth = 100, currentHealth, speedCheckDelay, timerHealthRegenerate, rateHealthRegenerate, timer, tileManagerDifference, healthPackHealthAmount;
    float healthRegenerateTimerReturn;
    public Vector2 tileManagerOldPos, tileManagerNewPos;
    public bool damageTaken, inAir, playHealingSFX;
    bool deathPanelOpen = false;

    public static GameManager GameManagerinstance;

    public GameObject coin;
    public GameObject healthRegenCollectable;
    public GameObject healthPackCollectable;
    public GameObject clone;
    public GameObject tileManager;

    public HealthBar healthBar;
    public ButtonScript ButtonScript;
    public Sound[] sound;

    public TextMeshProUGUI timerText, scoreText;

    public PlayerScript playerScript;
    public CollectableSpawner CollectableSpawner;

    //Turning "damageThreshold" up decreases the chance of smaller jumps dealing damage
    // coinSpawnChance, lower is higher chance

    void Awake()
    {
        Time.timeScale = 1.0f;
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        damageTaken = false;
        inAir = false;;
        coinSpawnAirChance = collectableSpawnChance / 2; // Gives half a chance for coins to spawn in the air instead
        tileManagerOldPos = tileManager.transform.position;
        playHealingSFX = true;
        difficulty = PlayerPrefs.GetInt("Difficulty");
        damageThreshold = difficulty;
    }

    void Update()
    {
        //Timer
        timer += Time.deltaTime;
        timerText.text = ("Timer: ") + timer.ToString("0.00");

        //Score
        scoreText.text = ("Score: ") + currentScore.ToString();

        // If health is at 0, Open death panel
        if(currentHealth < 0 && deathPanelOpen == false)
        {
            ButtonScript.OpenDeathPanel();
            deathPanelOpen = true;
        }

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

            //Do the healing sfx looping and ending here?
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
                speedCheckDelay = 0;
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
                        currentScore += 1;
                    }
                }
            }
        }
    }


    void FixedUpdate()
    {
        //Generates a random number between 0 and coinSpawnChance, spawns in coins and abilites depepnding on if the right number is chosen
        tileManagerNewPos.x = tileManager.transform.position.x;
        tileManagerDifference = tileManagerNewPos.x - tileManagerOldPos.x;
        if (tileManagerDifference > 1)
        {
            randomNumber = UnityEngine.Random.Range(0, collectableSpawnChance);
            if (randomNumber <= 10)
            {
                print("Coin");
                CoinSpawner(5);
            }

            if(randomNumber == 10)
            {
                print("Health Regen");
                HealthRegenSpawner();
            }

            if (randomNumber == 11)
            {
                print("Health Pack");
                HealthPackSpawner();
            }
            tileManagerOldPos = tileManagerNewPos;
            tileManagerDifference = 0;
        }
    }

    public void CoinSpawner(int coinsToSpawn)
    {
        CollectableSpawner.spawnPoint = Instantiate(coin);
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
