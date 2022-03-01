using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    Vector2 oldPos;
    float lookDirection;
    [SerializeField]
    float rotateSpeed;
    public int direction;
    AudioScript ac;

    private void Start()
    {
        ac = GameObject.Find("Main Camera").GetComponent<AudioScript>();  
    }
    private void Update()
    {
        /*if (oldPos == null)
        {
            oldPos = transform.position;
        }
        lookDirection = -Vector2.Angle(transform.position, oldPos);
        transform.eulerAngles = new Vector3(0, 0, lookDirection);

        oldPos = transform.position;*/
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime * direction);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyHit")
        {
            Score.scoreAmount += 100;
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            ac.playSound(0);
        }

        if (collision.gameObject.tag == "Border")
        {
            Destroy(this.gameObject);
        }
    }

}
