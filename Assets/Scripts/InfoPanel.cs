using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    public static InfoPanel Instance;
    [SerializeField] TextMeshProUGUI type;
    [SerializeField] TextMeshProUGUI description;

    Building currentBuilding;
    Camera mainCamera;

    // Marker currentMarker;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.SetActive(false);
        mainCamera = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
        if (!currentBuilding)
            return;

        transform.position = mainCamera.WorldToScreenPoint(currentBuilding.transform.position);
    }

    public void ShowPanel(Building building)
    {
        currentBuilding = building;

        type.SetText(building.BuildingData.Name.ToString());
        description.SetText(building.BuildingData.Description);
        gameObject.SetActive(true);
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
