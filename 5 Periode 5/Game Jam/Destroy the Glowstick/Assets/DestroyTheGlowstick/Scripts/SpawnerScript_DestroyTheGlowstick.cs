using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnerScript_DestroyTheGlowstick : MonoBehaviour
{
    GameManager gameManager;

    int glowstickCount;
    [SerializeField]
    GameObject glowstick;
    GameObject currentGlowstick;
    bool isLaserRed = true;
    List<GlowstickScript_DestroyTheGlowstick> glowsticks;
    [SerializeField]
    GameObject player;
    float playerSpeed = 100;
    [SerializeField]
    Transform[] laserHoles;
    [SerializeField]
    GameObject[] lasers;
    float laserHit;
    bool hasHit = false;
    GameObject currentLaser;
    bool laserShot = false;
    bool currentLaserIsRed = false;
    bool bounce = false;
    bool failed = false;
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] audioClips;
    [SerializeField]
    Sprite[] playerSprites;

    // Start is called before the first frame update
    void Start()
    {
        glowstickCount = Random.Range(12, 17);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        gameManager.StartGame(7, false, "Destroy the glowstick!");
        audioSource = GetComponent<AudioSource>();
        glowsticks = new List<GlowstickScript_DestroyTheGlowstick>(glowstickCount);
        int i = 0;
        while (i < glowstickCount)
        {
            currentGlowstick = Instantiate(glowstick);
            glowsticks.Add(currentGlowstick.GetComponent<GlowstickScript_DestroyTheGlowstick>());
            glowsticks[i].glowstickNumber = i;
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (glowsticks.Count != 0)
        {
            if (!failed)
            {
                if (Input.GetKeyDown(KeyCode.Space) && !laserShot)
                {
                    audioSource.PlayOneShot(audioClips[0]);
                    if (glowsticks[0].isRed == isLaserRed)
                    {
                        hasHit = true;
                        laserHit = 0.1f;
                    }
                    if (isLaserRed)
                    {
                        currentLaser = Instantiate(lasers[0]);
                        currentLaser.transform.position = laserHoles[0].position;
                    }
                    else
                    {
                        currentLaser = Instantiate(lasers[1]);
                        currentLaser.transform.position = laserHoles[1].position;
                    }
                    currentLaserIsRed = isLaserRed;
                    laserShot = true;
                }
                if (Input.GetKeyDown(KeyCode.A))
                {
                    isLaserRed = false;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    isLaserRed = true;
                }
            }
            

            MovePlayer(isLaserRed);
            MoveLaser();
            if (hasHit)
            {
                laserHit -= Time.deltaTime;
                if (laserHit < 0)
                {
                    hasHit = false;
                    Destroy(glowsticks[0].gameObject);
                    glowsticks.RemoveAt(0);
                    audioSource.PlayOneShot(audioClips[1]);
                    foreach (GlowstickScript_DestroyTheGlowstick glowstick in glowsticks)
                    {
                        glowstick.MoveOn();
                    }
                }
            }

            if (failed)
            {
                if (laserHit > 0)
                {
                    laserHit -= Time.deltaTime;
                }
                else
                {
                    failed = false;
                    player.GetComponent<SpriteRenderer>().sprite = playerSprites[0];
                }
                player.transform.eulerAngles = new Vector3(0, 0, Random.Range(-15, 15f));
            }
            else
            {
                player.transform.eulerAngles = Vector3.zero;
            }
        }

        else
        {
            gameManager.SetWon(true);
            SceneManager.LoadScene(0);
        }


    }

    void MovePlayer(bool isRed)
    {
        if (failed)
        {
            // return;
        }
        float targetX;
        if (isRed)
        {
            targetX = 17;
            if (player.transform.position.x < targetX)
            {
                player.transform.Translate(playerSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                player.transform.position = new Vector3(targetX, 2.7f, 0);
            }
        }
        else
        {
            targetX = 2;
            if (player.transform.position.x > targetX)
            {
                player.transform.Translate(-playerSpeed * Time.deltaTime, 0, 0);
            }
            else
            {
                player.transform.position = new Vector3(targetX, 2.7f, 0);
            }
        }

        if (player.transform.position.x < 9.6f)
        {
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
        else
        {
            player.GetComponent<SpriteRenderer>().flipX = true;
        }
    }

    void MoveLaser()
    {
        if (!laserShot)
        {
            return;
        }
        if (bounce)
        {
            currentLaser.GetComponent<SpriteRenderer>().flipX = true;
        }
        else
        {
            currentLaser.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (currentLaserIsRed)
        {
            if (glowsticks[0].isRed)
            {
                currentLaser.transform.Translate(-playerSpeed * 0.5f * Time.deltaTime, 0, 0);
                if (currentLaser.transform.position.x < 9.6f)
                {
                    Destroy(currentLaser);
                    laserShot = false;
                }
            }
            else
            {
                if (!bounce)
                {
                    currentLaser.transform.Translate(-playerSpeed * 0.5f * Time.deltaTime, 0, 0);
                }
                else
                {
                    currentLaser.transform.Translate(playerSpeed * 0.5f * Time.deltaTime, 0, 0);
                }
                if (currentLaser.transform.position.x < 12f)
                {
                    bounce = true;
                }
                if (currentLaser.transform.position.x > player.transform.position.x && bounce)
                {
                    Destroy(currentLaser);
                    laserShot = false;
                    bounce = false;
                    failed = true;
                    audioSource.PlayOneShot(audioClips[2]);
                    laserHit = 0.5f;
                    player.GetComponent<SpriteRenderer>().sprite = playerSprites[1];
                }
            }
        }
        else
        {
            if (!glowsticks[0].isRed)
            {
                currentLaser.transform.Translate(playerSpeed * 0.5f * Time.deltaTime, 0, 0);
                if (currentLaser.transform.position.x > 9.6f)
                {
                    Destroy(currentLaser);
                    laserShot = false;
                }
            }
            else
            {
                if (!bounce)
                {
                    currentLaser.transform.Translate(playerSpeed * 0.5f * Time.deltaTime, 0, 0);
                }
                else
                {
                    currentLaser.transform.Translate(-playerSpeed * 0.5f * Time.deltaTime, 0, 0);
                }
                if (currentLaser.transform.position.x > 7)
                {
                    bounce = true;
                }
                if (currentLaser.transform.position.x < player.transform.position.x && bounce)
                {
                    Destroy(currentLaser);
                    laserShot = false;
                    bounce = false;
                    laserHit = 0.5f;
                    failed = true;
                    audioSource.PlayOneShot(audioClips[2]);
                    player.GetComponent<SpriteRenderer>().sprite = playerSprites[1];
                }
            }
        }
    }

}
