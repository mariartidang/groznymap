using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class Marker : MonoBehaviour
{
    [SerializeField] RawImage rawImage;

    [SerializeField] Vector3 offset;

    public Building building;

    Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = mainCamera.WorldToScreenPoint(building.transform.position + offset);

        float distance = Vector3.Distance(building.transform.position, mainCamera.transform.position);

        if (distance > MarkersManager.Instance.maxMarkerVisibleDistance)
        {
            rawImage.color = Color.Lerp(rawImage.color, Color.clear, 12f * Time.deltaTime);
        }
        else
        {
            rawImage.color = Color.Lerp(rawImage.color, Color.white, 12f * Time.deltaTime);
        }
    }

    public void InitializeMarker(Building building)
    {
        this.building = building;
        rawImage.texture = MarkersManager.Instance.GetBuildingImageByType(building.BuildingData.Type);
    }
}
