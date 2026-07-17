using System.Collections;
using System.Linq;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    protected string myName;
    protected int level;
    public float damage;    //Attack.cs에서 데미지 불러올때 필요하기때문에 public
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
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();    
        //Linq를 이용하여 미니맵 객체를 배열에 넣지않음( 스턴, 사망시 색 변경 제외)
        rb = GetComponent<Rigidbody2D>();
    }

    public void TakeDamage(float damage)
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
            sr[i].color = new Color(150 / 255f, 150 / 255f, 1);
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
        rb.angularVelocity += Random.Range(-200, 200);
        for (int i = 0; i < sr.Length; i++)
        {
            sr[i].color = new Color(1, 50 / 255f, 50 / 255f);
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
