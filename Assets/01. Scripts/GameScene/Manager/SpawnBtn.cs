using UnityEngine;

public class SpawnBtn : MonoBehaviour
{
    [SerializeField] GameObject spawnTeam;

    private void Start()
    {
        spawnTeam = GameObject.Find("TeamSpawnManager");
    }
    public void OnSpawnBtn()
    {
        if (BattleManager.instance.GetManaNow() > 10)
        {
            BattleManager.instance.UseMana(10);
            spawnTeam.GetComponent<TeamSpawnManager>().Spawn();
        }
    }
}
