using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.UI;

public class launcher : MonoBehaviour
{
    public GameObject projectile;
    public TMP_Text SpringConst_text;
    public TMP_Text DeltaX_text;
    public TMP_Text Init_text;
    public TMP_Text X_text;
    public TMP_Text Z_text;
    public Button Launching_bt;

    //public Transform LauncherPos;
    public Transform ShootingPos;
    public Transform TargetPos;

    public int Resolution = 30;
    public Transform[] LaunchersPos = new Transform[6];
    private float gravity = 9.81f;
    private int index = 0;

    private Shooting_info shooting_Info;

    private float degrees;
    private float degreesInRad;
    private Vector3 ShootingPosition;
    private Vector3 LauncherPosition;
    private Vector3 TargetPosition;
    public LineRenderer lineRenderer;

    private bool Play = false;

    private float[,] pos = new float[6, 3]
    {
        { 35.06f, 0.07833f, 0.33928f }, // Row 0
        { 40.06f, 0.0564f, 0.36892f }, // Row 1
        { 45.06f, 0.0247f, 0.39604f }, // Row 2
        { 50.06f, -0.01194f, 0.42043f }, // Row 3
        { 55.06f, -0.04636f, 0.44189f }, // Row 4
        { 57.89f, -0.06669f, 0.45272f }  // Row 5
    };

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

    private void InitializeData()
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
                index = i;
                break;
            }
        }

        // setting Launcher Position
        if (index == 0)
        {
            if (shooting_Info.getXtarget() < 2)
            {
                LauncherPosition.x = 1.925f - shooting_Info.getXtarget() + shooting_Info.getXShooter();
            }
            else
            {
                LauncherPosition.x = shooting_Info.getXShooter() - 0.075f;
            }
            LauncherPosition.y = 0;
            LauncherPosition.z = 0.12f + shooting_Info.getZtarget();
        }
        else if (index == 1)
        {
            LauncherPosition.x = 2 - shooting_Info.getXtarget();
            LauncherPosition.y = 0;
            LauncherPosition.z = -0.0684f + shooting_Info.getZtarget();
        }
        else if (index > 1 && index < 6)
        {
            LauncherPosition.x = 2 - shooting_Info.getXtarget();
            LauncherPosition.y = 0;
            LauncherPosition.z = 0.121f + shooting_Info.getZtarget();
        }
        // setting launching position
        if (shooting_Info.getXtarget() < 2)
        {
            ShootingPosition.x = 2 - shooting_Info.getXtarget() + shooting_Info.getXShooter();
        }
        else
        {
            ShootingPosition.x = shooting_Info.getXShooter();
        }
        ShootingPosition.y = shooting_Info.getYShooter();
        ShootingPosition.z = shooting_Info.getZtarget();

        // setting Target position
        TargetPosition.x = 2;
        TargetPosition.y = shooting_Info.getYtarget();
        TargetPosition.z = shooting_Info.getZtarget();
    }

    LaunchData CalLaunchingData()
    {
        float xdis = shooting_Info.getXtarget() - shooting_Info.getXShooter();
        float ydis = shooting_Info.getYtarget() - shooting_Info.getYShooter();
        float InitVelocity = MathF.Sqrt((xdis * xdis * gravity) /
            (2f * MathF.Cos(degreesInRad) * MathF.Cos(degreesInRad) * (xdis * MathF.Tan(degreesInRad) - ydis)));

        float k = shooting_Info.getSpringConst();
        float delta_x = MathF.Sqrt(0.2f * (InitVelocity * InitVelocity - 2f * gravity * (shooting_Info.getYShooter())) / k);

        return new(InitVelocity, 
                    InitVelocity * MathF.Cos(degreesInRad), 
                    InitVelocity * MathF.Sin(degreesInRad), 
                    k, 
                    delta_x);
    }

    private void OnAction()
    {
        Play = true;
    }

    private void pathPredict()
    {
        Vector3[] points = new Vector3[Resolution];
        LaunchData launchData = CalLaunchingData();
        Vector3 prevPoint = ShootingPosition;

        float total_t = (2 * launchData.Vy) / gravity;
        for (int i = 0; i < Resolution; i++)
        {
            float t = ( i/ (float)Resolution) * total_t;
            Vector3 displacement = new Vector3 (launchData.Vx * t,
                                    launchData.Vy * t - 0.5f * gravity * t * t ,
                                    0f
                                    );
            Vector3 drawPoint = ShootingPosition + displacement;
            Debug.DrawLine(prevPoint, drawPoint, Color.green);
            points[i] = drawPoint;
            prevPoint = drawPoint;
        }
        lineRenderer.SetPositions(points);
    }

    private void launch()
    {
        GameObject ball = Instantiate(projectile, ShootingPos.position,
                                                      ShootingPos.rotation);
        ball.GetComponent<Rigidbody>().velocity = new Vector3
                                             (launchData.Vx ,
                                             launchData.Vy + 0.08f,
                                             0f);
    }


    private void Awake()
    {
        lineRenderer.positionCount = Resolution;
        Physics.gravity = Vector3.up * -gravity;
    }
    void Start()
    {
        InitializeData();

        // assign the position of component in the scene
        ShootingPos.position = ShootingPosition;
        LaunchersPos[index].position = LauncherPosition;
        TargetPos.position = TargetPosition;

        launchData = CalLaunchingData();
        Debug.Log(launchData.V);
        Debug.Log(launchData.Vx);
        Debug.Log(launchData.Vy);
        Debug.Log(Physics.gravity);
        Debug.Log(TargetPosition);
        SpringConst_text.text = launchData.springConstant.ToString() + " N/m";
        DeltaX_text.text = (launchData.Delta_x * 100).ToString() + " cm";
        Init_text.text = launchData.V.ToString() + " m/s";
        Launching_bt.onClick.AddListener(OnAction);
        X_text.text = (shooting_Info.getXtarget() * 100).ToString() + " cm";
        Z_text.text = (shooting_Info.getZtarget() * 100).ToString() + " cm";
    }
    // Update is called once per frame
    void Update()
    {
        if (Play)
        {
            launch();
        }
        pathPredict();
        Play = false;

    }
}
