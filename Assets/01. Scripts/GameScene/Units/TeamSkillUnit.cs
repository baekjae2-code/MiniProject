using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class TeamSkillUnit : Unit
{
    protected GameObject attackObj;
    protected GameObject attackObj2;
    UnitData unitData;
    bool isAttack;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        unitData = GameManager.instance.printData[8];

        myName = unitData.myName;
        level = unitData.level;
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        attackObj = transform.Find("AttackEffect").gameObject;
        attackObj2 = transform.Find("AttackEffect2").gameObject;
        attackCooltime = attackSpeed;

        isAttack = false;
    }

    private void FixedUpdate()
    {
        if (isStun == true)
            return;

        attackCooltime += Time.deltaTime;

        if (attackCooltime > 5 && isAttack)
            isAttack = false;

        CheckEnemy();
        if (target != null && attackCooltime > attackSpeed)
        {
            Attack();
        }
        if (canMove && !isAttack)
        {
            Move();
        }
    }

    void CheckEnemy()
    {
        target = null;
        int layer = LayerMask.NameToLayer("Enemy");
        int targetLayer = 1 << layer;
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, range, targetLayer);
        if (collider.Length == 0)
        {
            canMove = true;
        }
        else
        {
            target = collider.OrderBy(col => Vector2.Distance(transform.position, col.transform.position)).FirstOrDefault().transform;

            canMove = false;
        }
    }
    public void Attack()
    {
        StartCoroutine(Skill());
        attackCooltime = 0;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

    IEnumerator Skill()
    {
        isAttack = true;
        for (int i = 0; i < 3; i++)
        {
            if (target == null)
                break;
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * Random.Range(4, 6) + new Vector2(0f, 4f);
            attackObj.SetActive(true);
            yield return new WaitForSeconds(0.55f);
        }
        rb.linearVelocityY = 7f;
        yield return new WaitForSeconds(0.7f);
        rb.linearVelocityY = -20f;
        yield return new WaitForSeconds(0.2f);
        attackObj2.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        rb.linearVelocity = new Vector2(-5f, 3f);

        yield return new WaitForSeconds(2f);
        isAttack = false;
    }
}
