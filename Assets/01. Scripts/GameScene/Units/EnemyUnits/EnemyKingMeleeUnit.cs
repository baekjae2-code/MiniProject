using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyKingMeleeUnit : EnemyMeleeUnit
{
    UnitData unitData2;

    protected override void Awake()
    {
        base.Awake();

        unitData2 = GameManager.instance.unitsData.list[3];

        myName = unitData2.myName;
        level = GameManager.instance.NowStage * 3;         //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData2.maxHP;
        nowHP = unitData2.maxHP;
        damage = unitData2.damage;

        range = unitData2.range;
        moveSpeed = -unitData2.moveSpeed;
        attackSpeed = unitData2.attackSpeed;

        attackCooltime = attackSpeed;
    }
}
