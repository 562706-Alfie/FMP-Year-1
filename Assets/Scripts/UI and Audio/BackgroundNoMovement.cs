using UnityEngine;

public class BackgroundNoMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float backgroundHeight = 10;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, backgroundHeight, transform.position.z);
    }
}
