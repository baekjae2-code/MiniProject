using System.Collections;
using UnityEngine;

public class SkillMeleeAttack : MonoBehaviour
{
    public GameObject hitEffect;
    float lifeTime;
    float timer;
    float damage;
    void Start()
    {
        lifeTime = 0.5f;
        timer = 0;
        gameObject.SetActive(false);
        damage = gameObject.GetComponentInParent<Unit>().damage;
    }

    void FixedUpdate()
    {
        timer += Time.deltaTime;
        if (timer >= lifeTime)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Unit>().TakeDamage(damage);
            collision.gameObject.GetComponent<Unit>().Stun(0.5f);
            Vector2 direction = collision.transform.position - transform.position;
            collision.GetComponent<Rigidbody2D>().linearVelocity += direction * Random.Range(3f, 5f);
            GameObject hitEf = Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(hitEf, 1);
        }
    }
}
