using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    int buttonNumber;

    [SerializeField]
    AudioSource audioSource;

    [SerializeField]
    AudioClip buttonPress;

    bool isPressed = false;

    [SerializeField]
    GameObject[] otherButtons;

    private void Start()
    {
        if (buttonNumber == 0)
        {
            foreach (GameObject otherButton in otherButtons)
            {
                otherButton.SetActive(false);
            }
        }
    }

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
                audioSource.PlayOneShot(buttonPress);
                if (buttonNumber == 0)
                {
                    foreach (GameObject otherButton in otherButtons)
                    {
                        otherButton.SetActive(true);
                    }
                }
            }
        }
    }
}
