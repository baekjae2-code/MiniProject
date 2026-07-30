using UnityEngine;

public class TeamMeleeAttack : MonoBehaviour
{
    public GameObject hitEffect;
    float lifeTime;
    float timer;
    public float damage;
    void Start()
    {
        lifeTime = 0.5f;
        timer = 0;
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
            Vector2 direction = collision.transform.position - transform.position;
            collision.GetComponent<Rigidbody2D>().linearVelocity += direction * Random.Range(2f, 4f);
        }
    }
}
