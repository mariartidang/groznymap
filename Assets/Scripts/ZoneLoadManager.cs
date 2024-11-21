using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Linq;

public class ZoneLoadManager : MonoBehaviour
{
    public static ZoneLoadManager Instance;

    public SpriteRenderer zoneIndicatorPrefab;

    ZoneData zoneData;

    [SerializeField] ZoneLoadColorDependency zoneLoadColorDependency;

    [SerializeField] GameObject contentRoot;

    private void Awake()
    {
        Instance = this;
    }

    public void GenerateData()
    {
        StartCoroutine(Generate());
    }

    // Start is called before the first frame update
    IEnumerator Generate()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "ZoneLoad.json")))
        {
            yield return webRequest.SendWebRequest();

            var jsonString = webRequest.downloadHandler.text;

            zoneData = JsonUtility.FromJson<ZoneData>(jsonString);

            for (int i = 0; i < zoneData.zoneData.Count; i++)
            {
                var building = BuildingsManager.Instance.Buildings.SingleOrDefault(x => x.BuildingData.Id == zoneData.zoneData[i].zoneID);

                if (!building)
                    continue;

                var indicator = Instantiate(zoneIndicatorPrefab, building.transform.position, Quaternion.identity, contentRoot.transform);
                indicator.transform.eulerAngles = new Vector3(-90f, 0f, 0f);

                if (building.gameObject.TryGetComponent<BoxCollider>(out var collider))
                {
                    indicator.transform.localScale = Vector3.one * collider.bounds.size.magnitude * 0.32f;
                }


                indicator.color = zoneLoadColorDependency.loadStepColor[zoneData.zoneData[i].load];
            }

            contentRoot.SetActive(false);
        }
    }

    public void SwitchState()
    {
        contentRoot.SetActive(!contentRoot.activeSelf);
    }
}

[System.Serializable]
public class ZoneData
{
    public List<Zone> zoneData;
}

[System.Serializable]
public class Zone
{
    public int zoneID;
    public int load;
}

[System.Serializable]
public class ZoneLoadColorDependency
{
    public Color[] loadStepColor;
}

