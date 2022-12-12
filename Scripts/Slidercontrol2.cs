using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SpringFramework2.UI;
using SpringFramework.UI;

public class Slidercontrol2 : MonoBehaviour
{
    public Slider slider2;
    // public Button button1;
    // public Button button2;
    public GameObject graph1;
    public GameObject graph2;
    public GameObject warning;
    float curvalue2;
    //bool flag = false;
    // Use this for initialization
    void Start()
    {
        //InvokeRepeating("setX", 0.1f,0.001f);
        curvalue2 = slider2.value;

        //Destroy(graph1.GetComponent<FunctionalGraph2>());
        // Destroy(graph2.GetComponent<FunctionalGraph>());

        FunctionSetX2.x2 = 0;
        FunctionSetX.x = 0;
        //graph1.GetComponent<FunctionalGraph2>() .enabled = false;
        //graph2.GetComponent<FunctionalGraph>().enabled = false;
        // print("------------------------------------------------------------------------------------------");
        //  graph2.GetComponent<FunctionalGraph>().enabled = true;
        //graph1.GetComponent<FunctionalGraph2>().enabled = true;
    }


    // Update is called once per frame
    void Update()
    {
        if (Atrackable.AtrackID == 1&& Btrackable.BtrackID != 1)
        {
           
            slider2.interactable = true;
            warning.SetActive(false );
            if (curvalue2 != slider2.value)
            {
            //也许可以判断一下有拖拽我再destroy 和 add
            Destroy(graph1.GetComponent<FunctionalGraph2>());
            FunctionSetX2.x2 = slider2.value;
            graph1.AddComponent<FunctionalGraph2>();
            curvalue2 = slider2.value;
           }
            else
            {
           graph1.AddComponent<FunctionalGraph2>();
            }
        }else if(Btrackable .BtrackID==1&&Atrackable.AtrackID!= 1)
        {
            
            slider2.interactable = true;
            warning.SetActive(false);
            if (curvalue2 != slider2.value)
            {
               
               Destroy(graph2.GetComponent<FunctionalGraph>());
                FunctionSetX.x = slider2.value;
                graph2.AddComponent<FunctionalGraph>();
                curvalue2 = slider2.value;
                
 
            }
            else
            {
                graph2.AddComponent<FunctionalGraph>();
            }
        }else  if(Atrackable.AtrackID == 1&& Btrackable.BtrackID == 1)
        {
            slider2.interactable = false;
            warning.SetActive(true);
        }

        
    }

}
