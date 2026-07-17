using UnityEngine;

public class TeamKingMeleeUnit : TeamMeleeUnit
{
    UnitData unitData;

    void Start()
    {
        unitData = GameManager.instance.printData[3];

        myName = unitData.myName;
        level = unitData.level;
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        rb = GetComponent<Rigidbody2D>();
        attackObj = transform.Find("AttackEffect").gameObject;
        attackCooltime = attackSpeed;
    }
}
