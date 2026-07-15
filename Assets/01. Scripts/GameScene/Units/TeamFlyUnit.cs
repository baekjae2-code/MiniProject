using System.Linq;
using UnityEngine;

public class TeamFlyUnit : Unit
{
    GameObject attackObj;
    UnitData unitData;

    void Start()
    {
        unitData = GameManager.instance.printData[3];

        myName = unitData.myName;
        level = unitData.level;
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        range = 5;
        moveSpeed = 1;
        attackSpeed = unitData.attackSpeed;

        attackObj = transform.Find("TeamFlyMagicEffect").gameObject;
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
        else
        {
            rb.linearVelocityX = 0;
        }
        Fly();
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
        GameObject obj = Instantiate(attackObj, transform.position, Quaternion.identity);
        Vector2 direction = (target.position - transform.position).normalized;
        obj.SetActive(true);
        attackCooltime = 0;
        target = null;
    }
    public void Move()
    {
        rb.linearVelocityX = moveSpeed;
    }
    void Fly()
    {
        if (transform.position.y < 1.5f)
        {
            rb.linearVelocityY = 2f;
        }
        else
        {
            rb.linearVelocityY += 0.2f;
        }
    }
}
