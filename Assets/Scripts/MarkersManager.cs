using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarkersManager : MonoBehaviour
{
    public static MarkersManager Instance;

    public float maxMarkerVisibleDistance;

    [SerializeField] Marker marker;

    [SerializeField] Texture2D defaultIcon;

    [SerializeField] Transform contentRoot;

    public List<TypeMarkerDependency> markers;


    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void InitializeMarkers()
    {
        var buildings = BuildingsManager.Instance.Buildings;

        for (int i = 0; i < buildings.Count; i++)
        {
            Instantiate(marker, contentRoot).InitializeMarker(buildings[i]);
        }
    }

    public Texture2D GetBuildingImageByType(string type)
    {
        for (int i = 0; i < markers.Count; i++)
        {
            if (markers[i].type == type)
                return markers[i].icon;
        }
        return defaultIcon;
    }
}

[System.Serializable]
public class TypeMarkerDependency
{
    public string type;
    public Texture2D icon;
}