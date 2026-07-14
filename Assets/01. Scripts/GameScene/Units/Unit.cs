using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected string myName;
    protected int level;
    protected float damage;
    protected float range;
    protected float moveSpeed;
    protected float attackSpeed;
    protected float mana;
    protected float spawn;
    [SerializeField] protected float maxHP;
    [SerializeField] protected float nowHP;

    protected Rigidbody2D rb;
    protected bool canMove;
    protected Transform target;
    protected bool isStun;
    protected float attackCooltime;
    protected bool isDead;
    SpriteRenderer[] sr;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(int damage)
    {
        nowHP -= damage;
        if (nowHP < 0)
        {
            nowHP = 0;
            Die();
            isDead = true;
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
        StopAllCoroutines();
        StartCoroutine(StunCoroutine(time));
    }
    IEnumerator StunCoroutine(float time)
    {
        isStun = true;
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = Color.blue;
        }
        yield return new WaitForSeconds(time);
        isStun = false;
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = Color.white;
        }
    }
    protected virtual void Die()        //base에서 사망시 함수 새로 오버라이드하기 때문에 virtual로 생성
    {
        StopAllCoroutines();
        gameObject.layer = 0;
        rb.linearVelocity = new Vector2(0, 10f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.angularVelocity += Random.Range(-100f, 100f);
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = Color.red;
        }
        Destroy(gameObject, 3f);
        gameObject.GetComponent<Collider2D>().enabled = false;
        gameObject.GetComponent<Unit>().enabled = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("EndPopUp"))
        {
            gameObject.GetComponent<Unit>().TakeDamage(999);
        }
    }

}
