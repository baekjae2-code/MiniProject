using System.Linq;
using UnityEngine;

public class TeamHealUnit : Unit
{
    GameObject healObj;
    UnitData unitData;

    void Start()
    {
        unitData = GameManager.instance.printData[4];

        myName = unitData.myName;
        level = unitData.level;
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        healObj = transform.Find("HealMagicEffect").gameObject;
    }

    private void FixedUpdate()
    {
        if (isStun == true)
            return;

        attackCooltime += Time.deltaTime;

        CheckEnemy();
        if (target != null && attackCooltime > attackSpeed)
        {
            Heal();
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
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 2, targetLayer);
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
    void Heal()
    {
        GameObject obj = Instantiate(healObj, transform.position, healObj.transform.rotation);
        obj.SetActive(true);
        attackCooltime = 0;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

}
