using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeMovement : MonoBehaviour
{
    private Transform player;
    private float speed;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isDying = false; 
    public bool isDead = false;

    public void Initialize(Transform playerTransform, float swarmSpeed)
    {
        player = playerTransform;
        speed = swarmSpeed;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        SetAnimationDirection(); 
    }

    void Update()
    {
        if (isDying || isDead) return;

        if (player != null)
        {
            Vector2 targetPosition = player.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            SetAnimationDirection();
        }
    }

    private void SetAnimationDirection()
    {
        if (player == null) return;

        Vector2 direction = ((Vector2)player.position - (Vector2)transform.position).normalized;
        
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                animator.Play("bee_side"); 
                spriteRenderer.flipX = true;
            }
            else
            {
                animator.Play("bee_side"); 
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            spriteRenderer.flipX = false;
            if (direction.y > 0)
            {
                animator.Play("bee_up"); 
            }
            else
            {
                animator.Play("bee_down"); 
            }
        }
    }

    public void TriggerDeath()
    {
        if (isDying) return; 
        isDying = true;
        isDead = true;
        animator.Play("bee_death"); 
        ScoreTracker.instance.AddScore(1);
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            TriggerDeath();
            Destroy(other.gameObject); 
        }
    }
}