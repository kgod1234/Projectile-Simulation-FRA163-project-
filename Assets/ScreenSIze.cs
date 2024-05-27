using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSIze : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Set the screen resolution to 1920x1080 and fullscreen mode
        SetScreenSize(1280, 720, false);
    }

    public void SetScreenSize(int width, int height, bool fullscreen)
    {
        Screen.SetResolution(width, height, fullscreen);
    }
}
