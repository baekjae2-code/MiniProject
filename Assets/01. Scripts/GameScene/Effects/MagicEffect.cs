using System.Collections;
using UnityEngine;

public class MagicEffect : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public LayerMask enemyLayer;
    public float hitRadius;

    private ParticleSystem.Particle[] particles;
    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
        Destroy(gameObject, 5);
    }
    void Update()
    {
        int count = particleSystem.particleCount;

        if (particles == null || particles.Length < count)
            particles = new ParticleSystem.Particle[count];

        count = particleSystem.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            // 파티클의 월드 좌표
            Vector3 worldPos = particleSystem.transform.TransformPoint(particles[i].position);

            // 파티클 주변 충돌 검사
            Collider2D[] hits = Physics2D.OverlapCircleAll(worldPos, hitRadius, enemyLayer);

            foreach (Collider2D hit in hits)
            {
                Debug.Log($"파티클 {i}가 {hit.name}에 닿음");

                // 데미지
                hit.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(0, Random.Range(1f, 2f));
                hit.GetComponent<Unit>().Stun(2f);
                break;
            }
        }

        particleSystem.SetParticles(particles, count);
    }
    // 검사 범위 확인
    void OnDrawGizmosSelected()
    {
        if (particleSystem == null) return;

        int count = particleSystem.particleCount;

        if (particles == null || particles.Length < count)
            particles = new ParticleSystem.Particle[count];

        count = particleSystem.GetParticles(particles);

        Gizmos.color = Color.red;

        for (int i = 0; i < count; i++)
        {
            Vector3 worldPos = particleSystem.transform.TransformPoint(particles[i].position);
            Gizmos.DrawWireSphere(worldPos, hitRadius);
        }
    }
}
