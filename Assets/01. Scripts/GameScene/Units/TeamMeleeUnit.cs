using System.Linq;
using UnityEditor;
using UnityEngine;

public class TeamMeleeUnit : Unit
{
    protected GameObject attackObj;
    UnitData unitData;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        unitData = GameManager.instance.printData[0];

        myName = unitData.myName;
        level = unitData.level;
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        attackObj = transform.Find("AttackEffect").gameObject;
        attackCooltime = attackSpeed;
    }
    private void OnEnable()
    {
        Respawn();
        gameObject.layer = 7;

        Awake();
    }
    private void FixedUpdate()
    {
        if (isStun == true)
            return;
        if (isDie == true)
            return;

        attackCooltime += Time.deltaTime;

        if (target != null && attackCooltime > attackSpeed)
        {
            Attack();
        }
        if (canMove)
        {
            Move();
        }
    }
    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position + Vector3.up * 0.5f, 3f);
    //}
    void Attack()
    {
        SoundManager.instance.PlaySFX((SFXType)0);

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * Random.Range(3f, 4f) + new Vector2(0f, 3f);
        attackObj.SetActive(true);
        attackCooltime = 0;
        target = null;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

}
