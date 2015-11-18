using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public float speed;

    private Rigidbody rb;

    void Start ()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis ("Horizontal");
        float moveVertical = Input.GetAxis ("Vertical");

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        rb.AddForce (movement * speed);
    }

    // Detects the collision. With our collider connector.
    void OnTriggerEnter(Collider other) 
    {
        // Identifies the game object by comparing it to a string.
        if (other.gameObject.CompareTag ("Pick Up"))
        {
            // Activates or deactivates the object. (Hides)
            other.gameObject.SetActive (false);
        }
    }
}