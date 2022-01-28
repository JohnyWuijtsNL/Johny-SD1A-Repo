using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    int buttonNumber;

    bool isPressed = false;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y > 0.93f)
        {
            transform.position = new Vector3(transform.position.x, 0.93f, transform.position.z);
            isPressed = false;
        }
        if (transform.position.y < 0.9f)
        {
            transform.position = new Vector3(transform.position.x, 0.9f, transform.position.z);
            if (!isPressed)
            {
                gameManager.Teleport(buttonNumber);
                isPressed = true;
            }
        }
    }
}
