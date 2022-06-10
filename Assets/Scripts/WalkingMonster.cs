using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingMonster : Entity
{
    private Vector3 dir;
    private SpriteRenderer sprite;
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        dir = transform.right;
        lives = 3;
    }

    private void Update()
    {
        Move();
    }
    private void Move()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position + transform.right * dir.x *0.5f, 0.5f);

        if (colliders.Length > 1)
        {
            var interactWithHero = false;
            Hero hero = null;
            foreach (var item in colliders)
            {
                if (item.gameObject.TryGetComponent<Hero>(out hero))
                {
                    interactWithHero = true;
                    break;
                }
            }

            if (interactWithHero)
            {
                Vector3 dir = hero.transform.position - transform.position;
                dir = dir.normalized;
                hero?.GetDamage();
                hero?.Punch(dir, 500);
            }
            dir *= -1f;
            sprite.flipX = dir.x < 0.0f;

        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);

    }
}
