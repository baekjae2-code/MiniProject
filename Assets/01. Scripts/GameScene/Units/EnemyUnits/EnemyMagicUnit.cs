using System.Linq;
using UnityEngine;

public class EnemyMagicUnit : Unit
{
    UnitData unitData;
    protected override void Awake()
    {
        base.Awake();

        unitData = GameManager.instance.unitsData.list[5];

        myName = unitData.myName;
        level = GameManager.instance.NowStage * 3;         //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;

        for (int i = 0; i < level; i++)
        {
            maxHP += (maxHP / 10f);
            nowHP += (nowHP / 10f);
            damage += (damage / 10f);
        }

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

        SoundManager.instance.PlaySFX((SFXType)2);

        GameObject u = ObjectPoolManager.instance.GetObject(attackObj[0].name);
        u.transform.position = target.position + Vector3.up * 5f + Vector3.left * 1f;
        u.GetComponent<MagicEffect>().damage = damage;
        ObjectPoolManager.instance.ReturnObject(attackObj[0].name, u, 5);

        attackCooltime = 0;
        target = null;
    }
}
