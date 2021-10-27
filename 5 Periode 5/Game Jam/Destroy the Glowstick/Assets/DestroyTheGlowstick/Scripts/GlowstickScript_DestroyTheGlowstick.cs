using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowstickScript_DestroyTheGlowstick : MonoBehaviour
{
    [SerializeField]
    Sprite[] glowstickSprites;
    public bool isRed;
    public int glowstickNumber;
    float moveSpeed = 40;
    float targetHeight = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        isRed = Random.Range(0, 2) == 1;
        if (isRed)
        {
            GetComponent<SpriteRenderer>().sprite = glowstickSprites[0];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = glowstickSprites[1];
        }
        transform.position = new Vector2(9.6f, 2.5f + 4.5f * glowstickNumber);
        targetHeight = 2.5f + 4.5f * glowstickNumber;
    }

    // Update is called once per frame
    void Update()
    {
        if (targetHeight < transform.position.y)
        {
            transform.Translate(0, -moveSpeed * Time.deltaTime, 0);
        }
        else
        {
            transform.position = new Vector3(9.6f, targetHeight, 0);
        }
    }

    public void MoveOn()
    {
        targetHeight -= 4.5f;
    }
}
