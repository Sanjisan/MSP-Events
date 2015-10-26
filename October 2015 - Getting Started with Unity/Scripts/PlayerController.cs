/*Contains all of the code requried for moving the ball. Physics and user input*/
using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    // Rigidbody variable that holds the reference.
    private Rigidbody rb;

    /* Public variables show up in the inspector as editable, and can thus make
    code changes directly in the editor instead of editing it in code.*/
    public float speed;
    
    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    /* Called just before performing any physics calculations. Contains physics 
    code. If editing in MonoDevelop a shortcut to search the Unity API in 
    Windows is CTRL + '. This allows you to see what functions, events, etc can be
    used by a particular item such as Input.XXXX then locate it on the loaded page.*/
    void FixedUpdate () {
        // Take input from keybaord at axis locations.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // By default movement is not multiplied by speed.
        rb.AddForce(movement * speed);
	
	}
}
