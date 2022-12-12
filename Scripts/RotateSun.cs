using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;
using UnityEngine.UI;

public class RotateSun : MonoBehaviour
{
    // public GameObject sandbox;
    float speed;
    public Slider slider;
    float currentvalue;
    float sunX = 0f;
    float sunY = 0f;
    float posX = 0f;
    float posY = 0f;
    // Use this for initialization
    void Start()
    {
        currentvalue = slider.value;

        transform.localEulerAngles = new Vector3(90, 90, 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (currentvalue != slider.value)
        {

            /* if (slider.value <= 20)
            {     speed=0;     }
           else if (currentvalue > 20 && currentvalue < 160)
            {
            speed = 45;
            }
             if (currentvalue > slider.value)
             {
                 transform.RotateAround(sandbox.transform.position, Vector3.back, speed * Time.deltaTime);
             }
             if (currentvalue < slider.value)
             {
                 transform.RotateAround(sandbox.transform.position, Vector3.forward , speed * Time.deltaTime);

              // transform.localEulerAngles = new Vector3(sunX, sunY, 0);
             }
             currentvalue = slider.value;

         }
         else
         {
             speed = 0;
         }*/

            if (slider.value == 90)
            {
                sunX = 90f;
                sunY = 90f;
                posX = 0;
                posY = 10f;

            }
            else if (slider.value < 90)
            {
                sunX = slider.value;
                sunY = 90f;
                posX = 0 - 0.015f * (90 - slider.value);//0.015=1.36/90
                posY = 1.36f - 0.015f * (90 - slider.value);

            }
            else if (slider.value > 90)
            {
                sunX = slider.value;
                sunY = 90f;
                posX = 0 + 0.015f * (slider.value - 90);
                posY = 1.36f - 0.015f * (slider.value - 90);

            }

            transform.localEulerAngles = new Vector3(sunX, sunY, 0);
            // transform .localPosition = new Vector3(posX, posY, 0);
            // print(transform.localPosition);
            transform.localPosition = new Vector3(posX, posY, 0);
            currentvalue = slider.value;




        }
    }
}
