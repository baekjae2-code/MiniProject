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
            deckUnit.position = deckButtons[i + 1].transform.position;

            Transform costText = Instantiate(deckCostText);
            costText.SetParent(deckUnit);
            costText.GetComponent<TextMeshProUGUI>().text = GameManager.instance.printData[GameManager.instance.deckUnitNumber[i]].mana.ToString();
            costText.position = deckUnit.position + Vector3.right * 30f + Vector3.down * 30f;
            costText.gameObject.SetActive(true);
        }
    }
}
