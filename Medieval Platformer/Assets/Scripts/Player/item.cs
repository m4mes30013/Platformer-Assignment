using System.Threading;
using TMPro;
using UnityEngine;

public class item : MonoBehaviour
{
    public GameObject pressF;

    public GameObject DialogBox;
    public string[] dialog;
    int count = 0;

    GameObject player;

    public void DoSomething()
    {
        // pop up dialog   
        // deduct enemy hp 
        // GetComponent<Animator>().SetBool("isOpen", true);
        Dialog();
    }

    public void Dialog()
    {
        if (count >= dialog.Length)
        {
            DialogBox.SetActive(false);
            count = 0;
            player.GetComponent<character>().isChat = false;
            return;
        }

        DialogBox.SetActive(true);
        DialogBox.GetComponentInChildren<TMP_Text>().text = dialog[count];
        count++;
        player.GetComponent<character>().isChat = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = col.gameObject;
            pressF.SetActive(true);
            col.gameObject.GetComponent<character>().target = gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player = null;
            pressF.SetActive(false);
            col.gameObject.GetComponent<character>().target = null;
        }
    }

}
