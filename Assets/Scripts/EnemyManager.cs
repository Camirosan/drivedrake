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
    [Min(1), Tooltip("The iterations needed to change enemy direction")]
    public int cambioDireccion = 5;
    private float cambioDireccionCounter;
    public AudioSource dracCry;
    // Start is called before the first frame update
    void Start()
    {
        cambioDireccionCounter = cambioDireccion;
    }

    // Update is called once per frame
    void Update()
    {
        var rand = new System.Random();
        Debug.Log($"Cambio direction counter = {cambioDireccionCounter}");
        cambioDireccionCounter -= Time.deltaTime;
        if (cambioDireccionCounter <= 0)
        {
            enemyTF.rotation = Quaternion.Euler((float)rand.Next(-10, 10), (float)rand.Next(-90, 90), enemyTF.rotation.eulerAngles.z);
            cambioDireccionCounter = cambioDireccion;
        }
        movementDirection.x = (float)Math.Sin(DegToRad(enemyTF.rotation.eulerAngles.y));
        movementDirection.y = -1.0f * (float)Math.Sin(DegToRad(enemyTF.rotation.eulerAngles.x));
        movementDirection.z = (float)Math.Cos(DegToRad(enemyTF.rotation.eulerAngles.y));
        enemyTF.position += (movementDirection * TopSpeed * Time.deltaTime);
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
        if (enemyTF.position.y >= 4.99f)
        {
            enemyTF.position = new Vector3(enemyTF.position.x, -0.99f, enemyTF.position.z);
        }
        if (enemyTF.position.y <= -1.0f)
        {
            enemyTF.position = new Vector3(enemyTF.position.x, 3.99f, enemyTF.position.z);
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

    private void OnDestroy()
    {
        dracCry.Play();        
    }
}
