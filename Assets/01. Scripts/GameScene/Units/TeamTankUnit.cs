using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TeamTankUnit : Unit
{
    GameObject tankObj;
    UnitData unitData;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        unitData = GameManager.instance.printData[2];

        myName = unitData.myName;
        level = unitData.level;
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;
        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        //tankObj = transform.Find("HealMagicEffect").gameObject;
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

        if (target != null)
        {
            Tank();
        }
        if (canMove)
        {
            Move();
        }
    }
    void Tank()
    {
        //GameObject obj = Instantiate(tankObj, transform.position, tankObj.transform.rotation);
        //obj.SetActive(true);
        rb.linearVelocity = new Vector2(0, rb.linearVelocityY);
        target = null;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }
}
