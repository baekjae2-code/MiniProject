using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMeleeUnit : Unit
{
    public GameObject attackObj;
    void Start()
    {
        moveSpeed = 3f;
        rb = GetComponent<Rigidbody2D>();
        attackObj = transform.Find("AttackEffect").gameObject;
        attackSpeed = 2f;
        attackCooltime = attackSpeed;
    }

    private void FixedUpdate()
    {
        if (isStun == true)
            return;

        attackCooltime += Time.deltaTime;

        CheckEnemy();
        if (target != null && attackCooltime > attackSpeed)
        {
            Attack();
        }
        if (canMove)
        {
            Move();
        }
    }
    void CheckEnemy()
    {
        target = null;
        int layer = LayerMask.NameToLayer("Player");
        int targetLayer = 1 << layer;
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 3, targetLayer);
        if (collider.Length == 0)
        {
            canMove = true;
        }
        else
        {
            target = collider.OrderBy(col => Vector2.Distance(transform.position, col.transform.position)).FirstOrDefault().transform;

            canMove = false;
        }
    }
    void Attack()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * Random.Range(3f, 4f) + new Vector2(0f, 3f);
        attackObj.SetActive(true);
        attackCooltime = 0;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }


}
