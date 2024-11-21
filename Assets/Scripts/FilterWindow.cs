using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FilterWindow : MonoBehaviour
{
    public static FilterWindow Instance;

    public FilterSettings ActiveSettings;

    public static bool isExpanded;

    Animation animationComponent;

    private void Awake()
    {
        Instance = this;

        animationComponent = GetComponent<Animation>();

       // ActiveSettings = new FilterSettings();
        ActiveSettings.Tags = new List<string>();
    }

    private void Start()
    {

    }

    public void SwitchToAdvancedSettings()
    {
        isExpanded = !isExpanded;
        animationComponent.Play(!isExpanded ? "FilterHide" : "FilterShow");
    }

    public void SetVisitorsAmount(Slider slider)
    {
        int option = (int)slider.value;

        TextMeshProUGUI val = slider.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        val.SetText(option.ToString());

        ActiveSettings.VisitorsAmount = option;

        RoutesWindow.Instance.Filter();
    }

    public void SetAvailableTime(Slider slider)
    {
        int option = (int)slider.value;

        TextMeshProUGUI val = slider.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (option == 0)
        {
            val.SetText("Меньше 30 ч.");
            ActiveSettings.AvailableTime = 29;
        }
        else if (option == 1)
        {
            val.SetText("Меньше часа");
            ActiveSettings.AvailableTime = 59;
        }
        else if (option == 2)
        {
            val.SetText("Меньше 2 ч.");
            ActiveSettings.AvailableTime = 119;
        }
        else if (option == 3)
        {
            val.SetText("Меньше 5 ч.");
            ActiveSettings.AvailableTime = 299;
        }
        else if (option == 4)
        {
            val.SetText("Неограничено");
            ActiveSettings.AvailableTime = 9999;
        }

        //ActiveSettings.AvailableTime = option;

        RoutesWindow.Instance.Filter();
    }

    public void SetBudget(Slider slider)
    {
        int option = (int)slider.value;

        TextMeshProUGUI val = slider.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (option == 0)
            val.SetText("Нет");
        else if (option == 1)
            val.SetText("Малый");
        else if (option == 2)
            val.SetText("Средний");
        else if (option == 3)
            val.SetText("Большой");
        else if (option == 4)
            val.SetText("Неограничен");

        ActiveSettings.Budget = option;

        RoutesWindow.Instance.Filter();
    }

    public void SetAge(Slider slider)
    {
        int option = (int)slider.value;

        TextMeshProUGUI val = slider.transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        if (option == 0)
        {
            val.SetText("Меньше 12");
            ActiveSettings.Age = 11;
        }
        else if (option == 1)
        {
            val.SetText("Меньше 18");
            ActiveSettings.Age = 17;
        }
        else if (option == 2)
        {
            val.SetText("Меньше 24");
            ActiveSettings.Age = 23;
        }
        else if (option == 3)
        {
            val.SetText("Меньше 40");
            ActiveSettings.Age = 39;
        }
        else if (option == 4)
        {
            val.SetText("Меньше 100");
            ActiveSettings.Age = 99;
        }

        //tiveSettings.Age = option;

        RoutesWindow.Instance.Filter();
    }

    public void SetTransportPreferences(TMPro.TMP_Dropdown dropdown)
    {
        ActiveSettings.TransportPreferences = (int)dropdown.value;
        RoutesWindow.Instance.Filter();
    }

    public void SetAreaPreferences(TMPro.TMP_Dropdown dropdown)
    {
        ActiveSettings.AreaPreferences = (int)dropdown.value;
        RoutesWindow.Instance.Filter();
    }

    public void SetVHSStatus(TMPro.TMP_Dropdown dropdown)
    {
        ActiveSettings.IsForHVS = (int)dropdown.value;
        RoutesWindow.Instance.Filter();
    }

    public void SetTag(string tag)
    {
        if (ActiveSettings.Tags.Contains(tag))
        {
            ActiveSettings.Tags.Remove(tag);
            RoutesWindow.Instance.Filter();
            return;
        }

        ActiveSettings.Tags.Add(tag);

        RoutesWindow.Instance.Filter();
    }

    public void ResetTags()
    {
        ActiveSettings.Tags.Clear();
        RoutesWindow.Instance.Filter();
    }
}

[System.Serializable]
public class FilterSettings
{
    public int VisitorsAmount;
    public int AvailableTime;
    public int Budget;
    public int Age;

    public int TransportPreferences;
    public int AreaPreferences;
    public int IsForHVS;

    public List<string> Tags;
}
