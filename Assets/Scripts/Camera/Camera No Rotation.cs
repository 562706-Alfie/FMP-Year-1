using UnityEngine;

public class camera_no_rotate : MonoBehaviour
{

    [SerializeField] float cameraHeight, cameraSize;
    public bool isCameraLocked = true;
    public PlayerScript PlayerScript;
    //33, 45, 70

    void Start()
    {
        Camera.main.orthographicSize = cameraSize;
    }
    void Update()
    {
        // Locks camera height, increases and scales up when player moves up 
        if (PlayerScript.ypos < 36)
        {
            transform.position = new Vector3(transform.position.x, cameraHeight, transform.position.z);
            Camera.main.orthographicSize = cameraSize;

        }
        else
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + PlayerScript.yvel * Time.deltaTime;
        }
    }
}
