using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGeneratorManager : MonoBehaviour
{
    public GameObject enemyDragon;

    // Start is called before the first frame update
    void Start()
    {
        var rand = new System.Random();
        Vector3 position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), Random.Range(-4, 4));//(float)rand.Next(-4, 4), (float)rand.Next(-4, 4), (float)rand.Next(-4, 4))
        Instantiate(enemyDragon, position, enemyDragon.transform.rotation);
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
