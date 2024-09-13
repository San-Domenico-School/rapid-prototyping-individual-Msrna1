using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/**************************************
This script is For the camera movement
The purpose is to track behind player
Mathew Srna
Ver 1.0.0
********************************************/
public class CameraController : MonoBehaviour
{
    [Tooltip("Drag Vehicle onto Vehicle Transform")]
    [SerializeField] private Transform vehicleTransform; // Holds the Transform component of the player
    private Vector3 offset;                              // Holds the position vector that the camera will maintain from the player
    // Start is called before the first frame update /
    void Start()
    {
        offset = new Vector3(0, 5, -7);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        FollowPlayer();
    }

    // Assigns the correct offset to the camera position
    private void FollowPlayer()
    {
        transform.position = vehicleTransform.position + offset;
    }

}
