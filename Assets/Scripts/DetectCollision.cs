using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class DetectCollision : MonoBehaviour
{
    private void Update()
    {
        /*
        if ((transform.position.x < -20) || (transform.position.x > 20)
                                         || (transform.position.y < -20) || (transform.position.y > 20)
                                         || (transform.position.z < -100) || (transform.position.z > 100))
        {
            gameObject.SetActive(false);
        }
        */
    }
   private void OnTriggerEnter(Collider collision)
   {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(50);
            gameObject.SetActive(false);
        }
   }
}
 
