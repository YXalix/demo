using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UAVGroupController : MonoBehaviour
{

    public int groupID;
    public int groupNum;
    public int groupTarget;
    public float height;
    public UnityEvent m_MyEvent;
    public UnityEvent m_MyEvent2;
    public Vector3 targetPosition;
    public Transform targetTransform;
    public GameObject[] UAVPerfab = new GameObject[4];

    public LineRenderer line;


    //角速度大小
    private float angleSpeed = 3f;

    //飞行速度大小
    private float speed = 30f;

    //上下速度大小
    private float updownspeed = 10f;

    public bool readyup = false;

    public bool readyrotate = false;

    public bool readyfly = false;

    public bool readydown = false;

    public bool readyrotateequal = false;

    public float currentSpeed = 0f;

    // Start is called before the first frame update
    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new UnityEvent();
        if (m_MyEvent2 == null)
            m_MyEvent2 = new UnityEvent();
        var publishobject = GameObject.Find("InputHandler");
        var publish = publishobject.GetComponent<InputManage>().submit_Event;
        publish.AddListener(generator4case);
    }

    void FixedUpdate(){
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
            line.SetPosition(0,transform.position);
            line.SetPosition(1,targetPosition);
        }
        if(readyfly){
            myflyto();
            line.SetPosition(0,transform.position);
            line.SetPosition(1,targetPosition);
        }
        if(readyrotateequal){
            myrotateequal();
        }
        if(readydown){
            myflydown();
        }
    }

    public void myflyup(){
        currentSpeed = currentSpeed + Time.deltaTime*0.4f;
        print(currentSpeed);
        currentSpeed = Mathf.Min(currentSpeed,speed);
        var target = Vector3.zero ;
        target.y = height - transform.position.y;
        var step = target * Time.deltaTime;
        // check end node condition
        if(step.magnitude < 0.1f){
            readyup = false;
            readyrotate = true;
        }
        //transform.position =  (transform.position + step);
        transform.position = Vector3.MoveTowards(transform.position,transform.position+step,currentSpeed);
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
        if (Vector3.Angle(vec, item.forward) < 1f) // need to been checked
        {
            line.SetPosition(0,transform.position);
            line.SetPosition(1,targetPosition);
            readyrotate = false;
            readyfly = true;
        }
    }
    public void myflyto(){
        var target = targetPosition;
        target.y = transform.position.y;
        var dis = Vector3.Lerp(transform.position,target,Time.deltaTime)-transform.position;
        transform.position = Vector3.MoveTowards(transform.position,transform.position+dis,speed);
        if(dis.magnitude < 0.01f){
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


    public void generator(Vector3Int groupsNum,Vector3Int groupsTarget){
        // Init this group data
        groupNum = groupsNum[groupID];
        groupTarget = groupsTarget[groupID];
        targetTransform = GameObject.Find("UAVStarget"+groupTarget).transform;
        targetPosition  = targetTransform.position;

        var count = 0;
        int sum = (int)Mathf.Sqrt(groupNum);
        for(int i = 0;i<=sum;i++){
            for(int j = 0;j<=sum;j++){
                if(count < groupNum){
                    var templocation = transform.position;
                    templocation.x += (i-sum/2)*5;
                    templocation.z += (j-sum/2)*5;
                    Instantiate(UAVPerfab[0],templocation,Quaternion.Euler(0,0,0),transform);
                    count++;
                }
            }
        }
        readyup = true;
    }

    public void generator4case(Vector3Int groupsNum,Vector3Int groupsTarget){
        // Init this group data
        groupNum = groupsNum[groupID];
        groupTarget = groupsTarget[groupID];
        targetTransform = GameObject.Find("UAVStarget"+groupTarget).transform;
        targetPosition  = targetTransform.position;

        var count = 0;
        int sumi = (int)Mathf.Sqrt(groupNum);
        int sumj = groupNum / sumi + 1;
        if(groupNum < 16){
            sumi = 1;
            sumj = groupNum / 2 + 1;
        }
        if(groupNum < 8){
            sumi = 0;
            sumj = groupNum;
        }
        for(int i = 0;i<=sumi;i++){
            for(int j = 0;j<=sumj;j++){
                if(count < groupNum){
                    var templocation = transform.position;
                    templocation.x += (i-sumi/2)*5;
                    templocation.z += (j-sumj/2)*5;
                    Instantiate(UAVPerfab[i%4],templocation,Quaternion.Euler(0,0,0),transform);
                    count++;
                }
            }
        }
        readyup = true;
    }
}
