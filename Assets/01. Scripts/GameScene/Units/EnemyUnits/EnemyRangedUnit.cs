using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyRangedUnit : Unit
{
    GameObject attackObj;
    UnitData unitData;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        unitData = GameManager.instance.unitsData.list[1];

        myName = unitData.myName;
        level = GameManager.instance.NowStage * 3;        //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData.maxHP; 
        nowHP = unitData.maxHP;
        damage = unitData.damage;

        for(int i = 0; i < GameManager.instance.NowStage; i++)
        {
            maxHP += (maxHP / 10f);
            nowHP += (nowHP / 10f);
            damage += (damage / 10f);
        }

        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        attackObj = transform.Find("EnemyRangedAttack").gameObject;
        attackCooltime = attackSpeed;
    }

    private void OnEnable()
    {
        Respawn();
        gameObject.layer = 8;

        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;

        for (int i = 0; i < GameManager.instance.NowStage; i++)
        {
            maxHP += (maxHP / 10f);
            nowHP += (nowHP / 10f);
            damage += (damage / 10f);
        }
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
        SoundManager.instance.PlaySFX((SFXType)4);

        Vector2 direction = (target.position - transform.position).normalized;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Quaternion rotation = Quaternion.Euler(0, 0, angle);
        //GameObject obj = Instantiate(attackObj, transform.position + Vector3.up * 0.5f, rotation);

        GameObject obj = ObjectPoolManager.instance.GetObject(attackObj.name);
        obj.transform.position = transform.position + Vector3.up * 0.5f;
        obj.GetComponent<EmenyRangedAttack>().damage = damage;
        obj.SetActive(true);
        obj.GetComponent<Rigidbody2D>().linearVelocity = direction * 15 + Vector2.up * 6f;
        attackCooltime = 0;
        target = null;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }
}
