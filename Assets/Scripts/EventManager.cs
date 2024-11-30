using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;

    [SerializeField] EventPanel eventPanel;
    [SerializeField] Transform contentRoot;
    public bool isOpened;
    Animation animationComponent;

    private void Awake()
    {
        Instance = this;
        animationComponent = GetComponent<Animation>();
    }

    public void SwitchState()
    {
        isOpened = !isOpened;

        animationComponent.Play(!isOpened ? "RoutesWindowHide" : "RoutesWindowShow");


        if (isOpened)
        {
            if (RoutesWindow.Instance.isOpened)
                RoutesWindow.Instance.SwitchState();
            if (PlacesWindow.Instance.isOpened)
                PlacesWindow.Instance.SwitchState();
            if (NewsWindow.Instance.isOpened)
                NewsWindow.Instance.SwitchState();
        }

    }

    public IEnumerator InitializeEvents()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "Events.json")))
        {
            yield return webRequest.SendWebRequest();

            var jsonString = webRequest.downloadHandler.text;

            var content = JsonUtility.FromJson<EventList>(jsonString);

            for (int i = 0; i < content.events.Count; i++)
            {
                Instantiate(eventPanel, contentRoot).InitializePanel(content.events[i]);
            }

            MarkersManager.Instance.InitializeMarkers();
            //Route route = new Route();
            //route.Points = new int[] { 435, 409, 306, 343};

            //RouteBuilder.Instance.DrawPath(route);
            //ZoneLoadManager.Instance.GenerateData();
            //PlacesWindow.Instance.LoadPlaces();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}

[System.Serializable]
public class EventData
{
    public string EventName;
    public string Description;
    public string Date;
    public string Type;
    public int Cost;
    public int LocationID;
    public string Link;
}

[System.Serializable]
public class EventList
{
    public List<EventData> events;
}