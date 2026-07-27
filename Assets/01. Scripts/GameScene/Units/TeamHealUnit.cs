using System.Linq;
using UnityEngine;

public class TeamHealUnit : Unit
{
    GameObject healObj;
    UnitData unitData;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

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
    private void OnEnable()
    {
        Respawn();
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
            Heal();
        }
        if (canMove)
        {
            Move();
        }
    }
  
    void Heal()
    {
        SoundManager.instance.PlaySFX((SFXType)3);

        GameObject u = ObjectPoolManager.instance.GetObject(healObj.name);
        u.transform.position = transform.position;
        ObjectPoolManager.instance.ReturnObject(healObj.name, u, 1);

        attackCooltime = 0;
        target = null;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);
    }

}
