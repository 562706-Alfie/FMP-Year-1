using UnityEngine;

public class CoinAtStartRemover : MonoBehaviour
{
    Rigidbody2D rb;
    float timer = 3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float xvel;
        xvel = rb.linearVelocity.x;
        xvel = 1;
        timer -= Time.deltaTime;
        Destroy(gameObject, timer);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject)
        {
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject)
        {
            Destroy(collision.gameObject);
        }
    }
}
