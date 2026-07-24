using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuildingHPBar : MonoBehaviour
{
    public GameObject myBuilding;
    Unit unit;
    Slider mySlider;
    TextMeshProUGUI myText;
    [SerializeField] bool moveHPBar;
    private void Start()
    {
        mySlider = GetComponent<Slider>();
        myText = GetComponentInChildren<TextMeshProUGUI>();
        unit = myBuilding.GetComponent<Unit>();
    }

    void LateUpdate()
    {
        if (myBuilding == null)
        {
            Destroy(gameObject);
            return;
        }
        if (moveHPBar)
            transform.position = Camera.main.WorldToScreenPoint(myBuilding.transform.position + Vector3.up * 2.5f);
        mySlider.value = unit.nowHP / unit.maxHP;
        myText.text = $"{(int)unit.nowHP} / {(int)unit.maxHP}";
    }
}