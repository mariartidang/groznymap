using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class LocationPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] TextMeshProUGUI locationNameText;

    public string LocationName;

    Building building;

    public void InitializePanel(Building building)
    {
        this.building = building;
        LocationName = building.BuildingData.Name; 
        locationNameText.SetText(LocationName);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        CameraTarget.Instance.LookAt(building.transform.position);
    }
}
