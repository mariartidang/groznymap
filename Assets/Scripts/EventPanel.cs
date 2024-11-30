using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
using System.Linq;

public class EventPanel : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] TextMeshProUGUI eventName;
    [SerializeField] TextMeshProUGUI eventDesc;
    [SerializeField] TextMeshProUGUI date;
    [SerializeField] TextMeshProUGUI cost;
    [SerializeField] TextMeshProUGUI type;
    [SerializeField] TextMeshProUGUI location;

    EventData data;
    Building building;

    public void openIt(string url)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
             OpenNewTab(url);
#endif
    }

    public void InitializePanel(EventData eventData)
    {
        eventName.SetText(eventData.EventName);
        eventDesc.SetText(eventData.Description);
        date.SetText(eventData.Date);
        cost.SetText("От " + eventData.Cost);
        type.SetText(eventData.Type);

        data = eventData;

        building = BuildingsManager.Instance.Buildings.SingleOrDefault(x => x.BuildingData.Id == data.LocationID);

        if (building != null)
            location.SetText(building.BuildingData.Name);
    }

    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);

    public void BuyTicket()
    {
        openIt(data.Link);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (building != null)
            CameraTarget.Instance.LookAt(building.transform.position);
    }
}
