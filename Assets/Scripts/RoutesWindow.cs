using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Linq;
public class RoutesWindow : MonoBehaviour
{
    public static RoutesWindow Instance;

    [SerializeField] RoutePoint routePointPrefab;
    [SerializeField] RoutePanel routePanelPrefab;
    [SerializeField] RouteList routeList;

    [SerializeField] Transform routePanelContentRoot;
    [SerializeField] Transform routePointContentRoot;

    Animation animationComponent;

    List<Route> routes;

    public bool isOpened;

    private void Awake()
    {
        Instance = this;

        routes = new List<Route>();

        animationComponent = GetComponent<Animation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LoadRoutesData());
    }

    IEnumerator LoadRoutesData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "Routes.json")))
        {
            yield return webRequest.SendWebRequest();

            var jsonString = webRequest.downloadHandler.text;

            routeList = JsonUtility.FromJson<RouteList>(jsonString);

            for (int i = 0; i < routeList.Routes.Count; i++)
            {
                Instantiate(routePanelPrefab, routePanelContentRoot).InitializePanel(routeList.Routes[i]);
                routes.Add(routeList.Routes[i]);
            }
            //routeList.Routes[0].Tags.Add("Спорт");
            //routeList.Routes[1].Tags.Add("Инфоцентр");
            //routeList.Routes[2].Tags.Add("Для мам и детей");
            //routeList.Routes[3].Tags.Add("Фастфуд");
            //routeList.Routes[3].Tags.Add("Еда");
            //routeList.Routes[3].Tags.Add("Туалет");
            //routeList.Routes[4].Tags.Add("Туалет");
            //routeList.Routes[4].Tags.Add("Пруд");
            //routeList.Routes[5].Tags.Add("Пруд");
            //routeList.Routes[5].Tags.Add("Сад");
            //routeList.Routes[5].Tags.Add("Архитектура");
            //routeList.Routes[6].Tags.Add("Рокета");
            //routeList.Routes[6].Tags.Add("Архитектура");


            //string jsn = JsonUtility.ToJson(routeList);
            //System.IO.File.WriteAllText(@"E:/testrer.json", jsn);
        }
    }

    public IEnumerator SetRoutePoints(List<Building> buildings)
    {
        StopAllCoroutines();

        for (int i = 0; i < routePointContentRoot.childCount; i++)
        {
            Destroy(routePointContentRoot.GetChild(i).gameObject);
        }

        for (int i = 0; i < buildings.Count; i++)
        {
            Instantiate(routePointPrefab, routePointContentRoot).InitializePoint(buildings[i], i == buildings.Count - 1 ? true : false);
            yield return new WaitForSeconds(0.12f);
        }

    }

    public void SwitchState()
    {
        isOpened = !isOpened;

        animationComponent.Play(!isOpened ? "RoutesWindowHide" : "RoutesWindowShow");

        if (isOpened)
        {
            if (PlacesWindow.Instance.isOpened)
                PlacesWindow.Instance.SwitchState();
            if (EventManager.Instance.isOpened)
                EventManager.Instance.SwitchState();
        }

    }

    public void Filter()
    {
        for (int i = 0; i < routePanelContentRoot.childCount; i++)
        {
            Destroy(routePanelContentRoot.GetChild(i).gameObject);
        }

        FilterSettings settings = FilterWindow.Instance.ActiveSettings;

        for (int i = 0; i < routes.Count; i++)
        {
            Route route = routes[i];

            if (route.MaxVisitorsAmount > settings.VisitorsAmount)
                if (route.MinimalAge < settings.Age)
                    if (route.TimeInMinutes < settings.AvailableTime)
                        if (route.BudgetStatus < settings.Budget)

                            if (CheckForExtendedPreferences(route))
                                if (CheckForTags(route))
                                    // return;


                                    //if (settings.Tags.Count > 0)
                                    //    if (route.Tags.Count > 0)
                                    //        if (!route.Tags.Any(settings.Tags.Contains))
                                    //            return;

                                    Instantiate(routePanelPrefab, routePanelContentRoot).InitializePanel(route);
        }
    }

    bool CheckForExtendedPreferences(Route route)
    {
        FilterSettings settings = FilterWindow.Instance.ActiveSettings;

        if (settings.TransportPreferences != 0)
        {
            if (route.TransportStatus != settings.TransportPreferences)
                return false;
        }

        if (settings.AreaPreferences != 0)
        {
            if (route.AreaStatus != settings.AreaPreferences)
                return false;
        }

        if (settings.IsForHVS != 0)
        {
            if (route.HVSSTatus != settings.IsForHVS)
                return false;
        }

        return true;
    }

    bool CheckForTags(Route route)
    {
        FilterSettings settings = FilterWindow.Instance.ActiveSettings;

        if (settings.Tags.Count > 0)
            if (route.Tags.Count > 0)
                return route.Tags.Intersect(settings.Tags).Count() > 0;

        return true;
    }
}

