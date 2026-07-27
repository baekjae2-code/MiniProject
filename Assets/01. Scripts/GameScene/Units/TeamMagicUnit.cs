using System.Linq;
using UnityEngine;

public class TeamMagicUnit : Unit
{
    GameObject attackObj;
    UnitData unitData;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        unitData = GameManager.instance.printData[5];

        myName = unitData.myName;
        level = unitData.level;
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        attackObj = transform.Find("TeamMagicEffect").gameObject;
        attackCooltime = attackSpeed;
    }
    private void OnEnable()
    {
        Respawn();
        Awake();
    }
    void FixedUpdate()
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
    void Attack()
    {
        SoundManager.instance.PlaySFX((SFXType)2);

        GameObject u = ObjectPoolManager.instance.GetObject(attackObj.name);
        u.transform.position = target.position + Vector3.up * 5f + Vector3.right * 1f;
        ObjectPoolManager.instance.ReturnObject(attackObj.name, u, 5);

        attackCooltime = 0;
        target = null;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }
}
