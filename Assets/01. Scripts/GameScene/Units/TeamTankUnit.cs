using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TeamTankUnit : Unit
{
    UnitData unitData;

    protected override void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        rb = GetComponent<Rigidbody2D>();
    }
    protected override void OnEnable()
    {
        unitData = GameManager.instance.printData[2];

        myName = unitData.myName;
        level = unitData.level;
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        base.OnEnable();
    }
    protected override void Attack()
    {
        //GameObject obj = Instantiate(tankObj, transform.position, tankObj.transform.rotation);
        //obj.SetActive(true);
        if (target == null)
            return;

        rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        base.Attack();
    }
}
