using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3f;

    // [SerializeField] private float attackDamage = 10f;

    private GameObject Player;

    
    void Start()
    {
        Player = GameObject.Find("Player");
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
    }

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            other.gameObject.GetComponent<HealthPlayer>().TakeDamage();
        }
    } 
   
}



