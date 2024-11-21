using System.Collections.Generic;

[System.Serializable]
public class Geometry
{
    public string type;
    public List<double> coordinates;
}

[System.Serializable]
public class Place
{
        public string type;
        public int id;
        public string order;
        public Geometry geometry;
        public Properties properties;
}
[System.Serializable]
public class Properties
{
        public string _icon;
        public string _order;
        public string _url;
        public string cat;
        public string color;
        public string color_code;
        public List<double> coordinates;
        public string icon;
        public int id;
        public int map_icon;
        public string map_title;
        public string order;
        public string pic;
        public string s_icon;
        public string show_title;
        public string time;
        public string title;
        public string title_s1;
        public string title_en;
        public string title_cn;
        public string type;
        public string type_s1;
        public string type_en;
        public string type_cn;
        public string url;
        public string visibility;
        public string tickets_link;
        public double zoom;
}
[System.Serializable]
public class Root
{
    public List<Place> Places;
}

[System.Serializable]
public class Route
{
    public string RouteName;
    public string RouteDescription;

    public int MaxVisitorsAmount;
    public int TimeInMinutes;
    public int BudgetStatus;
    public int MinimalAge;

    public int TransportStatus;
    public int AreaStatus;
    public int HVSSTatus;

    public List<string> Tags;

    public int[] Points;
}

[System.Serializable]
public class RouteList
{
    public List<Route> Routes;
}