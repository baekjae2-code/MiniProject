using UnityEngine;

public class SpawnBtn : MonoBehaviour
{
    public void OnSpawnBtn()
    {
        if (BattleManager.instance.GetManaNow() > 10)
        {
            BattleManager.instance.UseMana(10);
            TeamSpawnManager.instance.Spawn();
        }
    }
}
