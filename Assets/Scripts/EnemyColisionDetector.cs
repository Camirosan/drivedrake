using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColisionDetector : MonoBehaviour
{

    public AudioSource dracCry;

    private void OnTriggerEnter(Collider other)
    {
        dracCry.Play();        
        Destroy(gameObject);
    }
}
