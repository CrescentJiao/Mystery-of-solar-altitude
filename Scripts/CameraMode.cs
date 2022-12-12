using UnityEngine;
using System.Collections;
using Vuforia;

public class CameraMode : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

        VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
        VuforiaARController.Instance.RegisterOnPauseCallback(OnPaused);
    }

    void Update()
    {
    //ÍË³ö¹¦ÄÜ
		 if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnVuforiaStarted()
    {
        CameraDevice.Instance.SetFocusMode(
            CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }

    private void OnPaused(bool paused)
    {
        if (!paused)
        { // resumed
            // Set again autofocus mode when app is resumed
            CameraDevice.Instance.SetFocusMode(
                CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
        }
    }
}