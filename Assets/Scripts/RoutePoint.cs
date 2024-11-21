using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoutePoint : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI routePoint;
    [SerializeField] GameObject arrow;

    Building building;


    public void InitializePoint(Building building, bool isLastPoint = false)
    {
        this.building = building;

        routePoint.SetText(building.BuildingData.Name);

        arrow.SetActive(!isLastPoint);
    }
}
