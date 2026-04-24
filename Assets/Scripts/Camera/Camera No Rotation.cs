using UnityEngine;

public class camera_no_rotate : MonoBehaviour
{

    [SerializeField] float cameraHeight, cameraSize, cameraZoomOutRate, cameraZoomInRate, cameraMaxZoom;
    public bool isCameraLocked = true;

    void Start()
    {
        Camera.main.orthographicSize = cameraSize;
    }
    void Update()
    {
        /*
        if (Input.GetKey(KeyCode.Q))
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + cameraZoomOutRate * Time.deltaTime;
            if (Camera.main.orthographicSize > cameraMaxZoom)
            {
                Camera.main.orthographicSize = cameraMaxZoom;
            }

            if (Camera.main.orthographicSize > 100f)
            {
                print("Need to move camera up");
                isCameraLocked = false;
                if (Camera.main.orthographicSize < 61.77879f)
                {
                    isCameraLocked = true;
                }
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            Camera.main.orthographicSize = Camera.main.orthographicSize + cameraZoomInRate * Time.deltaTime;
            if (Camera.main.orthographicSize < 8)
            {
                Camera.main.orthographicSize = 8;
            }
        }
        */
        if (isCameraLocked == true)
        {
            transform.position = new Vector3(transform.position.x, cameraHeight, transform.position.z);
            print("Camera Locked");
        }
    }

}
