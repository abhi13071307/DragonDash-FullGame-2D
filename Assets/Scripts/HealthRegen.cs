using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRegen : MonoBehaviour
{
    [SerializeField] private float healthValue;

    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if(collision2D.tag == "Player")
        {
            collision2D.GetComponent<Health>().AddHealth(healthValue);
            gameObject.SetActive(false);
        }
    }
}
