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

    [SerializeField] float jumpIntensity;
    float yVelocity = 0f;

    float downwardsVelocity = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
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
