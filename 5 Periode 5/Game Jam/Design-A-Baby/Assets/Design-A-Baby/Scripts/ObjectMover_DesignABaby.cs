using UnityEngine;

public class ObjectMover_DesignABaby : MonoBehaviour
{
    [SerializeField]
    int partType;
    float fallSpeed = 30;
    SpawnerScript_DesignABaby spawnerScript;
    float moveSpeed = 20;
    bool moveRight = true;
    bool moving = true;
    public bool falling = true;
    [SerializeField]
    Sprite[] sprites;

    private void Start()
    {
        if (partType != 0)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
        }
        else
        {
            if (Random.Range(0, 101) < 95)
            {
                GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length - 1)];
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = sprites[sprites.Length - 1];
            }
        }
        if (partType == 666)
        {
            return;
        }
        moveSpeed = Random.Range(20f, 35f);
        spawnerScript = GameObject.Find("Spawner").GetComponent<SpawnerScript_DesignABaby>();
        switch (partType)
        {
            case 0:
                transform.position = new Vector2(Random.Range(3.6f, 15.6f), 9f);
                break;
            case 1:
                transform.position = new Vector2(7f, Random.Range(0f, 10.8f));
                break;
            case 2:
            case 4:
                transform.position = new Vector2(Random.Range(3.6f, 15.6f), 1.8f);
                break;
            case 3:
                transform.position = new Vector2(12.2f, Random.Range(0f, 10.8f));
                break;
        }
    }

    void Update()
    {
        if (partType == 666)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) && moving)
        {
            moving = false;
            spawnerScript.PlaySound(1);
        }
        if (moving)
        {
            Move();
        }
        else if (falling)
        {
            Fall();
        }

        if (Mathf.Abs(transform.position.x) > 25 || Mathf.Abs(transform.position.y) > 25)
        {
            Destroy(this.gameObject);
            spawnerScript.EndGame(false);
        }
    }

    void Move()
    {
        switch (partType)
        {
            case 0: case 2:
            case 4:
                if (transform.position.x < 15.6f && moveRight)
                {
                    transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
                }
                else
                {
                    moveRight = false;
                }

                if (transform.position.x > 3.6f && !moveRight)
                {
                    transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
                }
                else
                {
                    moveRight = true;
                }
                break;
            case 1: case 3:
                if (transform.position.y < 10.8f && moveRight)
                {
                    transform.Translate(0, moveSpeed * Time.deltaTime, 0);
                }
                else
                {
                    moveRight = false;
                }

                if (transform.position.y > 0 && !moveRight)
                {
                    transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
                }
                else
                {
                    moveRight = true;
                }
                break;
        }
    }

    void Fall()
    {
        switch(partType)
        {
            case 0:
                transform.Translate(0, -fallSpeed * Time.deltaTime, 0);
                break;
            case 1:
                transform.Translate(fallSpeed * Time.deltaTime, 0, 0);
                break;
            case 2:
            case 4:
                transform.Translate(0, fallSpeed * Time.deltaTime, 0);
                break;
            case 3:
                transform.Translate(-fallSpeed * Time.deltaTime, 0, 0);
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (partType == 666)
        {
            return;
        }
        spawnerScript.PlaySound(0);
        falling = false;
        if (partType == 3)
        {
            spawnerScript.EndGame(true);
        }
    }
}
