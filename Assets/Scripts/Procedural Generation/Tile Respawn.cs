using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

// Script goes on each tile
public class TileRespawn : MonoBehaviour
{
    public Transform respawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Despawn")
        {
            RespawnNow();
        }
    }

    public void RespawnNow()
    {
        transform.position = respawnPoint.position;
    }

}
