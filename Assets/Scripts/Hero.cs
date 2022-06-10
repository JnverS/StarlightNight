using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private int health;
    [SerializeField] private Transform respawnPoint;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource damageSound;
    [SerializeField] private AudioSource missattackSound;
    [SerializeField] private AudioSource attackMobSound;
    private bool isGrounded = false;

    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite aliveHeart;
    [SerializeField] private Sprite deadHeart;

    public bool isAttacking = false;
    public bool isRecharged = true;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;
    public Joystick joystick;

    private LevelControl levelController;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    public static Hero Instance { get; set; }
    public int Health { get=>health; }

    private States State
    {
        get { return (States)anim.GetInteger("state"); }
        set { anim.SetInteger("state", (int)value); }
    }

    private void Awake()
    {
        Instance = this;
        lives = 5;
        if (LevelControl.CurrentScene != 2 && PlayerPrefs.GetString("retry") == "false")
        {
            health = PlayerPrefs.GetInt("lives");
            Debug.Log(health);
        }
        else
            health = lives;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        levelController = FindObjectOfType<LevelControl>();
        isRecharged = true;
    }

    private void FixedUpdate()
    {
        CheckGround();
    }

    private void Update()
    {
        if (isGrounded && !isAttacking) State = States.idle;

        if (!isAttacking && joystick.Horizontal != 0)
            Run();
        if (!isAttacking && isGrounded && joystick.Vertical > 0.5f)
            Jump();

        if (health > lives)
            health = lives;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < health)
                hearts[i].sprite = aliveHeart;
            else
                hearts[i].sprite = deadHeart;
            if (i < lives)
                hearts[i].enabled = true;
            else
                hearts[i].enabled = false;
        }
    }

    private void Run()
    {
        if (isGrounded) State = States.run;

        Vector3 dir = transform.right * joystick.Horizontal;

        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);

        sprite.flipX = dir.x < 0.0f;
    }

    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        jumpSound.Play();
    }

    public void Attack()
    {
        if (isRecharged)
        {
            State = States.attack;
            isAttacking = true;
            isRecharged = false;

            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
    }

    private void OnAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        if (colliders.Length == 0)
            missattackSound.Play();
        else
            attackMobSound.Play();

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();

            StartCoroutine(EnemyOnAttack(colliders[i]));
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
        if (!isGrounded && !isAttacking) State = States.jump;
    }

    public void Punch(Vector3 direction, float power)
    {
        rb.AddForce(direction * power);
    }

    public override void GetDamage()
    {
        health -= 1;
        damageSound.Play();
        if (health == 0)
        {
            foreach (var h in hearts)
                h.sprite = deadHeart;
            var finish = FindObjectOfType<Finish>();
            finish.FailUI();
        }

    }

    public void Respawn()
    {
        GetDamage();
        transform.position = respawnPoint.transform.position;
        Physics.SyncTransforms();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Finish>(out var finish))
        {
            finish.EndLevelUI();
        }
    }
    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(0.4f);
        isAttacking = false;
    }

    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        isRecharged = true;
    }
    private IEnumerator EnemyOnAttack(Collider2D enemy)
    {
        yield return new WaitForSeconds(0.2f);
        if (enemy != null)
        {
            SpriteRenderer enemyColor = enemy.GetComponentInChildren<SpriteRenderer>();
            enemyColor.color = new Color(0.972f, 0.078f, 0.078f);
            yield return new WaitForSeconds(0.2f);
            enemyColor.color = new Color(1, 1, 1);
        }
    }
}

public enum States
{
    idle,
    run,
    jump,
    attack
}
