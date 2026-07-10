using System.Linq;
using UnityEngine;

public class EnemyMagicUnit : Unit
{
    public GameObject attackObj;
    void Start()
    {
        moveSpeed = 2f;
        rb = GetComponent<Rigidbody2D>();
        attackObj = transform.Find("EnemyMagicEffect").gameObject;
        range = 6f;
        attackSpeed = 8f;
        attackCooltime = attackSpeed;
    }

    void FixedUpdate()
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
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);
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
        GameObject obj = Instantiate(attackObj, target.position + Vector3.up * 5f, Quaternion.identity);
        obj.SetActive(true);
        attackCooltime = 0;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }
}
