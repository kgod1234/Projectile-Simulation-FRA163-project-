using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class togglecam : MonoBehaviour
{
    private int x = 1;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Image panel;
    public Image panel1;
    public TMP_Text text1;
    public TMP_Text text2;
    public TMP_Text text3;
    public TMP_Text text4;
    public TMP_Text text5;

    private void Start()
    {
        cam1.enabled = true;
        cam2.enabled = false;
        cam3.enabled = false;
    }
    public void switchcam()
    {
        if (x < 3)
        {
            x = x + 1;
        }
        else
        {
            x = 1;
        }
        deactivateall();
        if (x == 1)
        {
            cam1.enabled = true;
            cam2.enabled = false;
            cam3.enabled = false;
            panel.enabled = true;
            panel1.enabled = true;

            text1.enabled = true;
            text2.enabled = true;
            text3.enabled = true;
            text4.enabled = true;
            text5.enabled = true;
        }
        else if (x == 2)
        {
            cam2.enabled = true;
            cam1.enabled = false;
            cam3.enabled = false;
            panel.enabled = true;
            panel1.enabled = true;

            text1.enabled = true;
            text2.enabled = true;
            text3.enabled = true;
            text4.enabled = true;
            text5.enabled = true;
        }
        else
        {
            cam3.enabled = true;
            cam2.enabled = false;
            cam1.enabled = false;
            panel.enabled = false;
            panel1.enabled = false;

            text1.enabled = false;
            text2.enabled = false;
            text3.enabled = false;
            text4.enabled = false;
            text5.enabled = false;
        }
    }

    public void deactivateall()
    {
        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = false;
    }
}