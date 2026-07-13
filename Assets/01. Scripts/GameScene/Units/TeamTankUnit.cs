using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TeamTankUnit : Unit
{
    GameObject tankObj;
    UnitData tankUnit;

    void Start()
    {
        tankUnit = GameManager.instance.printData[2];

        myName = tankUnit.myName;
        level = tankUnit.level;
        maxHP = tankUnit.maxHP;
        nowHP = tankUnit.maxHP;
        damage = tankUnit.damage;
        range = tankUnit.range;
        moveSpeed = tankUnit.moveSpeed;
        attackSpeed = tankUnit.attackSpeed;

        //tankObj = transform.Find("HealMagicEffect").gameObject;
    }

    private void FixedUpdate()
    {
        if (isStun == true)
            return;

        CheckEnemy();
        if (target != null)
        {
            Tank();
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
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.5f, targetLayer);
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
    void Tank()
    {
        //GameObject obj = Instantiate(tankObj, transform.position, tankObj.transform.rotation);
        //obj.SetActive(true);
        rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }
}
