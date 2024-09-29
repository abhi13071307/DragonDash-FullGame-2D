using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth {  get; private set; }
    private Animator animator;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float invulnerabilityDuration;
    [SerializeField] private int numberOffFlashed;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private AudioClip deathSound;
    

    public void Awake()
    { 
        animator = GetComponent<Animator>();
        currentHealth = startingHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float takeDamage)
    {
        currentHealth = Mathf.Clamp(currentHealth - takeDamage, 0, startingHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("hurt");
            StartCoroutine(Invunerability());
        }
        else
        {
            if (!dead)
            {
                
                if(GetComponent<PlayerMovement>() != null) 
                    GetComponent<PlayerMovement>().enabled = false;

                if(GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;
                
                if(GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;

                animator.SetBool("grounded", true);
                animator.SetTrigger("die");

                dead = true;
                SoundMananger.instance.PlaySound(deathSound);
                
            }
        }
    }
    public void AddHealth(float addHealth)
    {
        currentHealth = Mathf.Clamp(currentHealth + addHealth, 0, startingHealth);
    }
    
    private IEnumerator Invunerability()
    {
        Physics2D.IgnoreLayerCollision(7, 8, true);
        for (int i = 0; i < numberOffFlashed; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOffFlashed * 2));
            spriteRenderer.color = Color.white;
            yield return new WaitForSeconds(invulnerabilityDuration / (numberOffFlashed * 2));
        }
        Physics2D.IgnoreLayerCollision(7, 8, false);
    }

    public void Respawn()
    {
        dead = false;
        AddHealth(startingHealth);
        animator.ResetTrigger("die");
        animator.Play("Idle");
        StartCoroutine(Invunerability());

        if (GetComponent<PlayerMovement>() != null)
            GetComponent<PlayerMovement>().enabled = true;

        if (GetComponentInParent<EnemyPatrol>() != null)
            GetComponentInParent<EnemyPatrol>().enabled = true;

        if (GetComponent<MeleeEnemy>() != null)
            GetComponent<MeleeEnemy>().enabled = true;
    }
    
}
