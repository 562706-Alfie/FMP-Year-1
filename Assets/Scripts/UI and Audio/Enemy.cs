using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision enemy)
    {
        if (enemy.gameObject.tag == "Player")
        {
            FindFirstObjectByType<AudioManager>().Play("PlayerHit");
            LevelManager.instance.playerHealth -= 1;
            print("Player health is " + LevelManager.instance.playerHealth);
        }
    }
}
