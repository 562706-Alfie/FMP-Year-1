using TMPro;
using UnityEngine;

public class Health : MonoBehaviour
{
    public LevelManager lm;
    public TMP_Text healthText;


    void Update()
    {
        healthText.text = "Health:" + LevelManager.instance.playerHealth;
    }
}
