
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float bulletSpeed = 10f;
    public float damage = 20f;
    public float lifetime = 3f;

    private Rigidbody2D rb;
    private float direction = 1f;
    private bool hasHit = false;

    public void SetDirection(float newDirection)
    {
        direction = newDirection >= 0f ? 1f : -1f;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogError("Bullet needs a Rigidbody2D!", this);
            Destroy(gameObject);
            return;
        }

        rb.linearVelocity = new Vector2(
            direction * bulletSpeed,
            0f
        );

        // Destroy bullet if it doesn't hit anything
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasHit) return;

        if (collision.gameObject.CompareTag("Enemy"))
        {
            hasHit = true;

            Skeleton enemy =
                collision.gameObject.GetComponent<Skeleton>();

            // Damage the enemy
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Debug.Log("Bullet hit enemy! Damage: " + damage);
            }

            // Destroy bullet after hitting enemy
            Destroy(gameObject);
        }
    }
}
