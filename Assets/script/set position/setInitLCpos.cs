using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setInitLCpos : MonoBehaviour
{
    private Shooting_info shooting_Info;

    private float degrees;
    public Vector3 initialPosition;

    private float[,] pos = new float[6, 3]
    {
        { 35.06f, 0.07833f, 0.33928f }, // Row 0
        { 0.0f, 0.0f, 0.0f }, // Row 1
        { 0.0f, 0.0f, 0.0f }, // Row 2
        { 0.0f, 0.0f, 0.0f }, // Row 3
        { 0.0f, 0.0f, 0.0f }, // Row 4
        { 0.0f, 0.0f, 0.0f }  // Row 5
    };
    // Start is called before the first frame update

    private void Awake()
    {
        shooting_Info = FindObjectOfType<Shooting_info>();
        degrees = shooting_Info.getShootingDegrees();
        for (int i = 0; i < 6; i++)
        {
            if (degrees == pos[i, 0])
            {
                shooting_Info.SetXShooter(pos[i, 1]);
                shooting_Info.SetYShooter(pos[i, 2]);
                shooting_Info.SetZShooter(shooting_Info.getZtarget());
                break;
            }
        }
        Debug.Log(shooting_Info.getXShooter());
        Debug.Log(shooting_Info.getYShooter());
        if (shooting_Info.getXtarget() < 2)
        {
            initialPosition.x = 1.925f - shooting_Info.getXtarget() + shooting_Info.getXShooter();
        }
        else
        {
            initialPosition.x = shooting_Info.getXShooter() - 0.075f;
        }
        initialPosition.y = 0;
        initialPosition.z = 0.12f + shooting_Info.getZtarget();
    }
    void Start()
    {
        transform.position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
