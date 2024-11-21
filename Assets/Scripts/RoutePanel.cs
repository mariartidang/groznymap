using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoutePanel : MonoBehaviour, IPointerDownHandler
{
    public static RoutePanel Instance { get; private set; }

    [SerializeField] TextMeshProUGUI routeName;
    [SerializeField] TextMeshProUGUI routeDescription;

    [SerializeField] TextMeshProUGUI isOnTransport;
    [SerializeField] TextMeshProUGUI distance;
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] TextMeshProUGUI tags;

    Route route;

    List<Building> buildings;

    private void Awake()
    {
        buildings = new List<Building>();
    }

    public void InitializePanel(Route route)
    {
        this.route = route;

        routeDescription.SetText(route.RouteDescription);
        routeName.SetText(route.RouteName);

        isOnTransport.SetText(route.TransportStatus.ToString());
        points.SetText(route.Points.Length.ToString());
        time.SetText(route.TimeInMinutes.ToString());

        string finalString = "";

        for (int i = 0; i < route.Tags.Count; i++)
        {
            finalString += route.Tags[i] + " ";
        }

        tags.SetText(finalString);

        float totalDistance = 0f;

        for (int i = 0; i < route.Points.Length; i++)
        {
            var building = BuildingsManager.Instance.Buildings.SingleOrDefault(x => x.BuildingData.Id == route.Points[i]);

            if (building == null)
                continue;

            buildings.Add(building);
        }

        RoutesWindow.Instance.SetRoutePoints(buildings);

        for (int i = 0; i < buildings.Count - 1; i++)
        {
            totalDistance += Vector3.Distance(buildings[i].PointTransform.position, buildings[i + 1].PointTransform.position);
        }

        distance.SetText(totalDistance.ToString("0.00"));
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RouteBuilder.Instance.DrawPath(route);

        if (Instance != null)
            Instance.GetComponent<Image>().color = Color.white;

        GetComponent<Image>().color = Color.yellow;

        Instance = this;

        CameraTarget.Instance.LookAt(buildings[0].PointTransform.position);

       StartCoroutine(RoutesWindow.Instance.SetRoutePoints(buildings));
    }
}
