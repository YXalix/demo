using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showtarget : MonoBehaviour
{
    public LineRenderer line;
    public Transform Target;
    public Vector3 nowpos;

    public float speed;
    public bool activate;
    // Start is called before the first frame update
    void Start()
    {
        line.positionCount = 2;
        speed = 2f;
        nowpos = transform.position;
        activate = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)){
            line.SetPosition(0,transform.position);
            line.SetPosition(1,Target.position);
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
