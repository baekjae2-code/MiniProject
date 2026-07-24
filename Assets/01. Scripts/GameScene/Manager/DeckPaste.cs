using System.Linq;
using TMPro;
using UnityEngine;

public class DeckPaste : MonoBehaviour
{
    public GameObject deckPanel;
    public Transform[] deckButtons;

    public Transform deckCostText;

    void Start()
    {
        deckButtons = deckPanel.transform.GetComponentsInChildren<Transform>(); //본인도 가져와서 아래 코드에서 1씩 뺌

        for (int i = 0; i < deckButtons.Length - 1; i++)
        {
            Transform deckUnit = Instantiate(GameManager.instance.unitsImg[GameManager.instance.deckUnitNumber[i]].transform);
            deckUnit.SetParent(deckButtons[i + 1].transform);
            deckUnit.localPosition = Vector3.zero;
            int scaleX = 1;
            if (deckUnit.localScale.x < 0)
                scaleX = -1;
            deckUnit.localScale = new Vector3(scaleX, 1, 1);

            Transform costText = Instantiate(deckCostText);
            costText.SetParent(deckButtons[i + 1]);
            costText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.printData[GameManager.instance.deckUnitNumber[i]].mana.ToString();
            costText.GetComponent<RectTransform>().anchoredPosition = Vector3.left * 10f + Vector3.up * 30f;
            costText.localScale = Vector3.one;
            costText.gameObject.SetActive(true);
        }
    }
}
