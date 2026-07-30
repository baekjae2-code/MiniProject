using UnityEngine;

public class TeamRangedUnit : Unit
{
    UnitData unitData;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        unitData = GameManager.instance.printData[1];

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
    //void FixedUpdate()
    //{
    //    if (isStun == true)
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

        SoundManager.instance.PlaySFX((SFXType)4);

        Vector2 direction = (target.position - transform.position).normalized; 
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.Euler(0, 0, angle);
        //GameObject obj = Instantiate(attackObj, transform.position + Vector3.up * 0.5f, rotation);

        GameObject obj = ObjectPoolManager.instance.GetObject(attackObj[0].name);
        obj.transform.position = transform.position + Vector3.up * 0.5f;
        obj.GetComponent<TeamRangedAttack>().damage = damage;
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().linearVelocity = direction * 15 + Vector2.up * 2f;

        base.Attack();
    }

}
