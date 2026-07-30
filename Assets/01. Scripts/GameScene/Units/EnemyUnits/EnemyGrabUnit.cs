using System.Collections;
using System.Linq;
using UnityEngine;

public class EnemyGrabUnit : Unit
{
    bool isGrab;
    UnitData unitData;

    protected override void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        unitData = GameManager.instance.unitsData.list[6];

        myName = unitData.myName;
        level = GameManager.instance.NowStage * 3;         //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;

        range = unitData.range;
        moveSpeed = -unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        attackCooltime = attackSpeed;

        Respawn();
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
    private void FixedUpdate()
    {
        if (isDie == true)
            return;

        if (target != null && isGrab)
        {
            target.localPosition = transform.position + new Vector3(-0.5f, 0.5f);
            target.GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }
    }
    protected override void CheckEnemy()
    {
        target = null;
        int targetLayer = 1 << enemyLayer;
        Collider2D collider = Physics2D.OverlapCircle(transform.position, range, targetLayer);
        if (collider == null)
        {
            ChangeState(State.Move);
        }
        else
        {
            if (collider.tag == "Grabbed")
                return;
            target = collider.transform;
        }
    }
    protected override void Attack()
    {
        rb.linearVelocity = new Vector2(0f, 5f);
        StartCoroutine(Throw());
        attackCooltime = 0;
    }
    IEnumerator Throw()
    {
        Transform throwTarget = target;
        isGrab = true;
        if (throwTarget != null)
        {
            throwTarget.tag = "Grabbed";
            throwTarget.GetComponent<Collider2D>().enabled = false;
            throwTarget.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
        yield return new WaitForSeconds(0.5f);
        if (throwTarget != null)
        {
            throwTarget.tag = "Untagged";
            throwTarget.GetComponent<Unit>().Stun(2f);
            throwTarget.GetComponent<Collider2D>().enabled = true;
            throwTarget.GetComponent<Rigidbody2D>().gravityScale = 1;
            throwTarget.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(Random.Range(-3f, -5f), Random.Range(1f, 2f));
        }
        target = null;
        isGrab = false;
    }
    override protected void StunState()
    {
        if (target != null)
        {
            target.tag = "Untagged";
            target.GetComponent<Collider2D>().enabled = true;
            target.GetComponent<Rigidbody2D>().gravityScale = 1;
            target = null;
        }
        base.StunState();
    }
    override protected void Die()
    {
        if (target != null)
        {
            target.tag = "Untagged";
            target.GetComponent<Collider2D>().enabled = true;
            target.GetComponent<Rigidbody2D>().gravityScale = 1;
            target = null;
        }
        base.Die();
    }
    override protected IEnumerator SearchEnemy()
    {
        while (true)
        {
            if (target == null && attackCooltime > attackSpeed)
                CheckEnemy();
            yield return new WaitForSeconds(0.2f);
        }
    }
}
