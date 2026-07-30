using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffect : MonoBehaviour
{
    public float damage;
    // 유니티 파티클 시스템이 충돌할 때 자동으로 실행되는 이벤트 함수
    private void OnParticleCollision(GameObject other)      //particle의  collision에서 충돌할 layer 설정
    {
        if (other.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.linearVelocity += new Vector2(0, Random.Range(1f, 2f));
        }

        if (other.TryGetComponent<Unit>(out var unit))
        {
            unit.Stun(1f);
            unit.TakeDamage(damage);
        }
    }
}
