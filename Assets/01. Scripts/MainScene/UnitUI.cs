using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI stateText;
    public TextMeshProUGUI costText;

    int nowUnitNumber;

    public TextMeshProUGUI goldText;

    private void Start()
    {
        nowUnitNumber = 0;
        goldText.text = GameManager.instance.Gold.ToString();
    }

    public void OnClickUnitSelect()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject; //선택한 버튼의 이름 불러오기

        string[] btnName = clicked.name.Split();
        nowUnitNumber = int.Parse(btnName[1]);

        PrintText();
    }

    public void OnClickUnitLevelUp()
    {
        GameManager.instance.printData[nowUnitNumber].level++;
        GameManager.instance.printData[nowUnitNumber].maxHP += (GameManager.instance.printData[nowUnitNumber].maxHP) / 10;
        GameManager.instance.printData[nowUnitNumber].damage += (GameManager.instance.printData[nowUnitNumber].maxHP) / 10;
        GameManager.instance.UseLevelUp(1000);
        goldText.text = GameManager.instance.Gold.ToString();
        PrintText();
    }

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
