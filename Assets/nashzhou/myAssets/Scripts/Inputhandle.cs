using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputhandle : MonoBehaviour
{   
    public GameObject UAVPerfab;

    public GameObject[] spawns;

    public int InitUAVNum = 120;

    public Vector3 InitTargetlocation = new Vector3(6,2,100);

    public void setUAVNum(string UAVNum){
        InitUAVNum = int.Parse(UAVNum);
    }

    public void setTargetlocation(string sVector){
        InitTargetlocation = StringToVector3(sVector);
    }

    public void spawnUAVs(){
        Debug.Log("spawn succeed!UAVnum: "+InitUAVNum +" location: "+InitTargetlocation);

        spawns = new GameObject[InitUAVNum];
        var count = 0;
        Vector3 templocation = InitTargetlocation;
        for(int i = 0;i<Mathf.Sqrt(InitUAVNum);i++){
            for(int j = 0;j<Mathf.Sqrt(InitUAVNum);j++){
                if(count < InitUAVNum){
                    templocation = InitTargetlocation;
                    templocation.x += i*5;
                    templocation.z += j*5;
                    spawns[count] = Instantiate(UAVPerfab,templocation,Quaternion.Euler(0,0,0));
                }
                count++;
            }
        }

        Destroy(GameObject.Find("Canvas"));
        
    }


     public static Vector3 StringToVector3(string sVector)
     {
         // Remove the parentheses
         if (sVector.StartsWith ("(") && sVector.EndsWith (")")) {
             sVector = sVector.Substring(1, sVector.Length-2);
         }
 
         // split the items
         string[] sArray = sVector.Split(',');
 
         // store as a Vector3
         Vector3 result = new Vector3(
             float.Parse(sArray[0]),
             float.Parse(sArray[1]),
             float.Parse(sArray[2]));
 
         return result;
     }
}
