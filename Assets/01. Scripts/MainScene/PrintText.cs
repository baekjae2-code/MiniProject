using System.Collections;
using TMPro;
using UnityEngine;

public class PrintText : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI stateText;
    public TextMeshProUGUI costText;

    public TextMeshProUGUI levelUpText;

    protected int nowUnitNumber;

    public TextMeshProUGUI goldText;

    protected void PrintTexts()
    {
        goldText.text = GameManager.instance.Gold.ToString();

        if (nowUnitNumber == -1)
            return;

        UnitData nowUnit = GameManager.instance.printData[nowUnitNumber];
        nameText.text = $"Name  : {nowUnit.myName}";
        stateText.text = $"Lv   : {nowUnit.level}\n" +
            $"Hp   : {nowUnit.maxHP:F0}\n" +
            $"Damage    : {nowUnit.damage:F0}\n" +
            $"Range : {nowUnit.range}";
        costText.text = $"Mana  : {nowUnit.mana}\n" +
            $"Spawn : {nowUnit.spawn}";

        int cost = 100;
        int level = nowUnit.level;

        for (int i = 0; i < level - 1; i++)
        {
            cost = (int)((cost + 100) * 1.15f);
        }
        levelUpText.text = $"Level Up : {cost} G";
        GameManager.instance.printData[nowUnitNumber] = nowUnit;
    }
}
