using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColisionDetector : MonoBehaviour
{

    public AudioSource dracCry;
    /*// Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/

    private void OnTriggerEnter(Collider other)
    {
        dracCry.Play();        
        Destroy(gameObject);
    }
}
