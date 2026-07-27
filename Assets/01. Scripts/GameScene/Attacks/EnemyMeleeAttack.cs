using System.Collections;
using UnityEngine;

public class EnemyMeleeAttack : MonoBehaviour
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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.GetComponent<Unit>().TakeDamage(damage);
            Vector2 direction = collision.transform.position - transform.position;
            collision.GetComponent<Rigidbody2D>().linearVelocity += direction * Random.Range(2f, 3f);

            GameObject u = ObjectPoolManager.instance.GetObject(hitEffect.name);
            u.transform.position = transform.position;
            ObjectPoolManager.instance.ReturnObject(hitEffect.name, u, 1);
        }
    }

}
