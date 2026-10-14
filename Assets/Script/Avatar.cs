
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public float healthRestore = 20f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement2 player = collision.GetComponent<PlayerMovement2>();

            if (player != null)
            {
                // Restore 20 health, without exceeding max health
                player.health = Mathf.Min(
                    player.health + healthRestore,
                    player.maxHealth
                );

                // Update the health bar
                if (player.healthBar != null)
                {
                    player.healthBar.value = player.health;
                }

                // Update collected coins
                gameManager.instance.coinCollected--;

                // Destroy the avatar after collection
                Destroy(gameObject);
            }
        }
    }
}
