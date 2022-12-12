using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpringFramework.UI;

public class Slidercontrol : MonoBehaviour
{
    public Slider slider;
    public GameObject graph;
    float curvalue;
   // bool flag = false;
    // Use this for initialization
    void Start()
    {
        //InvokeRepeating("setX", 0.1f,0.001f);
        curvalue = slider.value;
        graph.AddComponent<FunctionalGraph>();
    }
    public void setX()
    {
        //graph.SetActive(false);

        //slider = GameObject.Find("Slider").GetComponent<Slider>();

        //Debug.Log(FunctionSetX.x);
        //UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(GameObject.Find("Capsule"), "添加力", "Force");

        //Destroy(GetComponent<Force>());


        //FunctionalGraph.drawline();
        //graph.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (curvalue != slider.value)
        {
            //也许可以判断一下有拖拽我再destroy 和 add
            Destroy(graph.GetComponent<FunctionalGraph>());
            FunctionSetX.x = slider.value;
            graph.AddComponent<FunctionalGraph>();
            curvalue = slider.value;
        }
        else
        {
            graph.AddComponent<FunctionalGraph>();
        }

    }

}