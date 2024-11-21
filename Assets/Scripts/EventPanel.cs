using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;
using UnityEngine.EventSystems;
using System.Linq;

public class EventPanel : MonoBehaviour, IPointerDownHandler
{
    //EventData ev = new EventData();
    //ev.EventName = "Выставка-реконструкция «Терракотовая армия. Бессмертные воины Китая»";
    //    ev.Description = "Посетителей выставки-реконструкции ждут детально воссозданные в натуральную величину статуи терракотовых воинов и лошадей, реплики великолепных колесниц, оружия и предметов быта, а также галерея красочных костюмов. Всего на выставке представлено более 150 экспонатов.";
    //    ev.Date = "Ежедневно 10:00-22:22";
    //    ev.Cost = 600;
    //    ev.Type = "Выскавка";
    //    EventList el = new EventList();
    //el.events = new List<EventData>();
    //    el.events.Add(ev);
    //    el.events.Add(ev);
    //    string jsonStr = JsonUtility.ToJson(el);
    //System.IO.File.WriteAllText("E:/Events.json", jsonStr);

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
