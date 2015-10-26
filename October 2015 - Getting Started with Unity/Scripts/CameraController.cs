using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    // Reference to the player
    public GameObject player;

    // Holds the offset value. Is private because it will be set in code only.
    private Vector3 offset;

    void Start ()
    {
        // Takes the distance of player and camera to keep equal distance.
        offset = transform.position - player.transform.position;
    }
    
    // Runs after every frame.
    void LateUpdate ()
    {
        transform.position = player.transform.position + offset;
    }
}