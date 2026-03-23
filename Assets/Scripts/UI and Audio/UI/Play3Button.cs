using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play3Button : MonoBehaviour
{
    //public variable to reference the button text - set this up in the Unity editor
    public TMP_Text playText;

    AudioSource audioSource;
    public AudioClip sfx1;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void PlaySoundEffect()
    {

    }

    public void ButtonMethod()
    {
        FindFirstObjectByType<AudioManager>().Play("MenuSelect");
        SceneManager.LoadScene("Level 3");
        LevelManager.instance.playerHealth = 10;
    }
}