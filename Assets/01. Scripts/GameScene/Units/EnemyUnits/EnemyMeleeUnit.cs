using UnityEngine;

public class EnemyMeleeUnit : Unit
{
    UnitData unitData;
    protected override void Awake()
    {
        base.Awake();

        unitData = GameManager.instance.unitsData.list[0]; //적 유닛은 원본 데이터에서 데이터 불러옴(printData는 내 level에 영향받기때문)

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

        SoundManager.instance.PlaySFX((SFXType)0);

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * Random.Range(3f, 4f) + new Vector2(0f, 3f);
        attackObj[0].GetComponent<EnemyMeleeAttack>().damage = damage;
        attackObj[0].SetActive(true);

        base.Attack();
    }
}
