using UnityEngine;

public class Base : Unit
{
    protected override void Die()
    {
        UIManager.instance?.GameOverUI(gameObject.name);
        Destroy(gameObject);
    }
}
