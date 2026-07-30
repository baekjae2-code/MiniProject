using UnityEngine;

public class TeamFlyUnit : Unit
{
    UnitData unitData;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
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

        base.OnEnable();
    }
    void FixedUpdate()
    {
        if (isDie == false && state != State.Stun)
        {

            Fly();

            //움직임 제어 ( 맞을때 빠르게 날아감 )
            if (rb.linearVelocityX > moveSpeed)
                rb.linearVelocityX -= moveSpeed / 5f;
            if (rb.linearVelocityX < -moveSpeed)
                rb.linearVelocityX += moveSpeed / 5f;

        }
    }
    protected override void AttackState()
    {
        rb.linearVelocityX = moveSpeed / 50f;

        base.AttackState();
    }
    protected override void Attack()
    {
        if (target == null)
            return;

        SoundManager.instance.PlaySFX((SFXType)1);

        Vector2 direction = (target.position - transform.position).normalized;
        //GameObject obj = Instantiate(attackObj, transform.position + Vector3.up * 0.5f, Quaternion.identity);
        GameObject obj = ObjectPoolManager.instance.GetObject(attackObj[0].name);
        obj.transform.position = transform.position + Vector3.up * 0.5f;
        obj.GetComponent<TeamRangedAttack>().damage = damage;
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().linearVelocity = direction * 15;

        base.Attack();
    }
    protected override void Move()
    {
        if (rb.linearVelocityX < moveSpeed)
            rb.linearVelocityX += moveSpeed / 30f;

        if (target != null)
        {
            ChangeState(State.Attack);
        }
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
