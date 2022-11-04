using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class rbmoveto : MonoBehaviour
{

    public UnityEvent m_MyEvent;
    public UnityEvent m_MyEvent2;
    public Vector3 targetPosition;
    public float angleSpeed = 0.01f;
    float speed = 0.1f;
    float updownspeed = 0.2f;

    float angle;

    public bool readyup = false;

    public bool readyrotate = false;

    public bool readyfly = false;

    public bool readydown = false;

    public Transform child;

    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();
        if (m_MyEvent2 == null)
            m_MyEvent2 = new UnityEvent();
        targetPosition = GameObject.Find("UAVStarget").transform.position;
        var temp = transform.GetComponentInChildren<Transform>();
        var temp1 = temp.GetComponentInChildren<Transform>();
        child = transform.GetChild(0);
        //Set the angular velocity of the Rigidbody (rotating around the Y axis, 100 deg/sec)
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.P)){
            readyup = true;
        }
        if (Input.GetKeyDown(KeyCode.F) && m_MyEvent != null)
        {
            m_MyEvent.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.L) && m_MyEvent2 != null)
        {
            m_MyEvent2.Invoke();
        }
        if(readyup){
            myflyup();
        }
        if(readyrotate){
            myrotateTo();
        }
        if(readyfly){
            myflyto();
        }
        if(readydown){
            myflydown();
        }
    }

    public void myflyup(){
        var target = Vector3.zero ;
        target.y = targetPosition.y + 30f - transform.position.y;
        var step = target * Time.deltaTime;
        print(step);
        // check end node condition
        if(step.magnitude < 0.001f){
            readyup = false;
            readyrotate = true;
        }
        //transform.position =  (transform.position + step);
        transform.position = Vector3.MoveTowards(transform.position,transform.position+step,speed);
    }

    public void myrotateTo(){
        Vector3 vec = (targetPosition - child.position);
        vec.y = 0;
        Quaternion rotate = Quaternion.LookRotation(vec);

        child.localRotation = Quaternion.Slerp(child.localRotation, rotate, angleSpeed);
        print(Vector3.Angle(vec, child.forward));
        if (Vector3.Angle(vec, child.forward) < 0.01f)
        {
            readyrotate = false;
            readyfly = true;
        }
    }
    public void myflyto(){
        var target = targetPosition;
        target.y = transform.position.y;
        var dis = Vector3.Lerp(transform.position,target,Time.deltaTime)-transform.position;
        transform.position = Vector3.MoveTowards(transform.position,transform.position+dis,speed);
        print(dis.magnitude);
        if(dis.magnitude < 0.001f){
            readyfly = false;
            readydown = true;
        }
    }

    public void myflydown(){
        var target = targetPosition - transform.position;
        // check end node condition
        if(Mathf.Abs(target.y) < 0.2f){
            readydown = false;
        }
        transform.position =  (transform.position + target * Time.deltaTime * updownspeed);
    }
}
