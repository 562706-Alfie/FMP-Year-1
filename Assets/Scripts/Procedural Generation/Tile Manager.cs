using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

// Script can't go on each tile
public class TileManager : MonoBehaviour
{
    public Transform respawnPoint;
    public PlayerScript PS;
    public float newWidth = 38.84f, oldWidth = 121.9f, tileRespawnPoint = 121.9f, oldTileRespawnPoint; // dont change these or it will probably break
    Rigidbody2D rb;

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

        rb = GetComponent<Rigidbody2D>();
    }

     void Update()
    {
        float xvelo;
        xvelo = rb.linearVelocity.x;
        xvelo = PS.xvel;

        if (xvelo <= 0)
        {
            xvelo = 0;
        }

        if (xvelo >= PS.speedLimit)
        {
            xvelo = PS.speedLimit;
        }
        rb.linearVelocity = new Vector3(xvelo, 0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "landTile")
        {
            float startX = collision.transform.position.x;
            int prefabIndex;

            oldWidth = collision.gameObject.GetComponent<Collider2D>().bounds.size.x; // Gets the width of the old tile

            prefabIndex = UnityEngine.Random.Range(0, tileList.Count); // Chooses a random tile from the array

            clone = Instantiate(tileList[prefabIndex], new Vector3(0, 0, 0), Quaternion.identity);// JUST spawns the new tile, nothing else
            newWidth = clone.GetComponent<Collider2D>().bounds.size.x; // Gets the width of the new tile
            tileRespawnPoint = startX + (oldWidth / 2) + (newWidth / 2); // find the correct location of the TRP

            clone.transform.position = new Vector3(tileRespawnPoint, 0, 0); // Moves the tile to the TRP

            print("Width of the new tile = " + newWidth + ",  Width of the old tile = " + newWidth);
            
            //Destroy(collision.gameObject);

        }
    }
}
