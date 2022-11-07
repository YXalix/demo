using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UAVGroupController : MonoBehaviour
{

    public int groupID;

    public float height;
    public UnityEvent m_MyEvent;
    public UnityEvent m_MyEvent2;
    public Vector3 targetPosition;
    public Transform targetTransform;

    public int InitUAVNum;

    public GameObject UAVPerfab;

    private Transform[] spawns;
    private float angleSpeed = 0.005f;
    private float speed = 0.5f;
    private float updownspeed = 0.5f;

    private float angle;

    public bool readyup = false;

    public bool readyrotate = false;

    public bool readyfly = false;

    public bool readydown = false;

    public bool readyrotateequal = false;

    Animator m_Animator;

    // Start is called before the first frame update
    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();
        if (m_MyEvent2 == null)
            m_MyEvent2 = new UnityEvent();
        targetTransform = GameObject.Find("UAVStarget"+groupID).transform;
        targetPosition  = targetTransform.position;
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
            myrotateTo(transform);
        }
        if(readyfly){
            myflyto();
        }
        if(readyrotateequal){
            myrotateequal();
        }
        if(readydown){
            myflydown();
        }
    }

    public void myflyup(){
        var target = Vector3.zero ;
        target.y = height - transform.position.y;
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
        if (Vector3.Angle(vec, item.forward) < 1f) // need to been checked
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
            readyrotateequal = true;
        }
    }
    public void myrotateequal(){
        Vector3 vec = targetTransform.forward;
        vec.y = 0;
        Quaternion rotate = Quaternion.LookRotation(vec);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotate, angleSpeed);
        if (Vector3.Angle(vec, transform.forward) < 1f) // need to been checked
        {
            readyrotateequal = false;
            readydown = true;
        }
    }

    public void myflydown(){
        var target = targetPosition - transform.position;
        // check end node condition
        if(Mathf.Abs(target.y) < 0.2f){
            readydown = false;
            m_MyEvent2.Invoke();
        }
        transform.position =  (transform.position + target * Time.deltaTime * updownspeed);
    }


    public void generator(){
        InitUAVNum = 64;
        //spawns = new Transform[InitUAVNum];
        var count = 0;
        int sum = (int)Mathf.Sqrt(InitUAVNum);
        for(int i = 0;i<=sum;i++){
            for(int j = 0;j<=sum;j++){
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
