using UnityEngine;

public struct BotInfo
{
    public Car Car;
    public RCC_AICarController AI;
    public LayerMask ObstacleLayers;
    public float ViewDistance;
    public int NextWaypointPassDistance;
    public float RaycastAngle;

    public void ResetAI()
    {
        AI.enabled = true;
        AI.currentWaypointIndex = 0;
        AI.totalWaypointPassed = 0;
    }
}