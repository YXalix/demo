using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{

    public GameObject[] targets;
    public GameObject respawnPrefab;
    public GameObject[] respawns;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        targets = GameObject.FindGameObjectsWithTag("groupitem");
    }
    public void outputlog(){
        Debug.Log("hello world");
    }


    public void move(){
        foreach(GameObject item in targets){
            item.transform.position += Vector3.forward * 5f;
        }
    }
}
