using System.Collections;
using UnityEngine;

public class EnemyKingMeleeUnit : EnemyMeleeUnit
{
    UnitData unitData;

    void Start()
    {
        unitData = GameManager.instance.unitsData.list[3];

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

        rb = GetComponent<Rigidbody2D>();
        attackObj = transform.Find("AttackEffect").gameObject;
        attackCooltime = attackSpeed;
    }
}
