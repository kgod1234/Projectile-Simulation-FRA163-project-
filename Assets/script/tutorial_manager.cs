using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class tutorial_manager : MonoBehaviour
{
    public Image tutorial;
    public Button exist;
    // Start is called before the first frame update
    private void Start()
    {
        collapse();
    }
    public void Show()
    {
        tutorial.enabled = true;
        exist.image.enabled = true;
        exist.enabled = true;
    }
    public void collapse()
    {
        tutorial.enabled = false;
        exist.image.enabled = false;
        exist.enabled = false;
    }
}
