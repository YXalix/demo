using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MyVecEvent : UnityEvent<Vector3Int,Vector3Int>
{
}

public class InputManage : MonoBehaviour
{

    private Vector3Int groupsNum;

    private Vector3Int groupsTarget;

    public MyVecEvent submit_Event;

    public GameObject GUI;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        if (submit_Event == null)
            submit_Event = new MyVecEvent();
        GUI = GameObject.Find("Canvas");
        GUI.SetActive(false);
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        groupsNum.Set(36,36,36);
        groupsTarget.Set(0,3,6);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P)){
            GUI.SetActive(true);
        }
    }
    public void setgroups0(string UAVNum){
         int number;
         bool success = int.TryParse(UAVNum, out number);
         if (success)
         {
            groupsNum[0] = number;
         }
    }

    public void setgroups1(string UAVNum){
         int number;
         bool success = int.TryParse(UAVNum, out number);
         if (success)
         {
            groupsNum[1] = number;
         }
    }
    public void setgroups2(string UAVNum){
         int number;
         bool success = int.TryParse(UAVNum, out number);
         if (success)
         {
            groupsNum[2] = number;
         }
    }

    public void setgroup0Target(int targetID){
        groupsTarget[0] = targetID;
    }

    public void setgroup1Target(int targetID){
        groupsTarget[1] = targetID + 3;
    }

    public void setgroup2Target(int targetID){
        groupsTarget[2] = targetID + 6;
    }

    public void submit(){
        submit_Event.Invoke(groupsNum,groupsTarget);
        GUI.SetActive(false);
    }
}
