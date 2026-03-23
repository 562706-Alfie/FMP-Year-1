using UnityEngine;

public class SmoothUpandDown : MonoBehaviour
{
    public GameObject objectToRotate;
    public float speed = 0.36f;

    Vector3 pointA;
    Vector3 pointB;

    public float amp = 5;
    public float freq = 0.4f;
    Vector3 initPos;


    private void Start()
    {
        initPos = transform.position;
    }
    private void Update()
    {
        transform.position = new Vector3(initPos.x, Mathf.Sin(Time.time * freq) * amp + initPos.y, 0);
    }
}
