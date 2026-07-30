using System.Linq;
using UnityEngine;

public class TeamKingMeleeUnit : TeamMeleeUnit
{
    UnitData unitData2;

    protected override void Awake()
    {
        base.Awake();
    }
    protected override void OnEnable()
    {
        unitData2 = GameManager.instance.printData[3];

        myName = unitData2.myName;
        level = unitData2.level;
        maxHP = unitData2.maxHP;
        nowHP = unitData2.maxHP;
        damage = unitData2.damage;
        range = unitData2.range;
        moveSpeed = unitData2.moveSpeed;
        attackSpeed = unitData2.attackSpeed;

        base.OnEnable();
    }
}
