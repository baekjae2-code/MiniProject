using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitUI : PrintText
{
    public GameObject lockUnit;

    public Image[] unitUnlockBtnsColor;

    private void Start()
    {
        nowUnitNumber = -1;
        levelUpText.text = "";

        UnitBtnUI();
        PrintTexts();
    }

    public void OnClickUnitSelect()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject; //선택한 버튼의 이름 불러오기

        string[] btnName = clicked.name.Split();
        nowUnitNumber = int.Parse(btnName[1]);

        PrintTexts();
        UnitBtnUI();

        if (GameManager.instance.printData[nowUnitNumber].level == 0)
        {
            lockUnit.SetActive(true);

            lockUnit.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Unlock : {nowUnitNumber * 1000}";
        }
        else
        {
            lockUnit.SetActive(false);
        }
    }

    public void OnClickUnitLevelUp()
    {
        if (nowUnitNumber == -1)
            return;

        if (GameManager.instance.Gold >= GameManager.instance.printData[nowUnitNumber].level * 100)
        {
            GameManager.instance.UseGold(GameManager.instance.printData[nowUnitNumber].level * 100);

            GameManager.instance.printData[nowUnitNumber].level++;
            GameManager.instance.printData[nowUnitNumber].maxHP += (GameManager.instance.printData[nowUnitNumber].maxHP) / 10;
            GameManager.instance.printData[nowUnitNumber].damage += (GameManager.instance.printData[nowUnitNumber].damage) / 10;
            PrintTexts();
        }
    }

    public void OnClickUnlockUnit()
    {
        if (nowUnitNumber == -1)
            return;

        if (GameManager.instance.printData[nowUnitNumber].level == 0)
        {
            if (GameManager.instance.Gold >= nowUnitNumber * 1000)
            {
                GameManager.instance.printData[nowUnitNumber].level++;
                GameManager.instance.UseGold(nowUnitNumber * 1000);
                lockUnit.SetActive(false);
                UnitBtnUI();
                PrintTexts();
            }
        }
    }
    public void UnitBtnUI() // UnitUI 에서 유닛 해금할때마다 호출
    {
        for (int i = 0; i < GameManager.instance.printData.Length; i++)
        {
            if (GameManager.instance.printData[i].level != 0)
                unitUnlockBtnsColor[i].color = Color.white;
            else
                unitUnlockBtnsColor[i].color = new Color(100 / 255f, 100 / 255f, 100 / 255f);
        }
    }

    public void UnlockActiveFalse() //초기화버튼 누를때 호출
    {
        lockUnit.SetActive(false);
    }

}
