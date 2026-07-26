using System.Linq;
using UnityEngine;

public class TeamKingMeleeUnit : TeamMeleeUnit
{
    UnitData unitData2;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        unitData2 = GameManager.instance.printData[3];

        myName = unitData2.myName;
        level = unitData2.level;
        maxHP = unitData2.maxHP;
        nowHP = unitData2.maxHP;
        damage = unitData2.damage;
        range = unitData2.range;
        moveSpeed = unitData2.moveSpeed;
        attackSpeed = unitData2.attackSpeed;

        rb = GetComponent<Rigidbody2D>();
        attackObj = transform.Find("AttackEffect").gameObject;
        attackCooltime = attackSpeed;
    }
    private void OnEnable()
    {
        Respawn();
        gameObject.layer = 7;

        Awake();
    }
}
