using TMPro;
using UnityEngine;

public class HighScore : MonoBehaviour
{
    public Player pl;
    public TMP_Text highScoreText;

    void Update()
    {
        highScoreText.text = "High Score:" + pl.highScore.ToString();
    }
}
