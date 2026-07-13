using TMPro;
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

        nowUnitNumber = GameManager.instance.deckUnitNumber[int.Parse(name[1])];

        if (BattleManager.instance.GetManaNow() > useMP)
        {
            BattleManager.instance.UseMana(useMP);
            TeamSpawnManager.instance.Spawn(GameManager.instance.deckUnitNumber[int.Parse(name[1])], spawnTime);
            PrintText();
        }
    }
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI stateText;
    public TextMeshProUGUI costText;
    int nowUnitNumber;
    void PrintText()
    {
        nameText.text = $"Name  : {GameManager.instance.printData[nowUnitNumber].myName}";
        stateText.text = $"Lv   : {GameManager.instance.printData[nowUnitNumber].level}\n" +
            $"Hp   : {GameManager.instance.printData[nowUnitNumber].maxHP:F0}\n" +
            $"Damage    : {GameManager.instance.printData[nowUnitNumber].damage:F0}\n" +
            $"Range : {GameManager.instance.printData[nowUnitNumber].range}";
        costText.text = $"Mana  : {GameManager.instance.printData[nowUnitNumber].mana}\n" +
            $"Spawn : {GameManager.instance.printData[nowUnitNumber].spawn}";
    }
}
