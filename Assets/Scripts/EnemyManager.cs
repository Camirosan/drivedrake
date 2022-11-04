using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class EnemyManager : MonoBehaviour
{
    public Transform enemyTF;
    [Header("Movement Speed")]
    [Min(0.001f), Tooltip("Fordwars movement speed.")]
    public float TopSpeed;
    private Vector3 movementDirection;
    [Header("Change direction iteration")]
    [Min(10), Tooltip("The iterations needed to change enemy direction")]
    public int cambioDireccion = 500;
    private int cambioDireccionCounter;
    
    // Start is called before the first frame update
    void Start()
    {
        cambioDireccionCounter = cambioDireccion;
    }

    // Update is called once per frame
    void Update()
    {
        var rand = new System.Random();
        cambioDireccionCounter--;
        if (cambioDireccionCounter == 0)
        {

            //Debug.Log(Time.deltaTime);
            //Debug.Log($"initial direccion {enemyTF.rotation.eulerAngles}");
            enemyTF.rotation = Quaternion.Euler((float)rand.Next(-10, 10), (float)rand.Next(-90, 90), enemyTF.rotation.eulerAngles.z);
            //Debug.Log($"new direccion {enemyTF.rotation.eulerAngles}");
            cambioDireccionCounter = cambioDireccion;
        }
        movementDirection.x = (float)Math.Sin(DegToRad(enemyTF.rotation.eulerAngles.y));
        movementDirection.y = -1.0f * (float)Math.Sin(DegToRad(enemyTF.rotation.eulerAngles.x));
        movementDirection.z = (float)Math.Cos(DegToRad(enemyTF.rotation.eulerAngles.y));
        enemyTF.position += (movementDirection * TopSpeed);
        BorderDetection();
    }
    public double DegToRad(double value)
    {
        return (float)(value * 3.141516 / 180);
    }
    public void BorderDetection()
    {
        if (enemyTF.position.x >= 5)
        {
            enemyTF.position = new Vector3(-4.99f, enemyTF.position.y, enemyTF.position.z);
        }
        if (enemyTF.position.x <= -5)
        {
            enemyTF.position = new Vector3(4.99f, enemyTF.position.y, enemyTF.position.z);
        }
        if (enemyTF.position.y >= 5)
        {
            enemyTF.position = new Vector3(enemyTF.position.x, 0.01f, enemyTF.position.z);
        }
        if (enemyTF.position.y <= 0)
        {
            enemyTF.position = new Vector3(enemyTF.position.x, 4.99f, enemyTF.position.z);
        }
        if (enemyTF.position.z >= 5)
        {
            enemyTF.position = new Vector3(enemyTF.position.x, enemyTF.position.y, -4.99f);
        }
        if (enemyTF.position.z <= -5)
        {
            enemyTF.position = new Vector3(enemyTF.position.x, enemyTF.position.y, 4.99f);
        }
    }

    public void OnPointerEnter()
    {
        //SetMaterial(true);
        //Debug.Log("Señalando");
    }
    public void OnPointerExit()
    {
        //SetMaterial(false);
        //Debug.Log("Deja de señalar");
    }
    public void OnPointerClick()
    {
        //TeleportRandomly();
        //Debug.Log("DESTROY!");
    }
}
