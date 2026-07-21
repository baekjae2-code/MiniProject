using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnBtn : MonoBehaviour
{
    public void OnSpawnUnit()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject; //선택한 버튼의 이름 불러오기
        string[] name = clicked.name.Split();
        nowUnitNumber = GameManager.instance.deckUnitNumber[int.Parse(name[1])];

        float spawnTime = GameManager.instance.printData[nowUnitNumber].spawn;
        float useMP = GameManager.instance.printData[nowUnitNumber].mana;

        if (BattleManager.instance.GetManaNow() > useMP)
        {
            BattleManager.instance.UseMana(useMP);
            TeamSpawnManager.instance.Spawn(nowUnitNumber, spawnTime);
        }
    }
    int nowUnitNumber;

    public GameObject mySkill;

    public void OnClickUseSkill()
    {
        if (BattleManager.instance.GetManaNow() > 2)
        {
            BattleManager.instance.UseMana(2);

            for (int i = 0; i < 3; i++)
            {
                GameObject skill = Instantiate(mySkill);
                skill.transform.position = new Vector3(-10f, -0.5f);
            }
        }
    }

}
