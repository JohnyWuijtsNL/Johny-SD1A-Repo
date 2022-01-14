using UnityEngine;

public class UIPlayer : MonoBehaviour
{
    [SerializeField]
    AudioClip hover;
    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnHover()
    {
        audioSource.PlayOneShot(hover);
    }
}
