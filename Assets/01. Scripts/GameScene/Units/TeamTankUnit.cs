using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TeamTankUnit : Unit
{
    public GameObject tankObj;

    UnitData tankData;

    void Start()
    {
        tankData = GameManager.instance.printData[2];

        myName = tankData.myName;
        level = tankData.level;
        maxHP = tankData.maxHP;
        nowHP = tankData.maxHP;
        damage = tankData.damage;
        range = tankData.range;
        moveSpeed = tankData.moveSpeed;
        attackSpeed = tankData.attackSpeed;

        //tankObj = transform.Find("HealMagicEffect").gameObject;
    }

    private void FixedUpdate()
    {
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
