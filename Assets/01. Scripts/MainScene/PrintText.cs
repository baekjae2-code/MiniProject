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

        nameText.text = $"Name  : {GameManager.instance.printData[nowUnitNumber].myName}";
        stateText.text = $"Lv   : {GameManager.instance.printData[nowUnitNumber].level}\n" +
            $"Hp   : {GameManager.instance.printData[nowUnitNumber].maxHP:F0}\n" +
            $"Damage    : {GameManager.instance.printData[nowUnitNumber].damage:F0}\n" +
            $"Range : {GameManager.instance.printData[nowUnitNumber].range}";
        costText.text = $"Mana  : {GameManager.instance.printData[nowUnitNumber].mana}\n" +
            $"Spawn : {GameManager.instance.printData[nowUnitNumber].spawn}";

        levelUpText.text = $"Level Up : {GameManager.instance.printData[nowUnitNumber].level * 100} G";
    }
}
