using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyRangedUnit : Unit
{
    GameObject attackObj;
    UnitData unitData;

    void Start()
    {
        unitData = GameManager.instance.printData[1];

        myName = unitData.myName;
        level = unitData.level + GameManager.instance.NowStage;        //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData.maxHP + GameManager.instance.NowStage * (unitData.maxHP / 10f);
        nowHP = unitData.maxHP + GameManager.instance.NowStage * (unitData.maxHP / 10f);
        damage = unitData.damage + GameManager.instance.NowStage * (unitData.damage / 10f);
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        attackObj = transform.Find("AttackEffect").gameObject;
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
        Vector2 direction = (target.position - transform.position).normalized;
        GameObject obj = Instantiate(attackObj, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().linearVelocity = direction * 20f;
        attackCooltime = 0;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }
}
