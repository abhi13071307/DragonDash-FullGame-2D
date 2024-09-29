using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private float direction;
    private bool hit;
    private float lifeTime;

    private Animator animator;
    private BoxCollider2D boxCollider2D;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit)
        {
            return;
        }
        float movementSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if (lifeTime > 5) { 
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider2D.enabled = false;
        animator.SetTrigger("explode");

        if (collision.tag == "Enemy")
            collision.GetComponent<Health>().TakeDamage(1);
    }

    public void SetDirection(float setDirection)
    {
        lifeTime = 0;
        direction = setDirection;
        gameObject.SetActive(true);
        hit = false;
        boxCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != setDirection)
            localScaleX = -localScaleX;

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }
    public void DeActivate()
    {
        gameObject.SetActive(false);
    }
}
