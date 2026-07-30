using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TeamSkillUnit : Unit
{
    UnitData unitData;
    WaitForSeconds attack1Time;
    WaitForSeconds attack2Time;

    protected override void Awake()
    {
        base.Awake();

        unitData = GameManager.instance.printData[8];

        myName = unitData.myName;
        level = unitData.level;
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;
        attackCooltime = attackSpeed;

        attack1Time = new WaitForSeconds(0.55f);
        attack2Time = new WaitForSeconds(0.2f);
    }

    protected override void Attack()
    {
        StartCoroutine(Skill());
        base.Attack();
    }
    protected override void StunState()
    {
        StopCoroutine(Skill());
    }
    IEnumerator Skill()
    {
        attackObj[0].GetComponent<SkillMeleeAttack>().damage = damage;
        attackObj[1].GetComponent<SkillMeleeAttack>().damage = damage;
        for (int i = 0; i < 3; i++)
        {
            if (target == null)
                break;
            SoundManager.instance.PlaySFX((SFXType)0);
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * Random.Range(4, 6) + new Vector2(0f, 4f);
            attackObj[0].SetActive(true);
            yield return attack1Time;
        }
        rb.linearVelocityY = 7f;
        yield return attack1Time;
        rb.linearVelocityY = -20f;
        yield return attack2Time;
        SoundManager.instance.PlaySFX((SFXType)0);
        attackObj[1].SetActive(true);
        yield return attack1Time;
        rb.linearVelocity = new Vector2(-5f, 3f);
    }
}
