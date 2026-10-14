
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    public float moveSpeed = 0.5f;
    public float health = 100f;
    public float damage = 10f;

    private int direction = -1;
    private Rigidbody2D rb;
    private Animator anim;

    private bool isAttacking = false;
    private bool isDead = false;

    private LanceMovement player;
    private PlayerMovement2 player1;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        if (rb == null)
            Debug.LogError("Skeleton: Rigidbody2D is missing!", this);

        if (anim == null)
            Debug.LogError("Skeleton: Animator is missing!", this);
    }

    private void FixedUpdate()
    {
        if (isDead || rb == null)
            return;

        if (isAttacking)
        {
            rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(
                moveSpeed * direction,
                rb.linearVelocity.y
            );
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDead)
            return;

        if (!collision.gameObject.CompareTag("Player"))
            return;

        player = collision.gameObject.GetComponent<LanceMovement>();
        player1 = collision.gameObject.GetComponent<PlayerMovement2>();

        if (player != null || player1 != null)
            Attack();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
            return;

        StopAttack();
        player = null;
        player1 = null;
    }

    void Attack()
    {
        if (isDead || anim == null)
            return;

        isAttacking = true;

        anim.SetBool("IsRunning", false);
        anim.SetBool("IsAttacking", true);
    }

    // Call this using an Animation Event during the attack animation.
    public void DealDamage()
    {
        if (isDead)
            return;

        if (player != null)
            player.TakeDamage(damage);
        else if (player1 != null)
            player1.TakeDamage(damage);
    }

    void StopAttack()
    {
        if (isDead)
            return;

        isAttacking = false;

        if (anim != null)
        {
            anim.SetBool("IsAttacking", false);
            anim.SetBool("IsRunning", true);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (isDead)
            return;

        health -= damageAmount;
        Debug.Log("Skeleton Health: " + health);

        if (health <= 0f)
            Die();
    }

    void Die()
    {
        isDead = true;
        isAttacking = false;

        // Stop the skeleton immediately.
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.angularVelocity = 0f;
            rb.simulated = false;
        }

        // Stop any further attack damage.
        player = null;
        player1 = null;

        if (anim != null)
        {
            anim.SetBool("IsRunning", false);
            anim.SetBool("IsAttacking", false);

            // This must match the parameter in your Animator.
            anim.SetBool("IsDying", true);
        }

        // Fallback in case the death animation does not destroy the object.
        Destroy(gameObject, 3f);
    }

    // Optional: Add this as an Animation Event on the final frame
    // of the death animation to destroy the skeleton at the exact time.
    public void FinishDeath()
    {
        Destroy(gameObject);
    }
}
