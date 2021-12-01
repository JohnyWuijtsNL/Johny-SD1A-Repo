using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    Animator animator;
    Vector3 oldPosition = Vector3.zero;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(oldPosition.x - transform.position.x) < 0.001 && Mathf.Abs(oldPosition.y - transform.position.y) < 0.001)
        {
            animator.SetBool("isWalking", false);
        }
        else
        {
            animator.SetBool("isWalking", true);
        }
        oldPosition = transform.position;
    }
}
