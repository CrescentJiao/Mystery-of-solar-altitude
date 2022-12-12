using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTemperat : MonoBehaviour {
    public static Text txt;
    public Slider slider;
    float temp;
   
    // Use this for initialization
    void Start () {

        txt = GameObject.Find("Temperature").GetComponent<Text>();
       }
	
	// Update is called once per frame
	void Update () {
       
            if (slider.value <= 20)
        {
            temp = 20;
           }else if( slider.value >20 && slider.value<90)
        {
            temp = 20+slider.value/3;
        }
        else if(slider.value >=90)
        {
            temp=50-(slider.value-90 )/2;
        }
        txt.text = "温度"+ temp.ToString ("f1") +"℃";
	}
}
