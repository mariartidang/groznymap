using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TagManager : MonoBehaviour
{
    public static TagManager Instance;

    List<string> tags;

    [SerializeField] TagFilter tagPrefab;
    [SerializeField] Transform contentRoot;

    private void Awake()
    {
        Instance = this;
        tags = new List<string>();
    }

    public void InitializeTags(List<Building> buildings)
    {
        for (int i = 0; i < buildings.Count; i++)
        {
            if (!tags.Contains(buildings[i].BuildingData.Type))
            {
                tags.Add(buildings[i].BuildingData.Type);
                Instantiate(tagPrefab, contentRoot).InitializeTag(buildings[i].BuildingData.Type);
            }

        }

    }
}
