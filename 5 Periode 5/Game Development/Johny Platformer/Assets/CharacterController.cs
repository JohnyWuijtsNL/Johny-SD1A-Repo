using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    Rigidbody2D playerRB;
    [SerializeField]
    float playerSpeed = 5;
    float jumpVelocity = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpVelocity = 20;
        }

        transform.Translate((Input.GetAxis("Horizontal") * playerSpeed * Time.deltaTime), jumpVelocity * Time.deltaTime, 0);

        if (jumpVelocity > 0)
        {
            jumpVelocity -= 10.81f * Time.deltaTime;
        }
        


    }
}
