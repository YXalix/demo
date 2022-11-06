using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[System.Serializable]
public class MyStringEvent : UnityEvent<string>
{
}
public class eventprint : MonoBehaviour
{
    public MyStringEvent m_MyEvent;

    // Start is called before the first frame update
    void Start()
    {
        if (m_MyEvent == null)
            m_MyEvent = new MyStringEvent();
        m_MyEvent.AddListener(Ping);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_MyEvent != null)
        {
            m_MyEvent.Invoke("move to the target please !");
        }
    }

    void Ping(string k)
    {
        Debug.Log("Ping");
    }
}
