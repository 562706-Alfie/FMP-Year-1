using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

// Script can't go on each tile
public class TileDespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public TileInstantiate TI;
    public bool tileAvailable = false;
    public int testTilesDespawned = 0;

    // Causes tiles to despawn when touching the "Tile Despawn Point", which will need a rigid body
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Untagged")
        {
            Destroy(collision.gameObject);
            tileAvailable = true;
            testTilesDespawned = testTilesDespawned + 1;
        }
    }
}
