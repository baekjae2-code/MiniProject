using System.Collections;
using UnityEngine;

public class TeamRangedAttack : MonoBehaviour
{
    public GameObject hitEffect;
    public float damage;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(DestroyCoroutine());
    }
    void Update()
    {
        if (rb.linearVelocity.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(rb.linearVelocity.y, rb.linearVelocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Unit>().TakeDamage(damage);
            Vector2 direction = collision.transform.position - transform.position;
            collision.GetComponent<Rigidbody2D>().linearVelocity += direction * Random.Range(1f, 3f);
            string names = name.Split("(Clone)")[0];
            ObjectPoolManager.instance.ReturnObject(names, gameObject);

            GameObject u = ObjectPoolManager.instance.GetObject(hitEffect.name);
            u.transform.position = transform.position;
            ObjectPoolManager.instance.ReturnObject(hitEffect.name, u, 1);
        }
    }
    IEnumerator DestroyCoroutine()
    {
        yield return new WaitForSeconds(2f);

        string names = name.Split("(Clone)")[0];
        ObjectPoolManager.instance.ReturnObject(names, gameObject);
    }
}
