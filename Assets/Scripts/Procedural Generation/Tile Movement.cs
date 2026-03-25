using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

// This script would no longer be used for the tiles, but for some reason breaks when I remove them??

public class TileMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody2D rb;
    public Vector3 pos;
    [SerializeField] float tileSpeed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        pos = transform.position;
        float xvel, yvel;
        xvel = rb.linearVelocity.x;
        yvel = rb.linearVelocity.y;

        // Gives the tiles their backwards velocity
        xvel = tileSpeed;

        rb.linearVelocity = new Vector3(xvel, yvel, 0);
    }
}

