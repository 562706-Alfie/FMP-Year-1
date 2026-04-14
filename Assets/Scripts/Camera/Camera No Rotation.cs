using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class camera_no_rotate : MonoBehaviour
{

    [SerializeField] float cameraHeight;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, cameraHeight, transform.position.z);
    }

}
