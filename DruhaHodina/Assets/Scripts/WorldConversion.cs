using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldConversion : MonoBehaviour
{
    [SerializeField] Transform worldSpace;
    [SerializeField] Transform localSpace;
    [SerializeField] Transform localSpaceChild;
    void OnDrawGizmos()
    {
        DrawTransformHandle(worldSpace);
        DrawTransformHandle(localSpace);
        DrawTransformHandle(localSpaceChild);
        
        Gizmos.DrawSphere(localSpaceChild.position, 0.5f);

        //localSpaceChild.localPosition = WorldToLocal(worldSpace.position, localSpace);
        worldSpace.position = LocalToWorld(localSpaceChild.localPosition, localSpace);
    }

    Vector3 LocalToWorld(Vector3 localPosition, Transform space)
    {
        Vector3 worldOffset = space.right * localPosition.x +
                              space.up * localPosition.y +
                              space.forward * localPosition.z;
        return worldOffset + space.position;
    }

    Vector3 WorldToLocal(Vector3 position, Transform space)
    {
        Vector3 relative = position - space.position;

        float x = Vector3.Dot(relative, space.right);
        float y = Vector3.Dot(relative, space.up);
        float z = Vector3.Dot(relative, space.forward);


        return new Vector3(x, y,z);
    }

    void DrawTransformHandle(Transform tr)
    {
        Vector3 position = tr.position;
        Gizmos.color = Color.red;
        Gizmos.DrawRay(position, tr.right);
        
        Gizmos.color = Color.green;
        Gizmos.DrawRay(position, tr.up);
    }
}
