using System.Linq;
using UnityEngine;

public class TeamHealUnit : Unit
{
    UnitData unitData;

    protected override void Awake()
    {
        base.Awake();

        unitData = GameManager.instance.printData[4];

        myName = unitData.myName;
        level = unitData.level;
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

    }
    protected override void Attack()
    {
        if (target == null)
            return;

        SoundManager.instance.PlaySFX((SFXType)3);

        GameObject u = ObjectPoolManager.instance.GetObject(attackObj[0].name);
        u.transform.position = transform.position;
        ObjectPoolManager.instance.ReturnObject(attackObj[0].name, u, 1);

        base.Attack();
    }

}
