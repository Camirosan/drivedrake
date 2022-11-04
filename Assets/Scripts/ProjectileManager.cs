using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    public Transform projectileTF;
    [Header("Movement Speed")]
    [Min(0.001f), Tooltip("Fordwars movement speed.")]
    public float TopSpeed;
    private Vector3 movementDirection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movementDirection.x = (float)Math.Sin(DegToRad(projectileTF.rotation.eulerAngles.y));
        movementDirection.y = -1.0f * (float)Math.Sin(DegToRad(projectileTF.rotation.eulerAngles.x));
        movementDirection.z = (float)Math.Cos(DegToRad(projectileTF.rotation.eulerAngles.y));
        projectileTF.position += (movementDirection * TopSpeed);
        BorderDetection();
    }
    public double DegToRad(double value)
    {
        return (float)(value * 3.141516 / 180);
    }
    public void BorderDetection()
    {
        if (projectileTF.position.x >= 5)
        {
            Destroy(gameObject);
            Destroy(projectileTF.parent);
            //projectileTF.position = new Vector3(-4.99f, projectileTF.position.y, projectileTF.position.z);
        }
        if (projectileTF.position.x <= -5)
        {
            Destroy(projectileTF.parent);
            //projectileTF.position = new Vector3(4.99f, projectileTF.position.y, projectileTF.position.z);
        }
        if (projectileTF.position.y >= 5)
        {
            Destroy(projectileTF.parent);
            //projectileTF.position = new Vector3(projectileTF.position.x, 0.01f, projectileTF.position.z);
        }
        if (projectileTF.position.y <= 0)
        { 
            Destroy(projectileTF.parent);
        //projectileTF.position = new Vector3(projectileTF.position.x, 4.99f, projectileTF.position.z);
        }
        if (projectileTF.position.z >= 5)
        {
            Destroy(gameObject);
            Destroy(projectileTF.parent);
            //projectileTF.position = new Vector3(projectileTF.position.x, projectileTF.position.y, -4.99f);
        }
        if (projectileTF.position.z <= -5)
        {
            Destroy(projectileTF.parent);
            //projectileTF.position = new Vector3(projectileTF.position.x, projectileTF.position.y, 4.99f);
        }
    }
}
