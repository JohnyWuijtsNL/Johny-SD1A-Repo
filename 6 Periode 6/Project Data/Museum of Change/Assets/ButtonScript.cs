using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    int buttonNumber;

    float coolDown = 0;
    float timer = 0.1f;

    // Update is called once per frame
    void Update()
    {
        coolDown -= Time.deltaTime;
        if (transform.position.y > 1.22f)
        {
            transform.position = new Vector3(transform.position.x, 1.22f, transform.position.z);
        }
        if (transform.position.y < 1.18f)
        {
            transform.position = new Vector3(transform.position.x, 1.18f, transform.position.z);
            if (coolDown <= 0)
            {
                gameManager.Teleport(buttonNumber);
                coolDown = timer;
            }
        }
    }
}
