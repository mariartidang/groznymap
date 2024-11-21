using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchManager : MonoBehaviour
{
    [SerializeField] GameObject searchPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSearchPanel()
    {
        searchPanel.SetActive(true);

        if (!PlacesWindow.Instance.isOpened)
            PlacesWindow.Instance.SwitchState();
    }

    public void HideSearchPanel()
    {
        searchPanel.SetActive(false);
    }
}
