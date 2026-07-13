using System.Linq;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class TeamHealUnit : Unit
{
    public GameObject healObj;

    UnitData healData;
    void Start()
    {
        healData = GameManager.instance.printData[4];

        myName = healData.myName;
        level = healData.level;
        maxHP = healData.maxHP;
        nowHP = healData.maxHP;
        damage = healData.damage;
        range = healData.range;
        moveSpeed = healData.moveSpeed;
        attackSpeed = healData.attackSpeed;

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
