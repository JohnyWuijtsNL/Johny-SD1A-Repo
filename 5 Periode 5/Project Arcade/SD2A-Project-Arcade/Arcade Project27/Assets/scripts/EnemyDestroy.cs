using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D colission)
    {
        if (colission.gameObject.name == "EnemyHit")
        {

            Destroy(colission.gameObject);
        }
    }
}
