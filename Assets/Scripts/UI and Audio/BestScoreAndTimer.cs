using TMPro;
using UnityEngine;

public class BestScoreAndTimer : MonoBehaviour
{
    public int bestScore;
    public float bestTime;
    public TextMeshProUGUI bestTimerText, bestScoreText;
    void Awake()
    {

        if (PlayerPrefs.HasKey("BestScore"))
        {
            bestScore = PlayerPrefs.GetInt("BestScore");
        }
        else
        {
            PlayerPrefs.SetInt("BestScore", 0);
        }

        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
        }
        else
        {
            PlayerPrefs.SetFloat("BestTime", 0f);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bestScoreText.text = ("Best Score: ") + bestScore.ToString();
        bestTimerText.text = ("Best Timer: ") + bestTime.ToString("0.00");
    }
}
