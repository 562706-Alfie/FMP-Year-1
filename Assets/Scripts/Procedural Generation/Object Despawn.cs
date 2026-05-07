using System.Numerics;
using UnityEngine;

// Script can't go on each tile
public class ObjectDespawn : MonoBehaviour
{
    public float startingPosition;
    public float distance;
    public float timeToDespawn;
    void Start()
    {
        startingPosition = transform.position.x; // = new vector(transform.position.x + startingPosition, transform.position.y, transform.position.z);
    }

    void Update()
    {
        //timeToDespawn = timeToDespawn - Time.deltaTime; 
        //if (timeToDespawn <= 0)
        //{
        //    Destroy(gameObject);
        //}
    }
}

