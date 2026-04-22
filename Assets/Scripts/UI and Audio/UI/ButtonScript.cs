using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    //public variable to reference the button text - set this up in the Unity editor
    public TMP_Text buttonText;
    public bool mainThemePlayed;

    public void Start()
    {
        
        if (mainThemePlayed == true)
        {
            FindFirstObjectByType<AudioManager>().Play("MainTheme");
            //mainThemePlayed = false;
        }

    }

    public void ButtonMethod()
    {
        FindFirstObjectByType<AudioManager>().Play("MenuSelect");
        buttonText.text = "Resetting!";
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LevelManager.instance.playerHealth = 10;
    }

    public void PlayMethod()
    {
        SceneManager.LoadScene("Main Scene");
    }

    public void ReturntoMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ChangeMusicVolume(float volume)
    {
        AudioManager.instance.musicVolume = volume;
    }

    public void ChangeSFXVolume(float volume)
    {
        AudioManager.instance.sfxVolume = volume;
    }

    public void PlaySelect()
    {
        AudioManager.instance.Play("MenuSelect");
    }

    public void PlaySelectBack()
    {
        AudioManager.instance.Play("MenuBack");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}