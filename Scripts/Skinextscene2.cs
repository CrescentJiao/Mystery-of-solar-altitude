﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Skinextscene2 : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(ButtonClick);

    }
    void ButtonClick()
    {
        SceneManager.LoadScene("scene3");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
