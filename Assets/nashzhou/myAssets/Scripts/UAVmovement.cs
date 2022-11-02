using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UAVmovement : MonoBehaviour
{

    public bool activate = false;

    public float speed = 1.0f;

    public float angleSpeed = 0.01f;
    public Transform target;
    public bool isRotate = false;
    public Transform child;
    // Start is called before the first frame update
    void Start()
    {
        
    }


    // Update is called once per frame
    void Update()
    {
        if(activate){
            if(isRotate){
                Vector3 vec = (target.position - transform.position);
                vec.y = 0;
                Quaternion rotate = Quaternion.LookRotation(vec);
                
                transform.localRotation = Quaternion.Slerp(transform.localRotation, rotate, angleSpeed);
                if (Vector3.Angle(vec, transform.forward) < 1.0f)
                {
                    isRotate = false;
                }
            }else{
                if((transform.position - target.position).magnitude > 1){
                    var temp = Vector3.Lerp(transform.position,target.position,Time.deltaTime)-transform.position;
                    var temprotate = child.rotation;
                    transform.position = Vector3.MoveTowards(transform.position,Vector3.Lerp(transform.position,target.position,Time.deltaTime),speed);
                }else{
                    activate = false;
            }
            }
        }
        
    }


    public void activateaction(string k){
        print(k+"I am received~");
        activate = true;
        isRotate = true;
    }
}


