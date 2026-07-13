using UnityEngine;
using UnityEngine.EventSystems;

public class DeckSetting : MonoBehaviour
{
    [SerializeField] GameObject[] units;

    [SerializeField] GameObject[] deckSlots;
    bool[] isSetting;

    private void Start()
    {
        isSetting = new bool[deckSlots.Length];
    }

    public void OnAddDeckSlot()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject; //선택한 버튼의 이름 불러오기

        string[] btnName = clicked.name.Split();
        int unitNumber = int.Parse(btnName[1]);

        for (int i = 0; i < deckSlots.Length; i++)
        {
            if (deckSlots[i].transform.childCount != 0) //자식이 있고 이름이 중복되면 등록 X
                if (deckSlots[i].transform.GetChild(0).name == unitNumber.ToString())
                    return;
        }

        for (int i = 0; i < deckSlots.Length; i++)
        {
            if (!isSetting[i])
            {
                GameObject unit = Instantiate(units[unitNumber]);   
                unit.name = unitNumber.ToString();
                unit.transform.SetParent(deckSlots[i].transform);
                unit.transform.position = deckSlots[i].transform.position;
                isSetting[i] = true;

                GameManager.instance.deckUnitNumber[i] = int.Parse(unit.name);

                return;
            }
        }
    }

    public void OnRemoveDeckSlot()
    {
        GameObject clicked = EventSystem.current.currentSelectedGameObject; //선택한 버튼의 이름 불러오기

        string[] btnName = clicked.name.Split();
        int deckNumber = int.Parse(btnName[1]);

        if (isSetting[deckNumber])
        {
            Destroy(deckSlots[deckNumber].transform.GetChild(0).gameObject);
            GameManager.instance.deckUnitNumber[deckNumber] = -1;
            isSetting[deckNumber] = false;
        }
    }
}

