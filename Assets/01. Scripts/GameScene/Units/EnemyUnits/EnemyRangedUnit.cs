using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyRangedUnit : Unit
{
    UnitData unitData;

    protected override void Awake()
    {
        base.Awake();

        unitData = GameManager.instance.unitsData.list[1];

        myName = unitData.myName;
        level = GameManager.instance.NowStage * 3;        //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData.maxHP; 
        nowHP = unitData.maxHP;
        damage = unitData.damage;

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

        SoundManager.instance.PlaySFX((SFXType)4);

        Vector2 direction = (target.position - transform.position).normalized;

        GameObject obj = ObjectPoolManager.instance.GetObject(attackObj[0].name);
        obj.transform.position = transform.position + Vector3.up * 0.5f;
        obj.GetComponent<EmenyRangedAttack>().damage = damage;
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().linearVelocity = direction * 15 + Vector2.up * 6f;
        attackCooltime = 0;
        target = null;
    }
}
