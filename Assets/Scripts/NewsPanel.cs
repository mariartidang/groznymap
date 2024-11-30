using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsPanel : MonoBehaviour
{
    // [SerializeField] RawImage picture;

    [SerializeField] TMP_Text title;
    [SerializeField] TMP_Text description;
    [SerializeField] TMP_Text date;

    NewsData data;

    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);

    public void openIt(string url)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
             OpenNewTab(url);
#endif
    }

    public void InitializePanel(NewsData newsData)
    {
        data = newsData;

        title.SetText(data.Title);
        description.SetText(data.Description);
        date.SetText(data.Date);
    }

    public void OpenLink()
    {
        openIt(data.Url);
    }

}

