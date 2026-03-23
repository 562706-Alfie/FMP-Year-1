using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Player pl;
    public static LevelManager instance;
    public int highScore;
    public int playerHealth = 10;
    

    void Awake()
    {
        if (instance == null)
        {
            // if instance is null, store a reference to this instance
            instance = this;
            DontDestroyOnLoad(gameObject);
            print("do not destroy");
        }
        else
        {
            // Another instance of this gameobject has been made so destroy it
            // as we already have one
            print("do destroy");
            Destroy(gameObject);
        }
    }

    //these methods are globally accessible

    public int GetHighScore()
    {
        return highScore;
    }

}

