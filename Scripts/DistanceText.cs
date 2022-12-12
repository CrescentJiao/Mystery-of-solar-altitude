using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.UI;

public class DistanceText : MonoBehaviour {
    
    public GameObject juminlou1;
    public GameObject juminlou2;
    // Use this for initialization
    void Start () {
         


    }

    // Update is called once per frame
    void Update () {
        //Vector3 p1 = juminlou1.transform.position;
        float dis = (juminlou1.transform.position - juminlou2.transform.position).sqrMagnitude;
        GameObject.Find("distance").GetComponent<TextMesh>().text="距离"+ (dis/3.5).ToString("f1")+"米";
    }
}
