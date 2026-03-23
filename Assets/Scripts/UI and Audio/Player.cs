using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public LevelManager lm;
    Rigidbody rb;
    public int score;
    public int highScore;
    public TMP_Text scoreText;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore");
        }
        else
        {
            PlayerPrefs.SetInt("HighScore", 10000);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float xvel, zvel;
        xvel = rb.linearVelocity.x;
        zvel = rb.linearVelocity.z;
        /*
        if (Input.GetKey("w"))
        {
            xvel = 5f;
        }

        if (Input.GetKey("s"))
        {
            xvel = -5f;
        }
        */
        if (Input.GetKey("d"))
        {
            zvel = 12.5f;
        }

        if (Input.GetKey("a"))
        {
            zvel = -12.5f;
        }

        rb.linearVelocity = new Vector3(xvel, rb.linearVelocity.y, zvel);

        if (LevelManager.instance.playerHealth == 0)
        {
            FindFirstObjectByType<AudioManager>().Play("PlayerDeath");
            SceneManager.LoadScene("Main Menu");
        }

        if (score >= highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", score);
        }

        scoreText.text = score.ToString();
    }
}


