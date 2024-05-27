using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // for Input field and etc. which involve in ui things.

public class passing_value : MonoBehaviour
{
    public TMP_InputField SpringConst; // Spring Constant Input field
    public TMP_InputField Xdis; // Xdistance to target input field
    public TMP_InputField Ydis; // Ydistance to targer input field
    public TMP_InputField Offset; // offset from the right side
    public TMP_Text errorText; // error text to warn user about invalid value
    public TMP_Dropdown degrees_dd; // dropdown degrees

    private float Xtarget;
    private float Ytarget;
    private float Roffset;
    private float Kvalue;

    private Shooting_info shooting_Info; // to find Shooting info
    private void Start()
    {
        shooting_Info = FindObjectOfType<Shooting_info>();
        Debug.Log(shooting_Info.getXtarget());
        Debug.Log(shooting_Info.getYtarget());
        Debug.Log(shooting_Info.getZtarget());
        SetEntryTxt();
    }

    private void Awake()
    {
        
    }
    private void Update()
    {
        if (string.IsNullOrEmpty(Xdis.text) || string.IsNullOrEmpty(Ydis.text) || string.IsNullOrEmpty(Offset.text) || string.IsNullOrEmpty(SpringConst.text))
        {
            errorText.text = "Please fill in all the fields.";
            return;
        } // check if the entry is empty

        if (!float.TryParse(Xdis.text, out Xtarget)
            || !float.TryParse(Ydis.text, out Ytarget)
            || !float.TryParse(Offset.text, out Roffset)
            || !float.TryParse(SpringConst.text, out Kvalue))
        {
            errorText.text = "Please enter valid values.";
            return;
        }
        else
        {
            if (Xtarget > 120 && Ytarget > 0 && Roffset >= 0 && Kvalue > 0)
            {
                if (Roffset > 55.5)
                {
                    errorText.text = "Maximum Roffset is 55.5 cm.";
                    shooting_Info.NotReady();
                    return;
                }
                else if (Xtarget > 200 && Ytarget <= 124)
                {
                    errorText.text = "Maximum horizontal distance from target is 200 cm.";
                    shooting_Info.NotReady();
                    return;
                }
                else if (Xtarget <= 200 && Ytarget > 124)
                {
                    errorText.text = "Maximum vertical distance from target is 124 cm.";
                    shooting_Info.NotReady();
                    return;
                }
                else if (Xtarget > 200 && Ytarget > 124)
                {
                    errorText.text = "Xdis should be below 200 cm and Ydis below 124 cm.";
                    shooting_Info.NotReady();
                    return;
                }
                else if (Xtarget <= 200 && Ytarget <= 124)
                {
                    shooting_Info.Ready();
                }
            }
            else
            {
                if (Xtarget < 120)
                {
                    errorText.text = "Xdis must be more than 120 the wall is in the way.";
                    shooting_Info.NotReady();
                    return;
                }
                else
                {
                    errorText.text = "All value must be positive or greater than 0.";
                    shooting_Info.NotReady();
                    return;
                }
            }

        }
    }

    public void passing_input() // to passing value to simulation page 
    {
        if (shooting_Info.IsReady())
        {
            string Raw = degrees_dd.options[degrees_dd.value].text;
            float degrees = float.Parse(Raw.Split(' ')[0]);
            shooting_Info.SetXtarget(Xtarget / 100);
            shooting_Info.SetYtarget(Ytarget / 100);
            shooting_Info.SetZtarget((-27.75f + Roffset) / 100);

            shooting_Info.SetShootingDegrees(degrees);
            shooting_Info.SetSpringConst(Kvalue);
            // Debug logs
            Debug.Log($"Raw: {Raw}");
            Debug.Log($"Degrees: {degrees}");

            Debug.Log($"Xtarget: {Xtarget / 100}");
            Debug.Log($"Ytarget: {Ytarget / 100}");
            Debug.Log($"Ztarget: {(27.75f - Roffset) / 100}");

            Debug.Log($"Shooting Degrees: {degrees}");
            Debug.Log(shooting_Info.IsReady());
        }
    }
    public void passing_to_target() // passin value to target page
    {
        if (!string.IsNullOrEmpty(Ydis.text))
        {
            float.TryParse(Ydis.text, out Ytarget);
        }
        else
        {
            Ytarget = 0;
        }
        if (!string.IsNullOrEmpty(Ydis.text))
        {
            float.TryParse(Offset.text, out Roffset);
        }
        else
        {
            Roffset = 0;
        }
        float.TryParse(Offset.text, out Roffset);
        if (Roffset >= 0 && Roffset <= 55.5 && Ytarget >= 0 && Ytarget <= 124)
        {
            Debug.Log(Ytarget);
            Debug.Log(Roffset);
            shooting_Info.SetYtarget(Ytarget / 100);
            if(Roffset == 0)
            {
                shooting_Info.SetZtarget(0);
            }
            else
            {
                shooting_Info.SetZtarget((Roffset / 100f) - 0.2775f);
            }
        }
        
    }

    private void SetEntryTxt() // to set the text in entry
    {
        
        if (shooting_Info.getZtarget() != 0)
        {
            Xdis.text = (shooting_Info.getXtarget() * 100).ToString();
        }
        else
        {
            Xdis.text = "";
        }

        if (shooting_Info.getZtarget() != 0)
        {
            Ydis.text = (shooting_Info.getYtarget() * 100).ToString();
        }
        else
        {
            Ydis.text = "";
        }

        if (shooting_Info.getZtarget() != 0)
        {
            Offset.text = (shooting_Info.getZtarget() * 100f + 27.75f).ToString();
        }
        else
        {
            Offset.text = "";
        }

        if (shooting_Info.getZtarget() != 0)
        {
            SpringConst.text = shooting_Info.getSpringConst().ToString();
        }
        else
        {
            SpringConst.text = "";
        }
    }
    public void Reset()
    {
        shooting_Info.SetXtarget(0);
        shooting_Info.SetYtarget(0);
        shooting_Info.SetZtarget(0);
        shooting_Info.SetSpringConst(0);
        SetEntryTxt();
    }
}
