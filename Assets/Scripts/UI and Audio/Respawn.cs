using UnityEngine;

public class GameRespawn : MonoBehaviour
{
    public Player pl;
    public Transform respawnPoint;

    private void OnCollisionEnter(Collision other)
    {
        pl.score = pl.score + 1;
        other.gameObject.transform.position = respawnPoint.position;
    }
}

// if (other.gameObject.CompareTag("Player") || Input.GetKeyDown("r"))
