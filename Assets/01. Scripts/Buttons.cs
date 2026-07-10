using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public RectTransform label;

    Vector2 original;

    void Start()
    {
        label = transform.GetChild(0).GetComponent<RectTransform>();
        original = label.anchoredPosition;
    }

    public void Press()
    {
        label.anchoredPosition = original - Vector2.up * 15;
    }

    public void Release()
    {
        label.anchoredPosition = original;
    }

}
