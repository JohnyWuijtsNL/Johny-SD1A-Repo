using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript_FlappyCroissant : MonoBehaviour
{
    float spawnDelay = 1;
    float spawnTimer;
    [SerializeField]
    GameObject baguettes;
    GameObject currentBaguette;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            SpawnBaguettes();
            spawnTimer = spawnDelay;
        }
    }

    void SpawnBaguettes()
    {
        currentBaguette = Instantiate(baguettes);
        currentBaguette.transform.position = new Vector3(20, Random.Range(-2.5f, 2.5f), 0);
    }
}
