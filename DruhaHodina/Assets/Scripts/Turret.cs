using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] Transform barrel;
    Transform player;
    bool isLocked;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Ball").transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
            isLocked = !isLocked;
        
        
        if (isLocked)
            barrel.transform.LookAt(player);
    }
}
