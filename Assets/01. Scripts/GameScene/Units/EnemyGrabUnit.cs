using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyGrabUnit : Unit
{
    bool isGrab;
    void Start()
    {
        moveSpeed = 2f;
        rb = GetComponent<Rigidbody2D>();
        attackSpeed = 2f;
        attackCooltime = attackSpeed;
    }

    private void FixedUpdate()
    {
        if (isStun == true)
            return;

        attackCooltime += Time.deltaTime;

        if (!isGrab)
        {
            CheckEnemy();
        }
        if (target != null && attackCooltime > attackSpeed)
        {
            Attack();
        }
        if (target != null && isGrab)
        {
            target.position = transform.position + new Vector3(-0.5f, 0.5f);
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
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 1, targetLayer);
        if (collider.Length == 0)
        {
            canMove = true;
        }
        else
        {
            
            target = collider.OrderBy(col => Vector2.Distance(transform.position, col.transform.position)).FirstOrDefault().transform;
            if (target.transform.parent != null)
                target = null;

            canMove = false;
        }
    }
    void Attack()
    {
        rb.linearVelocity = new Vector2(0f, 5f);
        StartCoroutine(Throw());
        attackCooltime = 0;
    }
    IEnumerator Throw()
    {
        Transform throwTarget = target;
        isGrab = true;
        throwTarget.SetParent(transform);
        //target.localPosition = Vector3.zero;
        target.GetComponent<Unit>().Stun(2f);
        yield return new WaitForSeconds(0.5f);
        isGrab = false;
        throwTarget.SetParent(null);
        throwTarget.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(-10f, 5f);
        target = null;
    }

    public void Move()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }

}
