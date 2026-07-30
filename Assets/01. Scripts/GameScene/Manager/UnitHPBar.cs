using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UnitHPBar : MonoBehaviour
{
    public GameObject myUnit;
    Unit unit;

    Slider mySlider;

    string[] unitName;
    float[] sliderPosition;

    float mySliderPosition;
    private void OnEnable()
    {
        mySlider ??= GetComponent<Slider>();
    }
    public void Init(GameObject unitObj)
    {
        myUnit = unitObj;
        unit = unitObj.GetComponent<Unit>();

        unitName = new string[] { "Melee", "Ranged", "Tank", "KingMelee", "Heal", "Magic", "Grab", "Fly", "Skill" };
        sliderPosition = new float[] { 1.2f, 1.2f, 1.4f, 2f, 1.4f, 1.1f, 1.7f, 1.0f, 0.9f };

        for (int i = 0; i < unitName.Length; i++)
        {
            if (myUnit.name.Split()[1] == unitName[i])
            {
                mySliderPosition = sliderPosition[i];
                break;
            }
        }
    }
    void LateUpdate()
    {
        if (myUnit == null || !myUnit.activeInHierarchy)
        {
            ObjectPoolManager.instance.ReturnObject(name.Split("(Clone)")[0], gameObject);
            unit = null;
            myUnit = null;
            return;
        }

        transform.position = Camera.main.WorldToScreenPoint(myUnit.transform.position + Vector3.up * mySliderPosition);
        mySlider.value = unit.nowHP / unit.maxHP;
    }
}