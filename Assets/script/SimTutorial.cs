using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimTutorial : MonoBehaviour
{

    public Image tutorial1;
    public Image tutorial2;
    public Button Next;
    public Button exist;
    private void Start()
    {
        collapseAll();
    }
    public void Show()
    {
        tutorial1.enabled = true;
        exist.image.enabled = true;
        exist.enabled = true;
        Next.image.enabled = true;
        Next.enabled = true;
    }
    public void Next_pg()
    {
        tutorial1.enabled = false;
        tutorial2.enabled = true;
        Next.image.enabled = false;
        Next.enabled = false;
        exist.image.enabled = true;
        exist.enabled = true;
    }
    public void collapseAll()
    {
        tutorial1.enabled = false;
        tutorial2.enabled = false;
        exist.image.enabled = false;
        exist.enabled = false;
        Next.image.enabled = false;
        Next.enabled = false;
    }
}
