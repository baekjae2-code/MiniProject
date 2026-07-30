using System.Linq;
using UnityEngine;

public class TeamMagicUnit : Unit
{
    UnitData unitData;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        unitData = GameManager.instance.printData[5];

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
    protected override void Attack()
    {
        if (target == null)
            return;

        SoundManager.instance.PlaySFX((SFXType)2);

        GameObject u = ObjectPoolManager.instance.GetObject(attackObj[0].name);
        u.transform.position = target.position + Vector3.up * 5f + Vector3.right * 1f;
        u.GetComponent<MagicEffect>().damage = damage;
        ObjectPoolManager.instance.ReturnObject(attackObj[0].name, u, 5);

        base.Attack();
    }
}
