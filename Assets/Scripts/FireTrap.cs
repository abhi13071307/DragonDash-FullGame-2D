using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private float damage;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool triggered;
    private bool active;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.tag == "Player") 
        {
            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }
            if (active) 
            {   
                collider2D.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private IEnumerator ActivateFireTrap()
    {
        triggered = true;
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(activationDelay);
        spriteRenderer.color = Color.white;
        active = true;

        animator.SetBool("activated", true);
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        animator.SetBool("activated", false);
    }
}
