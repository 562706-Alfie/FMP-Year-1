using System.Collections.Generic;
using UnityEngine;
using System;

// Script can't go on each tile
public class TileDespawn : MonoBehaviour
{
    public float timeToDespawn;
    void Start()
    {
        
    }

    void Update()
    {
        timeToDespawn = timeToDespawn - Time.deltaTime;
        if (timeToDespawn <= 0)
        {
            Destroy(gameObject);
        }
    }
}

