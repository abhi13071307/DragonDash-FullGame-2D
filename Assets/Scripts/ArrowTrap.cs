using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireballs;

    private float cooldownTimer;

    private void Attack()
    {
        cooldownTimer = 0;
        fireballs[FindFireballs()].transform.position = firepoint.position;
        fireballs[FindFireballs()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireballs()
    {
        for(int i = 0; i <fireballs.Length; i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if (cooldownTimer >= attackCooldown)
        {
            Attack();
        }
    }
}
