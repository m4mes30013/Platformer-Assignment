using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSceneSwapping : MonoBehaviour
{
    private void Awake() 
    {
        GameObject[] musicObject = GameObject.FindGameObjectsWithTag("AudioSceneSwapping");
        if(musicObject.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
