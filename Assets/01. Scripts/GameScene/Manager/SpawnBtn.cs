using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnBtn : MonoBehaviour
{
    public void OnSpawnBtn()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject; //선택한 버튼의 이름 불러오기
        string[] name = clicked.name.Split();

        float spawnTime = 0f;
        int useMP = 0;

        if (TeamSpawnManager.instance.teamUnits[int.Parse(name[1])].name == "TeamMeleeUnit") { spawnTime = 3f; useMP = 3; }
        if (TeamSpawnManager.instance.teamUnits[int.Parse(name[1])].name == "TeamTankUnit") { spawnTime = 5f; useMP = 5; }
        if (TeamSpawnManager.instance.teamUnits[int.Parse(name[1])].name == "TeamRangedUnit") { spawnTime = 1f; useMP = 3; }
        if (TeamSpawnManager.instance.teamUnits[int.Parse(name[1])].name == "TeamKingMeleeUnit") { spawnTime = 10f; useMP = 10; }
        if (TeamSpawnManager.instance.teamUnits[int.Parse(name[1])].name == "TeamMagicUnit") { spawnTime = 20f; useMP = 10; }

        if (BattleManager.instance.GetManaNow() > useMP)
        {
            BattleManager.instance.UseMana(useMP);
            TeamSpawnManager.instance.Spawn(int.Parse(name[1]), spawnTime);
        }
    }
}
