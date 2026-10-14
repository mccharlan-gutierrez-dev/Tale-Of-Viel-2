using UnityEngine;
using UnityEngine.UI;
using UnityEngine.PlayerLoop;

public class LanceMovement : MonoBehaviour
{
    public float moveSpeed;
    public float health = 100f;
    public float maxHealth = 100f;
    public Slider healthBar;

    Rigidbody2D rb;
    Animator anim;

    float horizontal, vertical;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public float attackDamage = 20f;

    public RuntimeAnimatorController deathController;
    bool isDead = false;

    public GameObject gameOverPanel;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        health = maxHealth;

        healthBar.maxValue = maxHealth;
        healthBar.value = health;

    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        if (horizontal < 0)
            transform.localScale = new Vector2(-1, 1);
        else if (horizontal > 0)
            transform.localScale = new Vector2(1, 1);

        if (horizontal != 0 || vertical != 0)
            anim.SetBool("IsRuinning", true);
        else
            anim.SetBool("IsRuinning", false);

        if (Input.GetMouseButtonDown(1))
        {
            Attack1();
        }

        //if (Input.GetMouseButtonDown(1))
        //{
        //    Attack2();
        //}

        //if (Input.GetKey(KeyCode.Space))
        //{
        //    anim.SetBool("IsGuarding", true);
        //}
        //else
        //{
        //    anim.SetBool("IsGuarding", false);
        //}
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = horizontal * moveSpeed;
        rb.linearVelocityY = vertical * moveSpeed;
    }

    void Attack1()
    {
        anim.SetTrigger("Attack1");
        Attack();
    }

    void Attack2()
    {
        anim.SetTrigger("Attack2");
        Attack();
    }

    void Attack()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(
            attackPoint.position,
            attackRange
        );

        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Skeleton skeleton = enemy.GetComponent<Skeleton>();

                if (skeleton != null)
                {
                    //this is from global variable which is naka public
                    skeleton.TakeDamage(attackDamage);

                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(
            attackPoint.position,
            attackRange
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Water"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        health -= damage;
        health = Mathf.Clamp(health, 0, maxHealth);

        healthBar.value = health;

        Debug.Log("Player Health: " + health);

        if (health <= 0)
        {
            isDead = true;

            rb.linearVelocity = Vector2.zero;

            anim.runtimeAnimatorController = deathController;
            anim.SetBool("IsRuinning", false);

            Destroy(gameObject, 1f);
            gameOverPanel.SetActive(true);
        }
    }
}
