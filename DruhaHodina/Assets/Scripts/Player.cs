using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour
{
    [SerializeField] CharacterController characterController;
    [SerializeField] Transform camera;
    [SerializeField] Transform playerObject;
    [SerializeField] float lookSpeed = 10;
    [SerializeField] float moveSpeed = 10;
    [SerializeField] float gravity;
    [SerializeField] GameObject turret;
    [SerializeField] GameObject ball;

    [SerializeField] float jumpIntensity;
    float yVelocity = 0f;

    float downwardsVelocity = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMovement();

        if (Input.GetMouseButtonDown(0))
        {
            BuildTurret();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            ball.transform.position = new Vector3(0,10,0);
        }
        
        
    }

    void BuildTurret()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, camera.forward, out hit, 1000f))
        {
            Vector3 up = hit.normal.normalized;
            Vector3 forward = Vector3.Cross(camera.right, up).normalized;

            Debug.Log(Quaternion.Euler(camera.forward));

            Instantiate(turret, hit.point, Quaternion.LookRotation(forward, up));
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(camera.transform.position, camera.forward * 100);
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, camera.forward, out hit, 1000f))
        {
            Debug.DrawRay(camera.transform.position, camera.forward * hit.distance, Color.yellow);
            Gizmos.color = Color.black;
            Gizmos.DrawSphere(hit.point, 0.1f);
            
            Gizmos.color = Color.blue;
            Vector3 up = hit.normal.normalized;
            Gizmos.DrawRay(hit.point, up);

            Gizmos.color = Color.red;
            Vector3 right = Vector3.Cross(up, camera.forward);
            Gizmos.DrawRay(hit.point, right.normalized);

            Gizmos.color = Color.green;
            Vector3 forward = Vector3.Cross(camera.right, up);
            Gizmos.DrawRay(hit.point, forward.normalized);

        }
    }

    void PlayerMovement()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        camera.eulerAngles += new Vector3(-mouseY, 0, 0) * lookSpeed;
        playerObject.eulerAngles += new Vector3(0, mouseX, 0) * lookSpeed;
        
        Vector3 movementDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.D)) movementDirection += playerObject.right;
        if (Input.GetKey(KeyCode.A)) movementDirection -= playerObject.right;
        if (Input.GetKey(KeyCode.W)) movementDirection += playerObject.forward;
        if (Input.GetKey(KeyCode.S)) movementDirection -= playerObject.forward;

        if (characterController.isGrounded)
            yVelocity = 0;
        
        if (Input.GetKey(KeyCode.Space) && characterController.isGrounded)
        {
            yVelocity = jumpIntensity;
            Debug.Log("test");
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementDirection *= 2;
        }

        yVelocity += gravity * Time.deltaTime;
        movementDirection.y = yVelocity;

        movementDirection.Normalize();
        movementDirection *= moveSpeed;
        characterController.Move(movementDirection * Time.deltaTime);
    }
}
