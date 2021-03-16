using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    [SerializeField]
    AudioClip[] keys;
    [SerializeField]
    AudioSource player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                player.PlayOneShot(keys[0]);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.PlayOneShot(keys[1]);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                player.PlayOneShot(keys[2]);
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                player.PlayOneShot(keys[3]);
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                player.PlayOneShot(keys[4]);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                player.PlayOneShot(keys[5]);
            }
            if (Input.GetKeyDown(KeyCode.Y))
            {
                player.PlayOneShot(keys[6]);
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                player.PlayOneShot(keys[7]);
            }
            if (Input.GetKeyDown(KeyCode.U))
            {
                player.PlayOneShot(keys[8]);
            }
            if (Input.GetKeyDown(KeyCode.J))
            {
                player.PlayOneShot(keys[9]);
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                player.PlayOneShot(keys[10]);
            }
            if (Input.GetKeyDown(KeyCode.K))
            {
                player.PlayOneShot(keys[11]);
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                player.PlayOneShot(keys[12]);
            }
            if (Input.GetKeyDown(KeyCode.P))
            {
                player.PlayOneShot(keys[13]);
            }
            if (Input.GetKeyDown(KeyCode.Semicolon))
            {
                player.PlayOneShot(keys[14]);
            }
            if (Input.GetKeyDown(KeyCode.LeftBracket))
            {
                player.PlayOneShot(keys[15]);
            }
            if (Input.GetKeyDown(KeyCode.Quote))
            {
                player.PlayOneShot(keys[16]);
            }
        }

    }
}
