using System.Linq;
using UnityEngine;

public class Base : Unit
{
    protected override void Awake()
    {
        sr = GetComponentsInChildren<SpriteRenderer>().Where((x) => x.gameObject.layer != 12).ToArray();
        rb = GetComponent<Rigidbody2D>();
        maxHP = 500f;
        nowHP = 500f;
    }
    protected void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(0, 0);
    }
    protected override void Die()
    {
        UIManager.instance?.GameOverUI(gameObject.name);
        Destroy(gameObject);
    }
}
