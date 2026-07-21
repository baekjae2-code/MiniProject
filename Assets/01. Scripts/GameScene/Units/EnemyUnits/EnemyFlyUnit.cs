using System.Linq;
using UnityEngine;

public class EnemyFlyUnit : Unit
{
    GameObject attackObj;
    UnitData unitData;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        unitData = GameManager.instance.unitsData.list[7];

        myName = unitData.myName;
        level = unitData.level + GameManager.instance.NowStage;        //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;

        for (int i = 0; i < GameManager.instance.NowStage; i++)
        {
            maxHP += (maxHP / 10f);
            nowHP += (nowHP / 10f);
            damage += (damage / 10f);
        }

        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        attackObj = transform.Find("EnemyFlyMagicEffect").gameObject;
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

        {
            if (rb.linearVelocityX < -moveSpeed)
                rb.linearVelocityX += moveSpeed / 5f;
            if (rb.linearVelocityX > moveSpeed)
                rb.linearVelocityX -= moveSpeed / 5f;
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
        obj.GetComponent<EmenyRangedAttack>().damage = damage;
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().linearVelocity = direction * 10;
        attackCooltime = 0;
    }
    public void Move()
    {
        if (rb.linearVelocityX > -moveSpeed)
            rb.linearVelocityX -= moveSpeed / 30f;
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
        if (transform.position.y > 2f)
        {
            rb.linearVelocityY = -1;
        }
    }
}
