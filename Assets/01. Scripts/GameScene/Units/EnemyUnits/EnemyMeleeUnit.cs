using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMeleeUnit : Unit
{
    protected GameObject attackObj;
    UnitData unitData;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        unitData = GameManager.instance.unitsData.list[0]; //적 유닛은 원본 데이터에서 데이터 불러옴(printData는 내 level에 영향받기때문)

        myName = unitData.myName;
        level = GameManager.instance.NowStage * 3;         //스테이지만큼 레벨업(스텟증가)
        maxHP = unitData.maxHP;
        nowHP = unitData.maxHP;
        damage = unitData.damage;

        for (int i = 0; i < GameManager.instance.NowStage; i++)
        {
            maxHP += (maxHP / 10f);
            nowHP += (nowHP / 10f);
            damage += (damage / 10f);
        }

        range = unitData.range;
        moveSpeed = unitData.moveSpeed;
        attackSpeed = unitData.attackSpeed;

        attackObj = transform.Find("AttackEffect").gameObject;
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

    private void FixedUpdate()
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
        SoundManager.instance.PlaySFX((SFXType)0);

        Vector2 direction = (target.position - transform.position).normalized;
        rb.linearVelocity = direction * Random.Range(3f, 4f) + new Vector2(0f, 3f);
        attackObj.SetActive(true);
        attackCooltime = 0;
        target = null;
    }
    public void Move()
    {
        rb.linearVelocity = new Vector2(-moveSpeed, rb.linearVelocity.y);
    }


}
