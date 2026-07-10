using UnityEngine;

public class TeamRangedAttack : MonoBehaviour
{
    public GameObject hitEffect;
    float lifeTime;
    float timer;
    int damage;
    void Start()
    {
        lifeTime = 2f;
        timer = 0;
        damage = 2;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            timer = 0;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Unit>().TakeDamage(damage);
            Vector2 direction = collision.transform.position - transform.position;
            collision.GetComponent<Rigidbody2D>().linearVelocity += direction * Random.Range(1f, 3f);
            Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
