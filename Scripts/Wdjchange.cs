using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wdjchange : MonoBehaviour {
    public Slider slider;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (slider.value <= 20)
        {
            
        }
        else if (slider.value > 20 && slider.value <= 90)
        {
            transform.localScale = new Vector3(0.02f, 0.017f+slider.value/580,0.02f);
        }
        else if (slider.value > 90)
        {
            transform.localScale = new Vector3(0.02f, 0.36f - slider.value /500, 0.02f);
        }
    }
}
