using UnityEngine;

public class TeamKingMeleeUnit : TeamMeleeUnit
{
    private UnitData kingUnitData;
    void Start()
    {
        kingUnitData = GameManager.instance.printData[3];

        myName = kingUnitData.myName;
        level = kingUnitData.level;
        maxHP = kingUnitData.maxHP;
        nowHP = kingUnitData.maxHP;
        damage = kingUnitData.damage;
        range = kingUnitData.range;
        moveSpeed = kingUnitData.moveSpeed;
        attackSpeed = kingUnitData.attackSpeed;

        rb = GetComponent<Rigidbody2D>();
        attackObj = transform.Find("AttackEffect").gameObject;
        attackCooltime = attackSpeed;
    }
}
