using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyGeneratorManager : MonoBehaviour
{
    public GameObject enemyDragon;
    public int enemyNumber = 3;
    GameObject enemyActive;
    public TextMeshProUGUI gameOverText;
    public string textGameOvere; 
    public TextMeshProUGUI dracRemainingText;
    public string textDracsRemaining;
    // Start is called before the first frame update
    void Start()
    {
        dracRemainingText.SetText(textDracsRemaining + (1 + enemyNumber).ToString());
    }
    
    // Update is called once per frame
    void Update()
    {
        if(enemyNumber > 0 && enemyActive == null)
        {
            enemyActive = InstantiateEnemy();
            enemyNumber--;
            Debug.Log($"Enemy number = {enemyNumber}");
            dracRemainingText.SetText(textDracsRemaining + (1 + enemyNumber).ToString());
        }
        else if(enemyNumber == 0 && enemyActive == null)
        {
            gameOverText.SetText(textGameOvere);
            dracRemainingText.SetText("");
        }
    }
    private GameObject InstantiateEnemy()
    {
        var rand = new System.Random();
        Vector3 position = new Vector3(Random.Range(-4, 4), Random.Range(-4, 4), Random.Range(-4, 4));//(float)rand.Next(-4, 4), (float)rand.Next(-4, 4), (float)rand.Next(-4, 4))
        return Instantiate(enemyDragon, position, enemyDragon.transform.rotation);
    }
}
