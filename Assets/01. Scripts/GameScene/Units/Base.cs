using UnityEngine;

public class Base : Unit
{
    private void OnDisable()
    {
        UIManager.instance.GameOverUI(name);
    }
}
