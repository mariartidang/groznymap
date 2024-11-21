using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.InteropServices;

public class PlacesWindow : MonoBehaviour
{
    public static PlacesWindow Instance;
    [SerializeField] LocationPanel locationPanelPrefab;

    [SerializeField] Transform panelsRoot;
    public bool isOpened;

    Animation animationComponent;

    public Transform canvasTransfrom;

    List<LocationPanel> panels = new List<LocationPanel>();

    [DllImport("__Internal")]
    private static extern void OpenNewTab(string url);

    public void openIt(string url)
    {
#if !UNITY_EDITOR && UNITY_WEBGL
             OpenNewTab(url);
#endif
    }

    private void Awake()
    {
        Instance = this;

        animationComponent = GetComponent<Animation>();
    }

    public void SwitchState()
    {
        isOpened = !isOpened;

        animationComponent.Play(!isOpened ? "RoutesWindowHide" : "RoutesWindowShow");

        if (isOpened)
        {
            if (RoutesWindow.Instance.isOpened)
                RoutesWindow.Instance.SwitchState();
            if (EventManager.Instance.isOpened)
                EventManager.Instance.SwitchState();
        }

    }

    public void Filter(TMPro.TMP_InputField filter)
    {
        for (int i = 0; i < panels.Count; i++)
            panels[i].gameObject.SetActive(true);

        if (string.IsNullOrEmpty(filter.text))
            return;
        else
        {
            for (int i = 0; i < panels.Count; i++)
                panels[i].gameObject.SetActive(false);

            for (int i = 0; i < panels.Count; i++)
            {
                if (panels[i].LocationName.ToLower().Contains(filter.text.ToLower()))
                    panels[i].gameObject.SetActive(true);
            }
        }
    }

    public void LoadPlaces()
    {
        for (int i = 0; i < BuildingsManager.Instance.Buildings.Count; i++)
        {
            var panel = Instantiate(locationPanelPrefab, panelsRoot);
            panel.InitializePanel(BuildingsManager.Instance.Buildings[i]);
            panels.Add(panel);
        }
    }

    public void BuyTicket()
    {
        openIt("https://www.teatrnuradilova.ru/afisha/");
    }
}
