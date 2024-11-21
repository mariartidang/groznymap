using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Building : MonoBehaviour
{
    public BuildingData BuildingData => buildingData;
    public Transform PointTransform => pointTransform;


    [SerializeField] BuildingData buildingData;
    [SerializeField] Transform pointTransform;

    Coroutine interactionCoroutine;
    Vector3 startScale;

    private void Awake()
    {
        if (pointTransform == null)
            pointTransform = transform.GetComponentsInChildren<Transform>().Single(x => x.name.ToLower().Contains("point"));

        startScale = transform.localScale;
    }

    public void Initialize(BuildingData buildingData)
    {
        this.buildingData = buildingData;
    }

    private void OnMouseDown()
    {
        InfoPanel.Instance.ShowPanel(this);
    }

    private void OnMouseEnter()
    {
        if (interactionCoroutine != null) StopCoroutine(interactionCoroutine);

        interactionCoroutine = StartCoroutine(LerpScale(1.1f));
    }

    private void OnMouseExit()
    {
        if (interactionCoroutine != null) StopCoroutine(interactionCoroutine);

        interactionCoroutine = StartCoroutine(LerpScale(1f));
    }

    IEnumerator LerpScale(float size)
    {
        var targetScale = startScale * size;

        while (transform.localScale != targetScale)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, 12f * Time.deltaTime);
            yield return null;
        }

        transform.localScale = targetScale;
    }

}

[System.Serializable]
public class BuildingData
{
    public string Type;

    public string Name;
    public string Description;
    public int Id;
}