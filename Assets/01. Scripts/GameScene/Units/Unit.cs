using System.Collections;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] protected int maxHP;
    [SerializeField] protected int nowHP;

    protected float moveSpeed;
    protected Rigidbody2D rb;
    protected bool canMove;
    protected float range;
    protected Transform target;
    protected bool isStun;
    protected float attackCooltime;
    protected float attackSpeed;
    SpriteRenderer[] sr;

    void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        //nowHP = 10;
        //maxHP = 10;
    }

    public void TakeDamage(int damage)
    {
        nowHP -= damage;
        if (nowHP < 0)
        {
            nowHP = 0;
            Die();
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
    void Die()
    {
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
