using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class camera_no_rotate : MonoBehaviour
{

    [SerializeField] Transform target;

    void Update()
    {
        transform.position = new Vector3(transform.position.x, 4.5f, transform.position.z);
    }

}
