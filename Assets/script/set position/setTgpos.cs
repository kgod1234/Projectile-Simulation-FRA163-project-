using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class setTgpos : MonoBehaviour
{
    private Shooting_info shooting_Info;

    private float degrees;
    public Vector3 initialPosition;

    private void Awake()
    {
        shooting_Info = FindObjectOfType<Shooting_info>();
        initialPosition.x = 2;
        initialPosition.y = shooting_Info.getYtarget() - 0.05f;
        initialPosition.z = shooting_Info.getZtarget();
    }

    // Start is called before the first frame update
    void Start()
    {
        transform.position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
