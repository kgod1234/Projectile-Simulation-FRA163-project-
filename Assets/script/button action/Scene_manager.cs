using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Changing_scene : MonoBehaviour
{
    private Shooting_info shooting_Info;
    private void Start()
    {
    }

    private void Awake()
    {
        shooting_Info = FindObjectOfType<Shooting_info>();
    }
    public void to_simulation()
    {
        if (shooting_Info.IsReady())
        {
            SceneManager.LoadScene("simulation");
        }
    }
    public void to_main_menu()
    {

        if (Shooting_info.Instance) Destroy(Shooting_info.Instance.gameObject);
        SceneManager.LoadScene("menu");
    }
    public void to_target()
    {
        SceneManager.LoadScene("target");
    }
    public void to_tutorial()
    {
        SceneManager.LoadScene("tutorial");
    }
}
