using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    // Script does NOT need to go on each button, only the canvas(or anything really)
    // When using this in the future please rename the methods and the script as well.
    // Only need button script, audio manager, sound, menu manager, and volume settings
    //      - Button script stores all the methods for pressing buttons
    //      - Audio manager stores the audio
    //      - Sound is used by audio manager to store each sound and give different otions for it
    //      - Menu manager is its own gameobject which I use just to store the difficulty via playerpref. Might need to connect music and sfx slider to it as well?? 
    //      - Volume settings is used for setting up the music and sfx slider


    public bool isMusicPlayer;
    public GameObject deathPanel;
    public GameObject optionsPanelHard;

    public GameObject easyDifficulty;
    public GameObject mediumDifficulty;
    public GameObject hardDifficulty;
    public GameObject impossibleDifficulty;

    public GameObject gameOverSelectedButton;
    public GameObject pauseScreenSelectedButton;


    public TextMeshProUGUI difficultyChosen;

    public GameObject selectedDifficulty;

    public MenuManager menuManager;

    public void Start()
    {

        if (isMusicPlayer == true)
        {
            FindFirstObjectByType<AudioManager>().Play("MainTheme");
            isMusicPlayer = false;
        }

        selectedDifficulty = easyDifficulty;

    }

    public void ButtonMethod()
    {
        FindFirstObjectByType<AudioManager>().Play("MenuSelect");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LevelManager.instance.playerHealth = 10;
    }

    // These methods are used for the new input system for setting the button to be selected when going to that panel
    public void OptionsPanelSelectedButton(GameObject selectedButton)
    {
        selectedButton = selectedDifficulty;
        print(selectedButton);
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(selectedButton, new BaseEventData(eventSystem));
    }
    public void NewPanelSelectedButton(GameObject selectedButton)
    {
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(selectedButton, new BaseEventData(eventSystem));
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

    public void PauseButton()
    {
        Time.timeScale = 0f;
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(pauseScreenSelectedButton, new BaseEventData(eventSystem));
    }
    public void UnPauseButton()
    {
        Time.timeScale = 1.0f;
    }

    public void OpenDeathPanel()
    {
        deathPanel.SetActive(true);
        Time.timeScale = 0f;
        AudioManager.instance.Play("Death");
        var eventSystem = EventSystem.current;
        eventSystem.SetSelectedGameObject(gameOverSelectedButton, new BaseEventData(eventSystem));
    }

    public void SetDifficulty(int difficulty)
    {
        menuManager.difficulty = difficulty;

        if (menuManager.difficulty == 20)
        {
            difficultyChosen.text = "Difficulty: Easy";
        }

        if (menuManager.difficulty == 10)
        {
            difficultyChosen.text = "Difficulty: Medium";
        }

        if (menuManager.difficulty == 0)
        {
            difficultyChosen.text = "Difficulty: Hard";
        }

        if (menuManager.difficulty == -10)
        {
            difficultyChosen.text = "Difficulty: Impossible";
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetStartOption()
    {
        if (selectedDifficulty == null)
        {
            selectedDifficulty = easyDifficulty;
            difficultyChosen.text = "Difficulty: Easy";
        }

        if (PlayerPrefs.GetInt("Difficulty") == 20)
        {
            selectedDifficulty = easyDifficulty;
            difficultyChosen.text = "Difficulty: Easy";
        }

        if (PlayerPrefs.GetInt("Difficulty") == 10)
        {
            selectedDifficulty = mediumDifficulty;
            difficultyChosen.text = "Difficulty: Medium";
        }

        if (PlayerPrefs.GetInt("Difficulty") == 0)
        {
            selectedDifficulty = hardDifficulty;
            difficultyChosen.text = "Difficulty: Hard";
        }

        if (PlayerPrefs.GetInt("Difficulty") == -10)
        {
            selectedDifficulty = impossibleDifficulty;
            difficultyChosen.text = "Difficulty: Impossible";
        }
    }
}