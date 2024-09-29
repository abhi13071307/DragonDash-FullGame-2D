using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;
    private Animator animator;
    private PlayerMovement playerMovement;
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private AudioClip fireballSound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if(Input.GetMouseButton(0) && cooldownTimer > attackCoolDown && playerMovement.CanAttack())
        {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        SoundMananger.instance.PlaySound(fireballSound);
        animator.SetTrigger("attack");
        cooldownTimer = 0;

        fireballs[FindFireball()].transform.position = firepoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for(int i = 0; i < fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
