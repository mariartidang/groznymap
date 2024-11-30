using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System;
using JetBrains.Annotations;

public class NewsWindow : MonoBehaviour
{
    public static NewsWindow Instance;

    [SerializeField] NewsPanel newsPanel;
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

        animationComponent.Play(!isOpened ? "NewsWindowClose" : "NewsWindow");


        if (isOpened)
        {
            if (RoutesWindow.Instance.isOpened)
                RoutesWindow.Instance.SwitchState();
            if (PlacesWindow.Instance.isOpened)
                PlacesWindow.Instance.SwitchState();
            if (EventManager.Instance.isOpened)
                EventManager.Instance.SwitchState();
        }

    }

    public IEnumerator InitializeNews()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Path.Combine(Application.streamingAssetsPath, "News.json")))
        {
            yield return webRequest.SendWebRequest();

            var jsonString = webRequest.downloadHandler.text;

            var content = JsonUtility.FromJson<NewsList>(jsonString);

            for (int i = 0; i < content.NewsData.Count; i++)
            {
                Instantiate(newsPanel, contentRoot).InitializePanel(content.NewsData[i]);
            }

            MarkersManager.Instance.InitializeMarkers();

        }
    }
}

[Serializable]
public class NewsList
{
    public List<NewsData> NewsData;
}

[Serializable]
public class NewsData
{
    public string Title;
    public string Description;
    public string Url;
    public string Date;
}