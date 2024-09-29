using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collider2D)
    {
        if(collider2D.tag == "Player")
        {
            collider2D.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
