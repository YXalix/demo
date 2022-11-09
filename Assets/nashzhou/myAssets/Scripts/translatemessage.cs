using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Lightbug.LaserMachine;

public class translatemessage : MonoBehaviour
{

    public LineRenderer line;
    public Transform Target;

    public Vector3 newtarget;
    public Vector3 nowpos;

    public float speed;
    public bool activate;

    public bool stage2;
    // Start is called before the first frame update
    void Start()
    {
        line.positionCount = 2;
        speed = 2f;
        nowpos = transform.position;
        activate = false;
        stage2 = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            activate = true;
        }
        message();
    }

    void message(){
        if(activate){
            line.SetPosition(0,transform.position);
            nowpos =  Vector3.Lerp(nowpos,Target.position,Time.deltaTime*speed);
            line.SetPosition(1,nowpos);
            if((nowpos-Target.position).magnitude < 0.01f){
                activate = false;
                newtarget = nowpos;
                newtarget.x = newtarget.x + 500f;
                stage2 = true;
            }
        }
        if(stage2){
            line.SetPosition(0,Target.position);
            nowpos =  Vector3.MoveTowards(nowpos,newtarget,Time.deltaTime*speed*100f);
            line.SetPosition(1,nowpos);
            if((nowpos-newtarget).magnitude < 0.01f){
                stage2 = false;
            }
        }
    }

    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    private void OnTriggerEnter(Collider other)
    {
        print("hello");
    }
}
