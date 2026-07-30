using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitUI : PrintText
{
    public GameObject lockUnit;             //유닛 추가 방법 => UnitsData에 유닛 데이터 추가
                                            //DeckSetting에 이미지 추가, GameManager에 이미지, 객체 프리팹 추가
    public Image[] unitUnlockBtnsColor;

    public GameObject openEffect;
    public GameObject levelUpEffect;

    private void Start()
    {
        nowUnitNumber = -1;
        levelUpText.text = "";

        UnitBtnUI();
        PrintTexts();
    }

    public void OnClickUnitSelect()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        GameObject clicked = EventSystem.current.currentSelectedGameObject; //선택한 버튼의 이름 불러오기

        string[] btnName = clicked.name.Split();
        nowUnitNumber = int.Parse(btnName[1]);

        PrintTexts();
        UnitBtnUI();

        if (GameManager.instance.printData[nowUnitNumber].level == 0)
        {
            lockUnit.SetActive(true);

            lockUnit.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = $"Unlock : {nowUnitNumber * 100}";
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

        UnitData nowUnit = GameManager.instance.printData[nowUnitNumber];
        int cost = 100;
        int level = nowUnit.level;

        for (int i = 0; i < level - 1; i++)
        {
            cost = (int)((cost + 100) * 1.15f);
        }
        if (GameManager.instance.Gold >= cost)
        {
            SoundManager.instance.PlaySFX((SFXType)9);
            GameManager.instance.UseGold(cost);

            nowUnit.level++;
            nowUnit.maxHP += (nowUnit.maxHP) / 10;
            nowUnit.damage += (nowUnit.damage) / 10;

            GameObject lvE = Instantiate(levelUpEffect, new Vector2(lockUnit.transform.position.x, lockUnit.transform.position.y), Quaternion.identity);
            lvE.transform.localScale = new Vector3(2, 2, 2);
            Destroy(lvE, 1);

            GameManager.instance.printData[nowUnitNumber] = nowUnit;
            PrintTexts();
        }
    }

    public void OnClickUnlockUnit()
    {
        if (nowUnitNumber == -1)
            return;

        if (GameManager.instance.printData[nowUnitNumber].level == 0)
        {
            if (GameManager.instance.Gold >= nowUnitNumber * 100)
            {
                GameManager.instance.printData[nowUnitNumber].level++;
                GameManager.instance.UseGold(nowUnitNumber * 100);
                lockUnit.SetActive(false);
                UnitBtnUI();
                PrintTexts();

                GameObject oe = Instantiate(openEffect, new Vector2(lockUnit.transform.position.x, lockUnit.transform.position.y), Quaternion.identity);
                Destroy(oe, 1);
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
