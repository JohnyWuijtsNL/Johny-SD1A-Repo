using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField]
    AudioScript ac;
    public Animator animator;

    public float moveSpeed = 5f;
    float bulletDelay = 0.2f;
    float bulletTimer = 0;
    int direction = 1;

    public Rigidbody2D rb;
    public Camera cam;

    [SerializeField]
    GameObject shootPosition;
    Vector3 shootDirection;
    Vector3 oldPos;
    Vector3 playerVel;


    Vector2 movement;


    public GameObject bulletPrefab;

    public float bulletForce = 200f;

    void Update()
    {
        if (oldPos == null)
        {
            oldPos = transform.position;
        }
        Debug.Log(playerVel);
        movement.x = Input.GetAxisRaw("Horizontal");
        animator.SetInteger("walkDir", (int)Input.GetAxisRaw("Horizontal"));
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            animator.SetBool("walkedRight", true);
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            animator.SetBool("walkedRight", false);
        }
        
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetBool("walkingVertical", Input.GetAxisRaw("Vertical") != 0 && Input.GetAxisRaw("Horizontal") == 0);
        shootDirection = Vector3.ClampMagnitude(new Vector3(Input.GetAxisRaw("HorizontalShoot"), -Input.GetAxisRaw("VerticalShoot"), 0), 1) * 0.2f;
        shootPosition.transform.position = transform.position + shootDirection;
        bulletTimer -= Time.deltaTime;

        


        if (new Vector2(Input.GetAxisRaw("HorizontalShoot"), -Input.GetAxisRaw("VerticalShoot")) != new Vector2(0, 0))
        {
            Shoot();
        }

        void Shoot()
        {
            if (bulletTimer <= 0)
            {
                ac.playSound(2);
                GameObject bullet = Instantiate(bulletPrefab, shootPosition.transform.position, Quaternion.identity);
                if (Input.GetAxisRaw("HorizontalShoot") > 0)
                {
                    animator.SetBool("threwRight", true);
                    direction = 1;
                }
                else if (Input.GetAxisRaw("HorizontalShoot") < 0)
                {
                    animator.SetBool("threwRight", false);
                    direction = -1;
                }
                bullet.GetComponent<Bullet>().direction = direction;
                animator.SetTrigger("throw");
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce((shootDirection * bulletForce) + playerVel * 50, ForceMode2D.Impulse);
                bulletTimer = bulletDelay;
            }

        }
    }


    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime);
        playerVel = (transform.position - oldPos);
        oldPos = transform.position;
    }
}