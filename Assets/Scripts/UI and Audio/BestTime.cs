using TMPro;
using UnityEngine;

public class BestTime : MonoBehaviour
{
    public float bestTime;
    public TextMeshProUGUI bestTimeText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
        }
        else
        {
            PlayerPrefs.SetFloat("BestTime", 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bestTimeText.text = "Best Time: " + bestTime.ToString("0.00") + "s";

    }
}
