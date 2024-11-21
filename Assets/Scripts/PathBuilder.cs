using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PathBuilder : MonoBehaviour
{
    public static PathBuilder Instance { get; private set; }    

    [SerializeField] LayerMask floorLayerMask;

    NavMeshAgent agent;

    NavMeshPath startPath;

    private void Awake()
    {
        Instance = this;

        agent = GetComponent<NavMeshAgent>();  
    }

    // Start is called before the first frame vv
    IEnumerator Start()
    {
        startPath = new NavMeshPath();

        if (!Input.location.isEnabledByUser)
        {
            yield break;
        }

        Input.location.Start();

        int maxWait = 20;
        while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
        {
            yield return new WaitForSeconds(1);
            maxWait--;
        }

        // If the service didn't initialize in 20 seconds this cancels location service use.
        if (maxWait < 1)
        {
            print("Timed out");
            yield break;
        }

        if (Input.location.status == LocationServiceStatus.Failed)
        {
            print("Unable to determine device location");
            yield break;
        }
        else
        {
            // If the connection succeeded, this retrieves the device's current location and displays it in the Console window.
            print("Location: " + Input.location.lastData.latitude + " " + Input.location.lastData.longitude + " " + Input.location.lastData.altitude + " " + Input.location.lastData.horizontalAccuracy + " " + Input.location.lastData.timestamp);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit, 999f, floorLayerMask.value))
        {
            BuildPath(raycastHit.point);
        }
    }

    public void BuildPath(Vector3 targetPosition)
    {
        agent.CalculatePath(targetPosition, startPath);

        //pathIndicator.transform.position = agent.transform.position;

        if (startPath.status == NavMeshPathStatus.PathComplete)
        {
            TrailBuilder.Instance.ClearTrail();
            agent.Warp(User.Instance.transform.position);
            agent.SetDestination(startPath.corners[startPath.corners.Length - 1]);
        }
    }
}
