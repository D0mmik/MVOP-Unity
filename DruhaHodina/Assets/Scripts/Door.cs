using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] Transform camera;
    [SerializeField] Transform Model;

    [SerializeField] Transform openReference;
    [SerializeField] Transform closedReference;

    void Start()
    {
        Model.position = closedReference.position;
    }

    void Update()
    {
        Vector3 lookDirection = camera.forward;
        lookDirection.y = 0;
        lookDirection.Normalize();
        
        Vector3 playerToDoorDirection = transform.position - camera.position;
        playerToDoorDirection.y = 0;
        playerToDoorDirection.Normalize();

        Model.position = Vector3.Dot(lookDirection, playerToDoorDirection) < -0.8
            ? openReference.position
            : closedReference.position;
    }

    void OnDrawGizmos()
    {
        Vector3 lookDirection = camera.forward;
        lookDirection.y = 0;
        
        Vector3 playerToDoorDirection = transform.position - camera.position;
        playerToDoorDirection.y = 0;
        playerToDoorDirection.Normalize();

        Gizmos.color = Vector3.Dot(lookDirection, playerToDoorDirection) < -0.8 ? Color.green : Color.red;
        
        Gizmos.DrawRay(camera.position, lookDirection);
        Gizmos.DrawRay(camera.position, playerToDoorDirection);
    }
}
