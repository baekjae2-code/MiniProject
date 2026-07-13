using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnBtn : MonoBehaviour
{
    public void OnSpawnUnit()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject; //선택한 버튼의 이름 불러오기
        string[] name = clicked.name.Split();

        float spawnTime = 3f;
        int useMP = 0;

        //if (GameManager.instance.deckUnitNumber[int.Parse(name[1])] == )

        if (BattleManager.instance.GetManaNow() > useMP)
        {
            BattleManager.instance.UseMana(useMP);
            TeamSpawnManager.instance.Spawn(GameManager.instance.deckUnitNumber[int.Parse(name[1])], spawnTime);
        }
    }
}
