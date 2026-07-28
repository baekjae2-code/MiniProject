using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class DeckSetting : MonoBehaviour
{
    [SerializeField] GameObject[] units;    // 이미지 프리팹

    [SerializeField] GameObject[] deckSlots;// Deckslots 0~4

    [SerializeField] Transform deckCostText;// 비용 텍스트
    [SerializeField] Canvas canvas;

    GameObject startSlot;
    GameObject dragCard;


    private void Start()
    {
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
            {
                if (deckSlots[i].transform.GetChild(0).name == unitNumber.ToString())   // 이미 덱에 있을시 제거
                {
                    Destroy(deckSlots[i].transform.GetChild(0).gameObject);
                    GameManager.instance.deckUnitNumber[i] = -1;
                    return;
                }
            }
        }

        if (GameManager.instance.printData[unitNumber].level == 0)  //레벨이 0이면 덱에 저장 불가
            return;

        for (int i = 0; i < deckSlots.Length; i++)
        {
            if (deckSlots[i].transform.childCount == 0)
            {
                Transform unit = Instantiate(units[unitNumber].transform);
                unit.name = unitNumber.ToString();
                unit.SetParent(deckSlots[i].transform);
                unit.position = deckSlots[i].transform.position;
                unit.localScale = new Vector3(unit.transform.localScale.x / 100f, unit.transform.localScale.y / 100, unit.transform.localScale.z / 100);

                Transform costText = Instantiate(deckCostText);
                costText.SetParent(unit);
                costText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.printData[unitNumber].mana.ToString();
                costText.position = unit.position + Vector3.right * 0.5f + Vector3.down * 0.3f;
                costText.localScale = new Vector3(costText.transform.localScale.x / 100f, costText.transform.localScale.y / 100, costText.transform.localScale.z / 100);
                costText.gameObject.SetActive(true);

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

        if (deckSlots[deckNumber].transform.childCount != 0)
        {
            Destroy(deckSlots[deckNumber].transform.GetChild(0).gameObject);
            GameManager.instance.deckUnitNumber[deckNumber] = -1;
        }
    }
    public void OnRemoveAllDeckSlot()   //초기화 버튼용
    {
        for (int i = 0; i < 5; i++)
        {
            if (deckSlots[i].transform.childCount == 1)
                Destroy(deckSlots[i].transform.GetChild(0).gameObject);
            GameManager.instance.deckUnitNumber[i] = -1;
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
                unit.localScale = new Vector3(unit.transform.localScale.x / 100f, unit.transform.localScale.y / 100, unit.transform.localScale.z / 100);

                Transform costText = Instantiate(deckCostText);
                costText.SetParent(unit);
                costText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.printData[GameManager.instance.deckUnitNumber[i]].mana.ToString();
                costText.position = unit.position + Vector3.right * 0.5f + Vector3.down * 0.3f;
                costText.localScale = new Vector3(costText.transform.localScale.x / 100f, costText.transform.localScale.y / 100, costText.transform.localScale.z / 100);
                costText.gameObject.SetActive(true);
            }
        }
    }
    public void ChangeDeckSlot1(BaseEventData data) //마우스 포인터 덱 누른 순간
    {
        PointerEventData eventData = (PointerEventData)data;

        startSlot = eventData.pointerCurrentRaycast.gameObject;

        if (startSlot.transform.childCount > 0)
        {
            dragCard = startSlot.transform.GetChild(0).gameObject;
            dragCard.transform.SetParent(canvas.transform);
            dragCard.transform.SetAsLastSibling();
        }
    }

    public void ChangeDeckSlot2(BaseEventData data) //덱에 마우스 포인터 뗀 순간
    {
        PointerEventData eventData = (PointerEventData)data;

        GameObject endSlot = eventData.pointerCurrentRaycast.gameObject;

        if (dragCard == null)   //빈공간에서 빈공간 드래그할때 에러
            return;
        if (endSlot == null || startSlot == null)   //endslot이 화면 바깥 다른 객체일때 에러
        {
            if (dragCard != null)
            {
                dragCard.transform.SetParent(startSlot.transform);
                dragCard.transform.localPosition = Vector3.zero;
                dragCard = null;
            }
            print("다른 객체");
            return;
        }
        if (startSlot.name.Split()[0] != "DeckSlot" || endSlot.name.Split()[0] != "DeckSlot")
        {
            if (dragCard != null)
            {
                dragCard.transform.SetParent(startSlot.transform);
                dragCard.transform.localPosition = Vector3.zero;
                dragCard = null;
            }
            print("다른 이름");
            return;
        }
        if (endSlot == startSlot)    //OnRemoveDeckSlot() 덱 삭제 안되는 상황 있음(드래그 판정이라 그런듯)
        {
            if (dragCard != null)
            {
                Destroy(dragCard.gameObject);
                GameManager.instance.deckUnitNumber[int.Parse(startSlot.name.Split()[1])] = -1;
            }
            return;
        }

        int startSlotName1 = int.Parse(startSlot.name.Split()[1]);
        int startSlotName2 = int.Parse(endSlot.name.Split()[1]);
        if (endSlot.transform.childCount != 0)
        {
            GameObject endCard = endSlot.transform.GetChild(0).gameObject;

            endCard.transform.SetParent(startSlot.transform);
            dragCard.transform.SetParent(endSlot.transform);        //둘의 부모 위치 교체
            endCard.transform.localPosition = Vector3.zero;
            dragCard.transform.localPosition = Vector3.zero;
            GameManager.instance.deckUnitNumber[startSlotName1] = int.Parse(endCard.name);
            GameManager.instance.deckUnitNumber[startSlotName2] = int.Parse(dragCard.name);
            print("자식 있음");
        }
        else
        {
            dragCard.transform.SetParent(endSlot.transform);        //둘의 부모 위치 교체
            dragCard.transform.localPosition = Vector3.zero;
            GameManager.instance.deckUnitNumber[startSlotName1] = -1;
            GameManager.instance.deckUnitNumber[startSlotName2] = int.Parse(dragCard.name);
            print("자식 없음");
        }
        print("통과");
        Debug.Log($"{startSlot.name} ↔ {endSlot.name} 교체");

        dragCard = null;
        startSlot = null;//맵 밖 객체에서 드래그한 상태로 안으로 들어올때 에러
    }
    public void OnDeckSlotDrag()
    {
        if (dragCard == null) return;

        Vector3 pos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        pos.z = dragCard.transform.position.z;

        dragCard.transform.position = pos;
    }
}

