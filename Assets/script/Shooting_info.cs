using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting_info : MonoBehaviour
{
    private static Shooting_info instance;
    public static Shooting_info Instance => instance;

    private float Xtarget = 0;
    private float Ytarget = 0;
    private float Ztarget = 0;

    private float Xshooter = 0;
    private float Yshooter = 0;
    private float Zshooter = 0;

    private float ShootingDegrees = 0;
    private float K = 0;

    private bool ready = false; // to check if the information is correct

    private void Awake()
    {
        // Does another instance already exist?
        if (instance && instance != this)
        {
            // Destroy myself
            Destroy(gameObject);
            return;
        }

        // Otherwise store my reference and make me DontDestroyOnLoad
        instance = this;
        // Ensure that this GameObject persists across scene loads.
        DontDestroyOnLoad(gameObject);
        Debug.Log(ready);
    }

    private void Start()
    { }

    // Method for Target position
    // To assign new value and getting current value
    public float getXtarget() { return Xtarget; }
    public float getYtarget() { return Ytarget; }
    public float getZtarget() { return Ztarget; }

    public void SetXtarget(float X) { Xtarget = X; }
    public void SetYtarget(float Y){ Ytarget = Y; }
    public void SetZtarget(float Z) { Ztarget = Z; }

    // Method for Shooter position
    // To assign new value and getting current value
    public float getXShooter() { return Xshooter; }
    public float getYShooter() { return Yshooter; }
    public float getShooter() { return Zshooter; }
    public void SetXShooter(float X) { Xshooter = X; }
    public void SetYShooter(float Y) { Yshooter = Y; }
    public void SetZShooter(float Z) { Zshooter = Z; }

    public float getShootingDegrees() { return ShootingDegrees; }
    public void SetShootingDegrees(float Dg) { ShootingDegrees = Dg; }

    public float getSpringConst() { return K; }
    public void SetSpringConst(float k) { K = k; }

    public bool IsReady() // To ask an object if the information is ready
    {
        return ready;
    }

    public void NotReady()
    {
        ready = false;
    }
    public void Ready()
    {
        ready = true;
    }
    public void Reset()
    {
        Xshooter = 0;
        Yshooter = 0;
        Zshooter = 0;

        ShootingDegrees = 0;
        K = 0;
    }
}
