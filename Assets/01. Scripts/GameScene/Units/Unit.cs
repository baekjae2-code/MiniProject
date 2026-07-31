using System.Collections;
using System.Linq;
using UnityEditor;
using UnityEngine;

public enum State
{
    Move,
    Attack,
    Stun,
    Die
}


public abstract class Unit : MonoBehaviour
{
    public State state;

    protected string myName;
    protected int level;
    public float damage;    //Attack.cs에서 데미지 불러올때 필요하기때문에 public
    protected float range;
    protected float moveSpeed;
    protected float attackSpeed;
    protected float mana;
    protected float spawn;
    public float maxHP { get; set; }
    public float nowHP { get; set; }

    public GameObject[] attackObj;
    protected Rigidbody2D rb;
    protected Transform target;
    protected float attackCooltime;
    protected SpriteRenderer[] sr;

    protected bool isDie;

    protected int myLayer;
    protected int enemyLayer;

    protected WaitForSeconds findEnemy;
    protected virtual void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();

        attackObj = GetComponentsInChildren<Transform>().Where((x) => x.gameObject.layer == 10).Select(x => x.gameObject).ToArray();
        attackObj[0].SetActive(false);

        findEnemy = new WaitForSeconds(0.2f);
    }
    protected virtual void OnEnable()
    {
        Respawn();
        ChangeState(State.Move);
        StartCoroutine(SearchEnemy());
    }
    private void Start()
    {
        myLayer = gameObject.layer;
        enemyLayer = LayerMask.NameToLayer("Enemy");
        if (myLayer == enemyLayer)
            enemyLayer = LayerMask.NameToLayer("Player");
    }
    protected virtual void Update()
    {

        switch (state)
        {
            case State.Move:
                attackCooltime += Time.deltaTime;
                MoveState();
                break;

            case State.Attack:
                attackCooltime += Time.deltaTime;
                AttackState();
                break;

            case State.Stun:
                StunState();
                break;

            case State.Die:
                DieState();
                break;
        }
    }
    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    public void ChangeState(State newState)
    {
        if (state == newState)
            return;

        state = newState;
    }

    protected virtual void MoveState()
    {
        if (attackCooltime > attackSpeed)
            Move();

        if (target != null)
            ChangeState(State.Attack);
    }
    protected virtual void AttackState()
    {
        if (attackCooltime > attackSpeed)
            Attack();

        if (target == null)
            ChangeState(State.Move);
    }
    protected virtual void StunState()
    {

    }
    protected void DieState()
    {
        if (isDie)
            return;

        isDie = true;

        Die();
    }
    //ㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡㅡ
    protected virtual void Move()
    {
        rb.linearVelocity = new Vector2(moveSpeed, rb.linearVelocity.y);

        if (target != null)
        {
            ChangeState(State.Attack);
        }

    }

    protected virtual void Attack()
    {
        attackCooltime = 0;
        target = null;
    }
    protected virtual void CheckEnemy()
    {
        target = null;
        int targetLayer = 1 << enemyLayer;
        Collider2D collider = Physics2D.OverlapCircle(transform.position, range, targetLayer);

        target = collider != null ? collider.transform : null;
    }
    public void TakeDamage(float damage)
    {
        SoundManager.instance.PlaySFX((SFXType)6);
        nowHP -= damage;
        if (nowHP < 0)
        {
            nowHP = 0;
            ChangeState(State.Die);
        }
    }
    public void TakeHeal(int heal)
    {
        nowHP += heal;
        if (nowHP > maxHP)
        {
            nowHP = maxHP;
        }
    }

    public void Stun(float time)
    {
        if (!gameObject.activeInHierarchy)
            return;
        StartCoroutine(StunCoroutine(time));
    }
    IEnumerator StunCoroutine(float time)
    {
        ChangeState(State.Stun);
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = new Color(150 / 255f, 150 / 255f, 1);
        }
        yield return new WaitForSeconds(time);
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = Color.white;
        }
        ChangeState(State.Move);
    }
    protected virtual void Die()        //base에서 사망시 함수 새로 오버라이드하기 때문에 virtual로 생성
    {
        SoundManager.instance.PlaySFX((SFXType)5);

        StopAllCoroutines();
        gameObject.layer = 0;
        rb.linearVelocity = new Vector2(0, 10f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.angularVelocity += Random.Range(-200, 200);
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = new Color(1, 50 / 255f, 50 / 255f);
        }
        gameObject.GetComponent<Collider2D>().enabled = false;
        isDie = true;
        ReturnPool();
    }
    public void ReturnPool()
    {
        ObjectPoolManager.instance.ReturnObject(name.Split("(Clone)")[0], gameObject, 3f);
    }

    public void Respawn()
    {
        gameObject.GetComponent<Collider2D>().enabled = true;
        transform.rotation = Quaternion.identity;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = new Color(1, 1, 1);
        }
        isDie = false;
        target = null;
        nowHP = maxHP;
        attackCooltime = attackSpeed;
        StopAllCoroutines();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EndPopUp"))
        {
            gameObject.GetComponent<Unit>().TakeDamage(9999999);
        }
    }
    protected virtual IEnumerator SearchEnemy() // GrabUnit은 타겟이 계속 바뀌면 안되기때문에 virtual override로 재선언
    {
        yield return new WaitForSeconds(Random.Range(0f, 0.2f));
        while (true)
        {
            CheckEnemy();
            yield return findEnemy;
        }
    }
}
