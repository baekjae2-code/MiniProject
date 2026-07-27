using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyKingMeleeUnit : EnemyMeleeUnit
{
    UnitData unitData2;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        unitData2 = GameManager.instance.unitsData.list[3];

        myName = unitData2.myName;
        level = GameManager.instance.NowStage * 3;         //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData2.maxHP;
        nowHP = unitData2.maxHP;
        damage = unitData2.damage;

        for (int i = 0; i < GameManager.instance.NowStage; i++)
        {
            maxHP += (maxHP / 10f);
            nowHP += (nowHP / 10f);
            damage += (damage / 10f);
        }

        range = unitData2.range;
        moveSpeed = unitData2.moveSpeed;
        attackSpeed = unitData2.attackSpeed;

        attackObj = transform.Find("AttackEffect").gameObject;
        attackCooltime = attackSpeed;
    }
    private void OnEnable()
    {
        Respawn();

        maxHP = unitData2.maxHP;
        nowHP = unitData2.maxHP;
        damage = unitData2.damage;

        for (int i = 0; i < GameManager.instance.NowStage; i++)
        {
            maxHP += (maxHP / 10f);
            nowHP += (nowHP / 10f);
            damage += (damage / 10f);
        }
    }
}
