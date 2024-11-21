using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class TagFilter : MonoBehaviour, IPointerDownHandler
{
    TextMeshProUGUI textComponent;

    bool isSelected;

    private void Awake()
    {
        textComponent = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isSelected = !isSelected;

        textComponent.color = isSelected ? Color.black : Color.white;

        FilterWindow.Instance.SetTag(textComponent.text);
    }

    public void InitializeTag(string text)
    {
        textComponent.SetText(text);
    }
}
