using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System;
using System.Linq;

public class BuildingsManager : MonoBehaviour
{
    public static BuildingsManager Instance;

    public List<Building> Buildings;

    private void Awake()
    {
        Instance = this;

        Buildings.AddRange(FindObjectsOfType<Building>());

    }

    private void Start()
    {
        StartCoroutine(LoadPlacedData());
    }

    IEnumerator LoadPlacedData()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "Places.json")))
        {
            yield return webRequest.SendWebRequest();

            var jsonString = webRequest.downloadHandler.text;

            var content = JsonUtility.FromJson<BuildingDataList>(jsonString);

            for (int i = 0; i < Buildings.Count; i++)
            {
                var targetData = content.Buildings.SingleOrDefault(x => x.Id == Buildings[i].BuildingData.Id);

                if (targetData != null)
                    Buildings[i].Initialize(targetData);
            }

            MarkersManager.Instance.InitializeMarkers();

            ZoneLoadManager.Instance.GenerateData();
            PlacesWindow.Instance.LoadPlaces();
            TagManager.Instance.InitializeTags(Buildings);
            StartCoroutine(EventManager.Instance.InitializeEvents());
        }
    }
}

[Serializable]
public class BuildingDataList
{
    public List<BuildingData> Buildings;
}