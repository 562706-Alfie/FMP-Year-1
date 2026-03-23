using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayButton : MonoBehaviour
{
    //public variable to reference the button text - set this up in the Unity editor
    public TMP_Text playText;

    public AudioSource audioSource;
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
        SceneManager.LoadScene("Level 1");
        LevelManager.instance.playerHealth = 10;
    }
}