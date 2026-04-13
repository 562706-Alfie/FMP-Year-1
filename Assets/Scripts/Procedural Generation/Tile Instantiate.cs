using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TileInstantiate : MonoBehaviour
{
    public Vector2 tileRespawnPoint;
    public Transform tileRespawnPosition;
    public TileDespawn TD;
    public BoxScript BS;

    Vector3 oldTRP;
    // Vector 3 oldTileWidth; -> from the BoxScript script

    // List of all the tiles used
    List<GameObject> tileList = new List<GameObject>();
    public GameObject Tile1;
    public GameObject Tile2;
    public GameObject Tile3;


    void Start()
    {
        tileList.Add(Tile1);
        tileList.Add(Tile2);
        tileList.Add(Tile3);
    }

    void Update()
    {
        /*
        GameObject clone;
        tileRespawnPoint = tileRespawnPosition.position;
        // Randomly selects tiles from the list to be spawned
        if(TD.tileAvailable == true)
        {
            int prefabIndex = UnityEngine.Random.Range(0, tileList.Count); // Should auto update based on how many tiles are in the array
            clone = Instantiate(tileList[prefabIndex]);
            BS.WidthCheck();
            if (BS.widthCheckComplete == true)
                {
                tileRespawnPoint = BS.oldTileWidth + oldTRP + BS.newTileWidth;
                clone.transform.position = tileRespawnPoint;
                BS.widthCheckComplete = false;
                TD.tileAvailable = false;
                }
        }
        */
    }
}
