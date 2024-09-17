using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/************************************
Component that takes in player imput and moves the vehicle
to reciev player imputs and move
Mathew Srna
ver 1.0.0
*************************************/

public class NewBehaviourScript : MonoBehaviour
{
    private float speed;     // Holds the forward movement of the vehicle
    private float turnspeed; // Holds the turn speed of the vehicle
    private float verticalInput; // Gets a value  [-1, 1] from user key press up/down or W/S
    private float horizontalinput; // Gets a value  [-1, 1] from user key press left/right or A/D
    private Rigidbody rb;  // points to vehicle rigidbody component

    // Start is called before the first frame update
    void Start()
    {           
        speed = 20.0f;
        turnspeed = 30.0f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddRelativeForce(Vector3.forward * speed * rb.mass * verticalInput);
        transform.Rotate(Vector3.up * turnspeed * horizontalinput * Time.deltaTime);
        Scorekeeper.Instance.AddToScore(verticalInput);

    }

    //Called from PlayerActionInput when user presses WASD or arrow keys
    private void OnMove(InputValue input)
    {
        verticalInput = input.Get<Vector2>().y;   // Assign ed when player presses W or S
        horizontalinput = input.Get<Vector2>().x; // Assigned when player presses A or D     
    }
    public class PlayerController : MonoBehaviour
    {
        // Existing methods like FixedUpdate, Update, etc.

        // Method called when the player collides with another object
        private void OnCollisionEnter(Collision collision)
        {
            // Log the name of the object collided with
            Debug.Log("Collision detected with: " + collision.gameObject.name);

            // Check if the collided object has the tag "Obstacle"
            if (collision.gameObject.CompareTag("Obstacle"))
            {
                // Log a message to confirm that an obstacle was hit (useful for debugging)
                Debug.Log("Hit an obstacle!");

                // Call the SubtractFromScore method in the Scorekeeper instance to decrease the score
                if (Scorekeeper.Instance != null)
                {
                    Scorekeeper.Instance.SubtractFromScore();
                }
                else
                {
                    Debug.LogError("Scorekeeper instance is null!");
                }
            }
        }
    }
}

