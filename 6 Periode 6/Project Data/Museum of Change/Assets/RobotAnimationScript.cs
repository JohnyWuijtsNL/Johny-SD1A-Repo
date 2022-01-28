using UnityEngine;

public class RobotAnimationScript : MonoBehaviour
{
    [SerializeField]
    GameObject propellor;
    [SerializeField]
    MeshRenderer face;
    [SerializeField]
    Material[] faces;
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip[] dialogues;
    [SerializeField]
    GameManager gameManager;
    int currentState = -1;
    int currentDialogue = 0;

    float rotateSpeed = 1080f;
    float talkSpeed = 0.08f;
    float emotionSpeed = 1.5f;
    int talkingMouth = 0;
    bool isEmoting = false;
    float timer;
    bool crash = false;

    int currentRoom = -1;
    int oldCurrentRoom = -1;

    void Update()
    {
        currentRoom = gameManager.currentRoom;
        if (currentRoom != oldCurrentRoom)
        {
            audioSource.Stop();
            oldCurrentRoom = currentRoom;
            switch (currentRoom)
            {
                case 0:
                    currentState = 0;
                    currentDialogue = 0;
                    break;
                case 1:
                    currentState = 5;
                    currentDialogue = 3;
                    break;
                case 2:
                    currentState = 7;
                    currentDialogue = 4;
                    break;
                case 3:
                    currentState = 17;
                    currentDialogue = 10;
                    break;
                case 4:
                    currentState = 23;
                    currentDialogue = 13;
                    break;
                case 5:
                    currentState = 31;
                    currentDialogue = 18;
                    break;
                case 6:
                    currentState = 33;
                    currentDialogue = 19;
                    break;
                case 7:
                    currentState = 41;
                    currentDialogue = 24;
                    break;
                case 8:
                    currentState = 43;
                    currentDialogue = 25;
                    break;
                default:
                    break;
            }
        }

        propellor.transform.Rotate(new Vector3(0, 0, rotateSpeed * Time.deltaTime));
        transform.position = new Vector3(transform.position.x, 1.5f + Mathf.Sin(Time.time * 4) * 0.05f, transform.position.z);
        if (audioSource.isPlaying)
        {
            Emote(0);
        }
        else
        {
            switch (currentState)
            {
                case -1:
                    break;
                case 1:
                    Emote(3);
                    if (timer <= 0)
                    {
                        isEmoting = false;
                        currentState++;
                    }
                    break;
                case 4: case 6: case 15: case 21: case 32: case 42:
                    face.material = faces[0];
                    break;
                case 9:
                    Emote(4);
                    if (timer <= 0)
                    {
                        isEmoting = false;
                        currentState++;
                    }
                    break;
                case 11:
                    Emote(5);
                    if (timer <= 0)
                    {
                        isEmoting = false;
                        currentState++;
                    }
                    break;
                case 18: case 34: case 38:
                    Emote(6);
                    if (timer <= 0)
                    {
                        isEmoting = false;
                        currentState++;
                    }
                    break;
                case 24: case 28:
                    Emote(7);
                    if (timer <= 0)
                    {
                        isEmoting = false;
                        currentState++;
                    }
                    break;
                case 30:
                    face.material = faces[7];
                    break;
                case 40:
                    Emote(8);
                    break;
                case 44:
                    face.material = faces[10];
                    break;
                default:
                    audioSource.PlayOneShot(dialogues[currentDialogue]);
                    currentDialogue++;
                    currentState++;
                    break;
            }
        }
        
        timer -= Time.deltaTime;
    }

    void Emote(int emotion)
    {
        switch (emotion)
        {
            case 0:
                if (timer <= 0)
                {
                    timer = talkSpeed;
                    int temp = Random.Range(0, 3);
                    while (temp == talkingMouth)
                    {
                        temp = Random.Range(0, 3);
                    }
                    face.material = faces[temp];
                    talkingMouth = temp;
                }
                break;
            case 8:
                if (timer <= 0)
                {
                    timer = Random.Range(0.1f, 0.5f);
                    if (crash)
                    {
                        face.material = faces[8];
                    }
                    else
                    {
                        face.material = faces[9];
                    }
                    crash = !crash;
                }
                break;
            default:
                if (!isEmoting)
                {
                    face.material = faces[emotion];
                    timer = emotionSpeed;
                    isEmoting = true;
                }
                break;
        }

    }
}
