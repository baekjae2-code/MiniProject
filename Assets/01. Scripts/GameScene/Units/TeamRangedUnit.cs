using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TeamRangedUnit : Unit
{
    public GameObject attackObj;
    void Start()
    {
        moveSpeed = 2f;
        rb = GetComponent<Rigidbody2D>();
        attackObj = transform.Find("AttackEffect").gameObject;
        range = 4f;
        attackSpeed = 2f;
        attackCooltime = attackSpeed;
    }

    // Update is called once per frame
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
        int layer = LayerMask.NameToLayer("Enemy");
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
        Vector2 direction = (target.position - transform.position).normalized;
        GameObject obj = Instantiate(attackObj, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().linearVelocity = direction * 20f;
        attackCooltime = 0;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

}
