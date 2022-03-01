using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESpawner : MonoBehaviour
{
    public GameObject enemy;
    float randX;
    Vector2 whereToSpawn;
    public float spawnrate = 2f;
    float nextSpawn = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnrate;
            randX = Random.Range(2.59f, 2.60f);
            whereToSpawn = new Vector2(randX, transform.position.y);
            Instantiate(enemy, whereToSpawn, Quaternion.identity);

        }
    }
}