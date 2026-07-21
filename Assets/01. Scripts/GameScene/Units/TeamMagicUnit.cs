using System.Linq;
using UnityEngine;

public class TeamMagicUnit : Unit
{
    GameObject attackObj;
    UnitData magicUnit;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        magicUnit = GameManager.instance.printData[5];

        myName = magicUnit.myName;
        level = magicUnit.level;
        maxHP = magicUnit.maxHP;
        nowHP = magicUnit.maxHP;
        damage = magicUnit.damage;
        range = magicUnit.range;
        moveSpeed = magicUnit.moveSpeed;
        attackSpeed = magicUnit.attackSpeed;

        attackObj = transform.Find("TeamMagicEffect").gameObject;
        attackCooltime = attackSpeed;
    }

    void FixedUpdate()
    {
        if (isStun == true)
            return;

        attackCooltime += Time.deltaTime;

        CheckEnemy();
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
    void Attack()
    {
        GameObject obj = Instantiate(attackObj, target.position + Vector3.up * 5f + Vector3.right * 1f, Quaternion.identity);
        obj.SetActive(true);
        attackCooltime = 0;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }
}
