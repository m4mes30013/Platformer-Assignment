using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ascend : MonoBehaviour
{
    public float ascendSpeed = 5f;
    public float maxAscendHeight = 10f;
    public LayerMask layerMask;
    private bool isAscending = false;
    private float initialYPosition;
    private RaycastHit2D hit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && !isAscending)
        {
            hit = Physics2D.Raycast(transform.position, Vector2.up, maxAscendHeight, layerMask);
            if (hit.collider != null)
            {
                Debug.DrawRay(transform.position, Vector2.up * hit.distance, Color.green);
                isAscending = true;
                initialYPosition = transform.position.y;
            }
            else
            {
                Debug.DrawRay(transform.position, Vector2.up * maxAscendHeight, Color.red);
            }
        }

        if (isAscending)
        {
            GetComponent<Rigidbody2D>().gravityScale = 0;
            transform.position += Vector3.up * ascendSpeed * Time.deltaTime;

            if (transform.position.y - initialYPosition >= hit.distance)
            {
                isAscending = false;
                GetComponent<Rigidbody2D>().gravityScale = 1;
            }
        }
    }
}
