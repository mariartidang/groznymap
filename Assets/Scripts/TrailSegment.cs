using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSegment : MonoBehaviour
{
    [SerializeField] GameObject imagePrefab;

    Camera mainCamera;

    Transform segment;

    private void Awake()
    {
      
    }

    private void Start()
    {
        mainCamera = Camera.main;

        segment = Instantiate(imagePrefab, PlacesWindow.Instance.canvasTransfrom).transform;
    }

    private void Update()
    {
        segment.position = mainCamera.WorldToScreenPoint(transform.position);
    }

    private void OnEnable()
    {
        if (segment != null)
            segment.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        if (segment != null)
            segment.gameObject.SetActive(false);
    }
}
