using Ilumisoft.HealthSystem;
using Ilumisoft.HealthSystem.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Use this script to manage the timer, score, and player health

    public int maxHealth = 100, currentHealth, damageThreshold, randomNumber, coinSpawnChance, coinSpawnAirChance;
    public float xvelPrevious, damageValue = 100, speedCheckDelay, timerHealthRegenerate, rateHealthRegenerate;
    float healthRegenerateTimerReturn;
    public bool damageTaken, inAir;
    public GameObject coin;
    public GameObject clone;
    public HealthBar healthBar;
    public PlayerScript playerScript;
    public CollectableSpawner CollectableSpawner;

    //Turning "damageThreshold" up decreases the chance of smaller jumps dealing damage
    // coinSpawnChance, lower is higher chance

    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        damageTaken = false;
        inAir = false;;
        healthRegenerateTimerReturn = timerHealthRegenerate;
        coinSpawnAirChance = coinSpawnChance / 2; // Gives half a chance for coins to spawn in the air instead
    }

    void Update()
    {
        // If health is at 0, Reset the scene
        if(currentHealth < 0)
        {
            SceneManager.LoadScene("Main Scene");
        }

        //Timer unitl health regenerates
        timerHealthRegenerate -= Time.deltaTime;
        if (timerHealthRegenerate < 0)
        {
            healthBar.RegenerateHealth();
        }

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
                    int roundedDamageValue = Convert.ToInt32(Mathf.Round(damageValue));
                    takeDamage(roundedDamageValue);
                    timerHealthRegenerate = healthRegenerateTimerReturn;
                }
            }
        }
    }


    void FixedUpdate()
    {
        //Generates a random number between 0 and coinSpawnChance, spawns in coins and abilites depepnding on if the right number is chosen
        randomNumber = UnityEngine.Random.Range(0, coinSpawnChance);
        if (randomNumber == 1)
        {
            print("1");
            CoinSpawner(5);
        }
    }

    public void CoinSpawner(int coinsToSpawn)
    {
        CollectableSpawner.spawnPoint = Instantiate(coin);
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}

//        clone.transform.position = CollectableSpawner.spawnerDown.point;
