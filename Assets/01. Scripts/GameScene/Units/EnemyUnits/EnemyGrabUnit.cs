using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyGrabUnit : Unit
{
    bool isGrab;
    UnitData unitData;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        unitData = GameManager.instance.unitsData.list[6];

        myName = unitData.myName;
        level = unitData.level + GameManager.instance.NowStage;        //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;

        for (int i = 0; i < GameManager.instance.NowStage; i++)
        {
            maxHP += (maxHP / 10f);
            nowHP += (nowHP / 10f);
            damage += (damage / 10f);
        }

        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        attackCooltime = attackSpeed;
    }

    private void FixedUpdate()
    {
        if (target != null && isGrab)
        {
            target.position = transform.position + new Vector3(-0.5f, 0.5f);
            target.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }

        if (isStun == true)
            return;

        attackCooltime += Time.deltaTime;

        if (!isGrab)
        {
            CheckEnemy();
        }
        if (target != null && attackCooltime > attackSpeed)
        {
            Attack();
        }
        if (canMove)
        {
            Move();
        }
    }
    void CheckEnemy()
    {
        target = null;
        int layer = LayerMask.NameToLayer("Player");
        int targetLayer = 1 << layer;
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 1, targetLayer);
        if (collider.Length == 0)
        {
            canMove = true;
        }
        else
        {

            target = collider.OrderBy(col => Vector2.Distance(transform.position, col.transform.position)).FirstOrDefault().transform;
            if (target.transform.parent != null)
                target = null;

            canMove = false;
        }
    }
    void Attack()
    {
        rb.linearVelocity = new Vector2(0f, 5f);
        StartCoroutine(Throw());
        attackCooltime = 0;
    }
    IEnumerator Throw()
    {
        Transform throwTarget = target;
        isGrab = true;
        throwTarget.SetParent(transform);
        throwTarget.GetComponent<Unit>().Stun(2f);
        throwTarget.GetComponent<Collider2D>().enabled = false;
        throwTarget.GetComponent<Rigidbody2D>().gravityScale = 0;

        yield return new WaitForSeconds(0.5f);
        if (throwTarget != null)
        {
            throwTarget.SetParent(null);
            throwTarget.GetComponent<Unit>().StopAllCoroutines();
            throwTarget.GetComponent<Unit>().Stun(2f);
            throwTarget.GetComponent<Collider2D>().enabled = true;
            throwTarget.GetComponent<Rigidbody2D>().gravityScale = 1;
            throwTarget.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Random.Range(-3f, -5f), Random.Range(1f, 2f));
        }
        isGrab = false;
        target = null;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }

    override protected void Die()
    {
        if (target != null)
        {
            target.SetParent(null); 
            target.GetComponent<Unit>().StopAllCoroutines();
            target.GetComponent<Collider2D>().enabled = true;
            target.GetComponent<Rigidbody2D>().gravityScale = 1;
        }
        base.Die();
    }
}
