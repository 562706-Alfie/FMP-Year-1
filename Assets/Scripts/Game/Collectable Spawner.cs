using UnityEngine;
using UnityEngine.UIElements;

public class CollectableSpawner : MonoBehaviour
{
    public GameObject spawnPoint; // Where the coins and abilites will spawn
    public LayerMask groundLayer;
    public int randomNumber;

    void Start()
    {

    }

    // Update is called once per frame
    public void Update()
    {
        Vector2 spawnerPositionDown = transform.position;
        Vector2 spawnerDirectionDown = Vector2.down;
        float spawnerDistanceDown = 999f;

        RaycastHit2D spawnerDown = Physics2D.Raycast(spawnerPositionDown, spawnerDirectionDown, spawnerDistanceDown, groundLayer);

        if (spawnerDown.collider != null)
        {
            spawnPoint.transform.position = spawnerDown.point;
        }
    }
}
