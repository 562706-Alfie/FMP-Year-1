using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

// Script can't go on each tile
public class TileDespawn : MonoBehaviour
{
    public Transform respawnPoint;
    public TileInstantiate TI;
    public float newWidth = 38.84f, oldWidth = 121.9f, tileRespawnPoint = 121.9f, oldTileRespawnPoint;

    List<GameObject> tileList = new List<GameObject>();
    public GameObject tile1;
    public GameObject tile2;
    public GameObject tile3;

    GameObject clone;

    void Start()
    {
        tileList.Add(tile1);
        tileList.Add(tile2);
        tileList.Add(tile3);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "landTile")
        {
            //gets the width of the tile that has been hit
            oldWidth = collision.gameObject.GetComponent<Collider2D>().bounds.size.x; // Gets the width of the old tile

            int prefabIndex = UnityEngine.Random.Range(0, tileList.Count); // Chooses a random tile from the array

            clone = Instantiate(tileList[prefabIndex], new Vector3(tileRespawnPoint, 0, 0), Quaternion.identity);// Spawns the new tile
            newWidth = clone.GetComponent<Collider2D>().bounds.size.x; // Gets the width of the new tile
            tileRespawnPoint = (oldWidth / 2) + (newWidth / 2) + tileRespawnPoint;

            print("width = " + newWidth + "  cloneWidth=" + newWidth);
            Destroy(collision.gameObject);

            // oldTileWidth = oldcloneSize / 2;
            // Debug.Log("Collider Size = " + oldcloneSize); // Outputs size of collider 
        }
    }

    public void WidthCheck()
    {
        /*
        newcloneSize = cloneCollider.bounds.size; // Gets the size of the box around the tile
        newTileWidth = newcloneSize / 2;
        print("Width Check Complete");
        widthCheckComplete = true;
        */
    }

    /*
    Destroy(collision.gameObject);
            tileAvailable = true;
            testTilesDespawned = testTilesDespawned + 1;
        }
    }
    */
}
