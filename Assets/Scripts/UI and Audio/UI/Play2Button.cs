using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Play2Button : MonoBehaviour
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
        audioSource.PlayOneShot(sfx1, 0.7f); // play audio clip with volume 0.7
    }

    public void ButtonMethod()
    {
        FindFirstObjectByType<AudioManager>().Play("MenuSelect");
        SceneManager.LoadScene("Level 2");
        LevelManager.instance.playerHealth = 10;
    }
}