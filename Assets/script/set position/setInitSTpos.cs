using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class set_initial_pos : MonoBehaviour
{
    public GameObject projectile;
    public TMP_Text SpringConst_text;
    public TMP_Text DeltaX_text;
    public TMP_Text Init_text;

    private Shooting_info shooting_Info;

    private float degrees;
    private float degreesInRad;
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

    struct LaunchData 
    {
        public readonly float V;
        public readonly float Vx;
        public readonly float Vy;
        public readonly float springConstant;
        public readonly float Delta_x;
        public LaunchData(float v, float vx, float vy, float k, float x)
        {
            Vx = vx;
            Vy = vy;
            V = v;
            springConstant = k;
            Delta_x = x;
        }
    }
    LaunchData launchData;
    private void Awake()
    {
        shooting_Info = FindObjectOfType<Shooting_info>();
        degrees = shooting_Info.getShootingDegrees();
        degreesInRad = degrees * Mathf.Deg2Rad;
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
            initialPosition.x = 2 - shooting_Info.getXtarget() + shooting_Info.getXShooter();
        }
        else
        {
            initialPosition.x = shooting_Info.getXShooter();
        }
        initialPosition.y = shooting_Info.getYShooter();
        initialPosition.z = shooting_Info.getZtarget();
    }

    LaunchData CalLaunchingData()
    {
        float xdis = shooting_Info.getXtarget() - shooting_Info.getXShooter();
        float ydis = shooting_Info.getYtarget() - shooting_Info.getYShooter();
        float InitVelocity = MathF.Sqrt( ( xdis * xdis * 9.81f ) /
            (2 * MathF.Cos(degreesInRad) * MathF.Cos(degreesInRad) * (xdis * MathF.Tan(degreesInRad) - ydis) ) );

        float k = shooting_Info.getSpringConst();
        float delta_x = MathF.Sqrt(0.2f * (InitVelocity* InitVelocity - 2 * 9.81f * (shooting_Info.getYShooter()) ) / k);
        return new (InitVelocity,InitVelocity * MathF.Cos(degreesInRad), InitVelocity * MathF.Sin(degreesInRad), k, delta_x);
    }
    void Start()
    {
        transform.position = initialPosition;
        transform.Rotate(0, 0, 0);
        launchData = CalLaunchingData();
        Debug.Log(launchData.V);
        Debug.Log(launchData.Vx);
        Debug.Log(launchData.Vy);
        Debug.Log(Physics.gravity);
        SpringConst_text.text = launchData.springConstant.ToString() + " N/m";
        DeltaX_text.text = (launchData.Delta_x * 100).ToString() + " cm";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject ball = Instantiate(projectile, transform.position,
                                                      transform.rotation);
            ball.GetComponent<Rigidbody>().velocity = new Vector3
                                                 (launchData.Vx,
                                                 launchData.Vy, 
                                                 0f);
        }
    }
}
