using System.Linq;
using UnityEngine;

public class DeckPaste : MonoBehaviour
{
    public GameObject deckPanel;
    public Transform[] deckButtons;

    void Start()
    {
        deckButtons = deckPanel.transform.GetComponentsInChildren<Transform>(); //본인도 가져와서 아래 코드에서 1씩 뺌

        for (int i = 0; i < deckButtons.Length - 1; i++)
        {
            GameObject deckUnit = Instantiate(GameManager.instance.unitsImg[GameManager.instance.deckUnitNumber[i]]);
            deckUnit.transform.SetParent(deckButtons[i + 1].transform);
            deckUnit.transform.position = deckButtons[i + 1].transform.position;
        }
    }
}
