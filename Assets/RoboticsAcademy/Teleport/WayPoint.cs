using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using UnityEngine.EventSystems;

// create a position in the scene that can be teleported to.
public class WayPoint : MonoBehaviour
{
    // Code from mixedrealityteleportsystem.
    public void teleportToPoint()
    {
        Vector3 targetPosition = gameObject.transform.position;
        Vector3 targetRotation = gameObject.transform.rotation.eulerAngles;

        float height = targetPosition.y;
        targetPosition -= CameraCache.Main.transform.position - MixedRealityPlayspace.Position;
        targetPosition.y = height;

        MixedRealityPlayspace.Position = targetPosition;
        MixedRealityPlayspace.RotateAround(
                    CameraCache.Main.transform.position,
                    Vector3.up,
                    targetRotation.y - CameraCache.Main.transform.eulerAngles.y);
    }
}
