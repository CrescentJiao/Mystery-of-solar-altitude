using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class RotateSun1 : MonoBehaviour
{
    
    float speed;
    
   
    // Use this for initialization
    void Start()
    {
        speed = 20;
    }

    // Update is called once per frame
    void Update()
    {
         transform.RotateAround(new Vector3 (0,7,0), Vector3.up, speed * Time.deltaTime);
         
    }
}
