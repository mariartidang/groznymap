using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class RouteBuilder : MonoBehaviour
{
    public static RouteBuilder Instance { get; private set; }

    [SerializeField] NavMeshAgent agent;

    Route activeRoute;
    bool isMoving;

    List<Building> routeBuildings;
    int currentPoint;
    NavMeshPath path;

    private void Awake()
    {
        Instance = this;
        routeBuildings = new List<Building>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isMoving) // Ensure movement logic only executes when moving
            return;

        // Check if the agent has reached its current destination
        if (!agent.pathPending && agent.remainingDistance < 1.1f)
        {
            // If the current point is the last one, stop moving
            if (currentPoint >= routeBuildings.Count)
            {
                print("Route completed.");
                isMoving = false; // Stop further movement
                return;
            }

            // Move to the next point
            print($"Moving to point {currentPoint + 1} of {routeBuildings.Count}");
            agent.SetDestination(routeBuildings[currentPoint].PointTransform.position);
            currentPoint++;
        }
    }

    public void DrawPath(Route route)
    {
        TrailBuilder.Instance.ClearTrail();

        // Reset the route and state
        currentPoint = 0;
        activeRoute = route;
        routeBuildings.Clear();

        // Populate route buildings
        var buildings = BuildingsManager.Instance.Buildings;
        foreach (int pointId in activeRoute.Points)
        {
            var building = buildings.SingleOrDefault(x => x.BuildingData.Id == pointId);
            if (building != null)
                routeBuildings.Add(building);
        }

        if (routeBuildings.Count == 0)
        {
            print("No valid buildings found for the route.");
            return;
        }

        print($"{routeBuildings.Count} points in the route.");

        // Start at the first building
        agent.Warp(routeBuildings[0].PointTransform.position);

        // Set destination to the first point
        if (routeBuildings.Count > 1)
        {
            agent.SetDestination(routeBuildings[1].PointTransform.position);
            currentPoint = 1; // Start moving to the second point
            isMoving = true;  // Begin movement
        }
        else
        {
            print("Only one point in the route. No movement needed.");
            isMoving = false;
        }
    }
}
