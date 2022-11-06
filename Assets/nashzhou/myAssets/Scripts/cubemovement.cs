using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubemovement : MonoBehaviour
{


    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            moveup();
        }
        if(Input.GetKey(KeyCode.S)){
            movedown();
        }
        if(Input.GetKey(KeyCode.A)){
            moveleft();
        }
        if(Input.GetKey(KeyCode.D)){
            moveright();
        }
    }


    private void moveup(){
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    private void movedown(){
        transform.position += Vector3.back * speed * Time.deltaTime;
    }
    private void moveright(){
        transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void moveleft(){
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

}
