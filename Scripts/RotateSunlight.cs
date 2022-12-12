using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RotateSunlight : MonoBehaviour {
   public Slider slider;
   //float angle;
  //  public static Quaternion Euler(float x, float y, float z);
    //public static Quaternion Euler(Vector3 euler);
    // Use this for initialization
    void Start () {
        //angle = slider.value/2;
	}
	
	// Update is called once per frame
	void Update () {
        //  this.transform.Rotate(angle, 0, 0);
        //transform.localEulerAngles = new Vector3(-90, 90, 0); 赋值旋转
        // transform.rotation = Quaternion.Euler(angle, -90, 0);
        //if(angle)
        Vector3 rotationVector = new Vector3(-30,90, 0);
        transform.rotation = Quaternion.Euler(rotationVector);
    }
}
