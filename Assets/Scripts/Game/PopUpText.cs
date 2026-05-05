using UnityEngine;

public class PopUpText : MonoBehaviour
{
    public Vector2 initialVelocity;
    public Rigidbody2D rb;
    public float lifetime = 1.5f;

    void Start()
    {
        rb.linearVelocity = initialVelocity;
        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
