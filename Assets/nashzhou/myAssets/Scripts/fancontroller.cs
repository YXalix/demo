using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class fancontroller : MonoBehaviour
{

    Animator m_Animator;
    // Start is called before the first frame update
    void Start()
    {
        var publish = GameObject.Find("agent").GetComponent<UAVGroupController>().m_MyEvent;
        var publish2 = GameObject.Find("agent").GetComponent<UAVGroupController>().m_MyEvent2;
        m_Animator = gameObject.GetComponent<Animator>();
        publish.AddListener(activate);
        publish2.AddListener(deactivate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void activate(){
        m_Animator.SetBool("fly",true);
    }

    public void deactivate(){
        m_Animator.SetBool("fly",false);
    }
}
