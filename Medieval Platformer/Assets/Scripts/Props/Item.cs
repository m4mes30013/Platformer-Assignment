using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject pressF;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            pressF.SetActive(true);
            col.gameObject.GetComponent<PlayerMovement>().target = gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            pressF.SetActive(false);
            col.gameObject.GetComponent<PlayerMovement>().target = null;

        }
    }

    public void ASD()
    {
        
    }
}