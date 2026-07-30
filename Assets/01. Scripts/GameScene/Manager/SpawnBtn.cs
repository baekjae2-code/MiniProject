using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpawnBtn : MonoBehaviour
{
    int nowUnitNumber;

    public GameObject mySkill;
    public Image mySkillCooltimeImage;

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
    public void OnClickUseSkill()
    {
        if (BattleManager.instance.GetManaNow() > 2 && mySkillCooltimeImage.fillAmount == 0)
        {
            BattleManager.instance.UseMana(2);

            for (int i = 0; i < 3; i++)
            {
                GameObject u = ObjectPoolManager.instance.GetObject(mySkill.name);
                u.transform.position = new Vector3(-10f, -0.5f);
                ObjectPoolManager.instance.ReturnObject(mySkill.name, u, 10);
            }
            StartCoroutine(SkillCoolCoroutine());
        }
    }

    IEnumerator SkillCoolCoroutine()
    {
        float coolTIme = (0.01f / 2f);
        WaitForSeconds wait = new WaitForSeconds(0.01f);
        mySkillCooltimeImage.fillAmount = 1;
        while (mySkillCooltimeImage.fillAmount > 0)
        {
            mySkillCooltimeImage.fillAmount -= coolTIme;
            yield return wait;
        }
    }
}
