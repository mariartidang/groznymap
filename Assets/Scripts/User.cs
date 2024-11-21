using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class User : MonoBehaviour
{
    public static User Instance { get; private set; }

    LineRenderer lineRenderer;

    NavMeshAgent agent;
    NavMeshPath path;

    Transform targetMarker;

    private void Awake()
    {
        Instance = this;

        lineRenderer = GetComponent<LineRenderer>();
        agent = GetComponent<NavMeshAgent>();

        path = new NavMeshPath();
    }

    // Start is called before the first frame update
    void Start()
    {
        targetMarker = PathBuilder.Instance.transform;

    }

    // Update is called once per frame
    void Update()
    {
        if (agent.CalculatePath(targetMarker.position, path))
        {
            lineRenderer.positionCount = path.corners.Length;
            lineRenderer.SetPositions(path.corners);
        }
    }
}
