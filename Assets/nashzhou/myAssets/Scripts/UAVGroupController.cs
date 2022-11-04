using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UAVGroupController : MonoBehaviour
{

    public UnityEvent m_MyEvent;
    public UnityEvent m_MyEvent2;
    public Vector3 targetPosition;

    public int InitUAVNum;

    public GameObject UAVPerfab;

    public Transform[] spawns;
    public float angleSpeed = 0.01f;
    float speed = 0.1f;
    float updownspeed = 0.2f;

    float angle;

    public bool readyup = false;

    public bool readyrotate = false;

    public bool readyfly = false;

    public bool readydown = false;

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
        
        //Set the angular velocity of the Rigidbody (rotating around the Y axis, 100 deg/sec)
    }

    void Update(){
        if(Input.GetKeyDown(KeyCode.G)){
            generator();
        }
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
            myrotateToall();
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

    public void myrotateToall(){
        foreach(Transform child in transform){
            myrotateTo(child);
        }
    }
    public void myrotateTo(Transform item){
        Vector3 vec = (targetPosition - item.position);
        vec.y = 0;
        Quaternion rotate = Quaternion.LookRotation(vec);

        item.localRotation = Quaternion.Slerp(item.localRotation, rotate, angleSpeed);
        print(Vector3.Angle(vec, item.forward));
        //if (Vector3.Angle(vec, item.forward) < 0.01f). // need to been checked
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


    public void generator(){
        InitUAVNum = 120;
        //spawns = new Transform[InitUAVNum];
        var count = 0;
        int sum = (int)Mathf.Sqrt(InitUAVNum);
        for(int i = 0;i<sum;i++){
            for(int j = 0;j<sum;j++){
                if(count < InitUAVNum){
                    var templocation = transform.position;
                    templocation.x += (i-sum/2)*5;
                    templocation.z += (j-sum/2)*5;
                    Instantiate(UAVPerfab,templocation,Quaternion.Euler(0,0,0),transform);
                    count++;
                }
            }
        }
    }
}
