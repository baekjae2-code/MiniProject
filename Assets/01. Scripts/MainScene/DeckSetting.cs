using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class DeckSetting : MonoBehaviour
{
    [SerializeField] GameObject[] units;    // 이미지 프리팹

    [SerializeField] GameObject[] deckSlots;// Deckslots 0~4
    bool[] isSetting;

    [SerializeField] Transform deckCostText;// 비용 텍스트

    private void Start()
    {
        isSetting = new bool[deckSlots.Length];

        units = new GameObject[GameManager.instance.unitsImg.Length];
        for (int i = 0; i < GameManager.instance.unitsImg.Length; i++)
        {
            units[i] = GameManager.instance.unitsImg[i];
        }
        GameManager.instance.LoadDeckData();
        LoadDeckSlot();
    }

    public void OnAddDeckSlot()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
        GameObject clicked = EventSystem.current.currentSelectedGameObject; //선택한 버튼의 이름 불러오기

        string[] btnName = clicked.name.Split();
        int unitNumber = int.Parse(btnName[1]);

        for (int i = 0; i < deckSlots.Length; i++)
        {
            if (deckSlots[i].transform.childCount != 0) //자식이 있고 이름이 중복되면 등록 X
                if (deckSlots[i].transform.GetChild(0).name == unitNumber.ToString())
                    return;
        }

        if (GameManager.instance.printData[unitNumber].level == 0)  //레벨이 0이면 덱에 저장 불가
            return;

        for (int i = 0; i < deckSlots.Length; i++)
        {
            if (!isSetting[i])
            {
                Transform unit = Instantiate(units[unitNumber].transform);
                unit.name = unitNumber.ToString();
                unit.SetParent(deckSlots[i].transform);
                unit.position = deckSlots[i].transform.position;
                unit.localScale = new Vector3(unit.transform.localScale.x / 100f, unit.transform.localScale.y / 100, unit.transform.localScale.z / 100);

                Transform costText = Instantiate(deckCostText);
                costText.SetParent(unit);
                costText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.printData[unitNumber].mana.ToString();
                costText.position = unit.position + Vector3.right * 0.3f + Vector3.down * 0.3f;
                costText.localScale = new Vector3(costText.transform.localScale.x / 100f, costText.transform.localScale.y / 100, costText.transform.localScale.z / 100);
                costText.gameObject.SetActive(true);

                isSetting[i] = true;
                GameManager.instance.deckUnitNumber[i] = int.Parse(unit.name);

                break;
            }
        }

        GameManager.instance.SaveDeckData();
    }

    public void OnRemoveDeckSlot()
    {
        SoundManager.instance.PlaySFX((SFXType)7);
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
    public void OnRemoveAllDeckSlot()   //초기화 버튼용
    {
        for (int i = 0; i < 5; i++)
        {
            if (deckSlots[i].transform.childCount == 1)
                Destroy(deckSlots[i].transform.GetChild(0).gameObject);
            GameManager.instance.deckUnitNumber[i] = -1;
            isSetting[i] = false;
        }
    }
    void LoadDeckSlot() // 저장한 덱을 불러오기
    {
        for (int i = 0; i < deckSlots.Length; i++)
        {
            if (GameManager.instance.deckUnitNumber[i] != -1)
            {
                Transform unit = Instantiate(units[GameManager.instance.deckUnitNumber[i]].transform);
                unit.name = GameManager.instance.deckUnitNumber[i].ToString();
                unit.SetParent(deckSlots[i].transform);
                unit.position = deckSlots[i].transform.position;
                unit.localScale = new Vector3(unit.transform.localScale.x/100f, unit.transform.localScale.y/100, unit.transform.localScale.z/100);

                Transform costText = Instantiate(deckCostText);
                costText.SetParent(unit);
                costText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.printData[GameManager.instance.deckUnitNumber[i]].mana.ToString();
                costText.position = unit.position + Vector3.right * 0.3f + Vector3.down * 0.3f;
                costText.localScale = new Vector3(costText.transform.localScale.x / 100f, costText.transform.localScale.y / 100, costText.transform.localScale.z / 100);
                costText.gameObject.SetActive(true);

                isSetting[i] = true;
            }
        }
    }
}

