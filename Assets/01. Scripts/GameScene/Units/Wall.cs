using UnityEngine;

public class Wall : Unit
{
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0,0);
    }
    protected override void Die()
    {
        EnemySpawnManager.instance.SpawnWall();
        Destroy(gameObject);
    }
}
