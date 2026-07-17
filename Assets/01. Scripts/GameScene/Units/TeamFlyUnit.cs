using System.Linq;
using UnityEngine;

public class TeamFlyUnit : Unit
{
    GameObject attackObj;
    UnitData unitData;

    void Start()
    {
        unitData = GameManager.instance.printData[7];

        myName = unitData.myName;
        level = unitData.level;
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
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
            rb.linearVelocityX = moveSpeed / 50f;
        }
        Fly();

        {   //움직임 제어 ( 맞을때 빠르게 날아감 )
            if (rb.linearVelocityX > moveSpeed)
                rb.linearVelocityX -= moveSpeed / 5f;
            if (rb.linearVelocityX < -moveSpeed)
                rb.linearVelocityX += moveSpeed / 5f;
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
        obj.GetComponent<TeamRangedAttack>().damage = damage;
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().linearVelocity = direction * 10;
        attackCooltime = 0;
    }
    public void Move()
    {
        if (rb.linearVelocityX < moveSpeed)
            rb.linearVelocityX += moveSpeed / 30f;
    }
    void Fly()
    {
        if (transform.position.y < 1.5f)
        {
            rb.linearVelocityY = 2f;
        }
        if (transform.position.y < 2f)
        {
            rb.linearVelocityY += 0.1f;
        }
        if (transform.position.y > 2f)  //높이 제한
        {
            rb.linearVelocityY = -1;
        }
    }
}
