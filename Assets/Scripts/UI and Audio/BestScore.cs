using TMPro;
using UnityEngine;

public class BestScore : MonoBehaviour
{
    public int bestScore;
    public TextMeshProUGUI bestScoreText;
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
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bestScoreText.text = "Best Score: " + bestScore.ToString();
    }
}
