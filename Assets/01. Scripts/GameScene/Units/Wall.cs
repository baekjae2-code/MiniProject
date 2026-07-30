using System.Linq;
using UnityEngine;

public class Wall : Unit
{
    protected override void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        rb = GetComponent<Rigidbody2D>();
        maxHP = 200f; 
        nowHP = 200f;
    }

    protected void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }
    protected override void Die()
    {
        EnemySpawnManager.instance.SpawnWall();
        Destroy(gameObject);
    }
}
