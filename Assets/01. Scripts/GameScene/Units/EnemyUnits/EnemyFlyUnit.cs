using UnityEngine;

public class EnemyFlyUnit : Unit
{
    UnitData unitData;

    protected override void Awake()
    {
        base.Awake();

        unitData = GameManager.instance.unitsData.list[7];

        myName = unitData.myName;
        level = GameManager.instance.NowStage * 3;        //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;

        range = unitData.range;
        moveSpeed = -unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        attackCooltime = attackSpeed;

    }
    protected override void OnEnable()
    {
        level = GameManager.instance.NowStage * 3;        //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        for (int i = 0; i < level; i++)
        {
            maxHP += (maxHP / 10f);
            nowHP += (nowHP / 10f);
            damage += (damage / 10f);
        }
        base.OnEnable();
    }
    protected override void Attack()
    {
        if (target == null)
            return;

        SoundManager.instance.PlaySFX((SFXType)1);

        Vector2 direction = (target.position - transform.position).normalized;
        GameObject obj = ObjectPoolManager.instance.GetObject(attackObj[0].name);
        obj.transform.position = transform.position + Vector3.up * 0.5f;
        obj.GetComponent<EmenyRangedAttack>().damage = damage;
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().linearVelocity = direction * 15;

        base.Attack();
    }

    private void FixedUpdate()
    {
        if (isDie == false && state != State.Stun)
        {
            Fly();

            if (rb.linearVelocityX > -moveSpeed)        //x < 2
                rb.linearVelocityX += moveSpeed / 5f;   //x -2/5
            if (rb.linearVelocityX < moveSpeed)
                rb.linearVelocityX -= moveSpeed / 5f;
        }
    }
    protected override void AttackState()
    {
        rb.linearVelocityX = moveSpeed / 50f;

        base.AttackState();
    }
    protected override void Move()
    {
        if (rb.linearVelocityX > moveSpeed) //x > -2, x -2/30 
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
        if (transform.position.y > 2f)
        {
            rb.linearVelocityY = -1;
        }
    }
}
