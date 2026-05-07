using UnityEngine;

public class DespawnPoint : MonoBehaviour
{
    Rigidbody2D rb;
    public GameManager gameManager;
    public PlayerScript PS;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float xvelo;
        xvelo = rb.linearVelocity.x;

        if (gameManager.finishedLoading == false)
        {
            xvelo = -300;
            rb.linearVelocity = new Vector3(xvelo, 0, 0);
        }

        if (gameManager.finishedLoading == true)
        {
            xvelo = PS.xvel;

            if (xvelo >= PS.speedLimit)
            {
                xvelo = PS.speedLimit;
            }

            rb.linearVelocity = new Vector3(xvelo, 0, 0);
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
