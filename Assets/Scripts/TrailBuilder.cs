using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailBuilder : MonoBehaviour
{
    public static TrailBuilder Instance { get; private set; }

    [SerializeField] GameObject trailSegmentPreafb;
    [SerializeField] float distance;

    Vector3 previousFramePosition;
    Vector3 previousPosition;

    List<GameObject> segmentsPool;

    private void Awake()
    {
        Instance = this;

        segmentsPool = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        previousPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, previousPosition) > distance)
        {
            var segment = GetFreeSegment();

            Vector3 direction = transform.position - previousFramePosition;

            segment.transform.SetPositionAndRotation(transform.position, Quaternion.LookRotation(direction));
            segment.SetActive(true);

            previousPosition = transform.position;
        }
        previousFramePosition = transform.position;
    }

    GameObject GetFreeSegment()
    {
        for (int i = 0; i < segmentsPool.Count; i++)
            if (!segmentsPool[i].activeSelf)
                return segmentsPool[i];

        var segment = Instantiate(trailSegmentPreafb);
        segmentsPool.Add(segment);
        return segment;
    }

    public void ClearTrail()
    {
        for (int i = 0; i < segmentsPool.Count; i++)
            if (segmentsPool[i].activeSelf)
                segmentsPool[i].SetActive(false);
    }
}
