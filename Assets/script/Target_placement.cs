using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using System.Linq;
using System;

public class Target_placement : MonoBehaviour
{
    private Shooting_info shooting_Info;
    public TMP_InputField Ydis; // Ydistance to targer input field
    public TMP_InputField Offset; // offset from the right side
    public TMP_Text errorText; // error text to warn user about invalid value
    public Image target;

    private float Ytarget;
    private float Roffset;

    // Start is called before the first frame update
    private void Awake()
    {
        shooting_Info = FindObjectOfType<Shooting_info>();
        Debug.Log(shooting_Info.getYtarget());
        Debug.Log(shooting_Info.getZtarget());
        SetEntryTxt();
    }

    // Update is called once per frame
    void Update()
    {
        if (string.IsNullOrEmpty(Ydis.text) || string.IsNullOrEmpty(Offset.text)) // Are the entry empty
        {
            errorText.text = "Please fill in all the fields.";
            shooting_Info.NotReady();
            return;
        }
        if (!float.TryParse(Ydis.text, out Ytarget)
           || !float.TryParse(Offset.text, out Roffset))
        {
            errorText.text = "Please enter valid values.";
            shooting_Info.NotReady();
            return;
        }
        else
        {
            if (Ytarget >= 0 && Roffset >= 0)
            {
                if (Ytarget < 75.5 && Roffset <= 55.5)
                {
                    errorText.text = "The target is below triangle plane.";
                    shooting_Info.NotReady();
                    target.enabled = false;
                    return;
                }
                else if(Ytarget > 124 && Roffset <= 55.5)
                {
                    errorText.text = "Maximum vertical distance from target is 124 cm.";
                    target.enabled = false;
                    shooting_Info.NotReady();
                }
                else if (Ytarget < 124 && Roffset > 55.5)
                {
                    errorText.text = "Maximum Roffset is 55.5 cm."; ;
                    target.enabled = false;
                    shooting_Info.NotReady();
                }
                else if (Ytarget > 124 && Roffset > 55.5)
                {
                    errorText.text = "Maximum vertical distance from target is 124 cm and Roffset is 55.5 cm.";
                    target.enabled = false;
                    shooting_Info.NotReady();
                }
                else
                {
                        errorText.text = "Ready to simulate.";
                        target.enabled = true;
                        target.rectTransform.anchoredPosition = new Vector2( 293.5f - ((Roffset * (293.5f + 9f) ) / 55.5f) ,
                                                        (((Ytarget - 75.5f) * 264.4f) / (55.5f * MathF.Sqrt(3) / 2)) - 162.3f
                                                      );
                }
            }
            else
            {
                errorText.text = "Please enter valid values.";
                target.enabled = false;
                shooting_Info.NotReady();
            }
        }
    }

    private void SetEntryTxt()
    {
        if (shooting_Info.getZtarget() == 0)
        {
            Offset.text = "";
        }
        else
        {
            Offset.text = (shooting_Info.getZtarget() * 100f + 27.75f).ToString();
        }

        if (shooting_Info.getYtarget() == 0)
        {
            Ydis.text = "";
        }
        else
        {
            Ydis.text = (shooting_Info.getYtarget() * 100).ToString();
        }
    }
}
