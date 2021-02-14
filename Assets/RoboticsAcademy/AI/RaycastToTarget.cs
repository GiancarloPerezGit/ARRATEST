using UnityEngine;

public static class RaycastToTarget
{
    public static GameObject GetTargetInfo(Vector3 origin, Vector3 direction, out Vector3 worldPos)
    {
        worldPos = Vector3.zero;
        RaycastHit hit;
        if (Physics.Raycast(origin - (direction * 0.1f), direction, out hit, Mathf.Infinity))
        {
            worldPos = hit.point;
            return hit.collider.gameObject;
        }
        else return null;
    }
}