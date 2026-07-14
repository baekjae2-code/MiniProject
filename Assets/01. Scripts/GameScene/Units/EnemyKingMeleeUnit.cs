using System.Collections;
using UnityEngine;

public class EnemyKingMeleeUnit : EnemyMeleeUnit
{
    UnitData unitData;

    void Start()
    {
        unitData = GameManager.instance.printData[3];

        myName = unitData.myName;
        level = unitData.level + GameManager.instance.NowStage;        //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData.maxHP + GameManager.instance.NowStage * (unitData.maxHP / 10f);
        nowHP = unitData.maxHP + GameManager.instance.NowStage * (unitData.maxHP / 10f);
        damage = unitData.damage + GameManager.instance.NowStage * (unitData.damage / 10f);
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        rb = GetComponent<Rigidbody2D>();
        attackObj = transform.Find("AttackEffect").gameObject;
        attackCooltime = attackSpeed;
    }
}
