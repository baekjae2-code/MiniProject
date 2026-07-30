using UnityEngine;

public class TeamMeleeUnit : Unit
{
    UnitData unitData;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        unitData = GameManager.instance.printData[0];

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
    //private void FixedUpdate()
    //{
    //    if (isStun == true)
    //        return;
    //    if (isDie == true)
    //        return;

    //    attackCooltime += Time.deltaTime;

    //    if (target != null && attackCooltime > attackSpeed)
    //    {
    //        Attack();
    //    }
    //    if (canMove)
    //    {
    //        Move();
    //    }
    //}
    protected override void Attack()
    {
        if (target == null)
            return;

        SoundManager.instance.PlaySFX((SFXType)0);

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * Random.Range(3f, 4f) + new Vector2(0f, 3f);
        attackObj[0].GetComponent<TeamMeleeAttack>().damage = damage;
        attackObj[0].SetActive(true);

        base.Attack();
    }

}
