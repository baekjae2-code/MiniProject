using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicEffect : MonoBehaviour
{
    private void Awake()
    {
        Destroy(gameObject, 5);
    }
    private HashSet<int> hitEnemies = new HashSet<int>();

    // 유니티 파티클 시스템이 충돌할 때 자동으로 실행되는 이벤트 함수
    private void OnParticleCollision(GameObject other)
    {
        // 충돌한 오브젝트의 고유 ID 구하기
        int enemyInstanceID = other.GetInstanceID();

        // 이미 한 번 맞은 적이라면 무시
        if (hitEnemies.Contains(enemyInstanceID)) return;

        // 처음 맞은 적이라면 저장 후 효과 적용
        hitEnemies.Add(enemyInstanceID);

        Debug.Log($"파티클이 {other.name}에 충돌함");

        if (other.TryGetComponent<Rigidbody2D>(out var rb))
        {
            rb.linearVelocity += new Vector2(0, Random.Range(3f, 5f));
        }

        if (other.TryGetComponent<Unit>(out var unit))
        {
            unit.Stun(2f);
            unit.TakeDamage(2);
        }
    }
}
