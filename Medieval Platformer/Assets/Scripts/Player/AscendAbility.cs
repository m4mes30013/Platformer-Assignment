using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscendAbility : MonoBehaviour
{
    public float ascendHeightRatio = 0.5f; // The ratio of the screen height that the player will ascend
    public float ascendDuration = 0.5f; // The duration of the ascend ability
    public KeyCode ascendKey = KeyCode.Space; // The key to activate the ascend ability
    public LayerMask canPassThroughLayer; // The layer that the player can ascend through

    private bool isAscending = false; // Whether the player is currently ascending
    private float ascendStartTime; // The time when the player started ascending
    private int originalLayer; // The original layer of the player character
    private bool isTouchingGround = false; // Whether the player is currently touching a game object with the Ground tag

    void Start()
    {
        // Store the original layer of the player character
        originalLayer = gameObject.layer;
    }

    void Update()
    {

        /*
         // Check if the player pressed the ascend key and is touching a game object with the Ground tag
         if (Input.GetKeyDown(ascendKey) && !isAscending && isTouchingGround)
         {
             // Start ascending
             isAscending = true;
             ascendStartTime = Time.time;

             // Move the player character to a different layer to avoid collision with tiles
             gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");

             // Play ascend animation and sound effect
             // ...
         }
 */
        // Check if the player is currently ascending
        if (isAscending)
        {
            // Calculate the elapsed time since the player started ascending
            float elapsedTime = Time.time - ascendStartTime;

            // Check if the ascend ability has finished
            if (elapsedTime >= ascendDuration)
            {
                // Stop ascending and move the player character back to their original layer
                isAscending = false;
                gameObject.layer = originalLayer;
            }
            else
            {
                // Calculate the new position of the player
                float ascendHeight = Screen.height * ascendHeightRatio;
                float newY = transform.position.y + (ascendHeight * (elapsedTime / ascendDuration));
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }

        }
        //====//

        RaycastHit2D[] hitTop = Physics2D.RaycastAll(transform.GetChild(0).transform.position, Vector2.up * 3);
        Debug.DrawRay(transform.GetChild(0).transform.position, Vector2.up * 3, Color.red);
        // If it hits something...
        foreach (RaycastHit2D col in hitTop)
        {
            if (col.collider.gameObject.tag == "Ground")
            {
                if (Input.GetKeyDown(ascendKey) && !isAscending && isTouchingGround)
                {
                    isAscending = true;
                    ascendStartTime = Time.time;
                }
                Debug.Log("Hit top");
            }

        }

        //===//
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // Check if the player collided with a game object with the Ground tag
        if (col.gameObject.tag == "Ground")
        {
            isTouchingGround = true;
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        // Check if the player stopped colliding with a game object with the Ground tag
        if (col.gameObject.tag == "Ground")
        {
            isTouchingGround = false;
        }
    }
}
