using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedNormal;
    public float speed;
    public float speedCrouched;
    public float gravity = 20.0F;
    public float jumpForce;
    public bool isFrozen;
    public LayerMask layerMask;
    bool onTramp = false;
    // Drag & Drop the camera in this field, in the inspector
    public Transform cameraTransform;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, Mathf.Infinity, layerMask))
        {
            jumpForce = 3;
        }
        else
        {
            jumpForce = 1.5f;
        }

        if (controller.isGrounded && !isFrozen)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), jumpForce, Input.GetAxis("Vertical"));
                moveDirection = cameraTransform.TransformDirection(moveDirection);
                moveDirection *= speed;
            }
            else
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = cameraTransform.TransformDirection(moveDirection);
                moveDirection *= speed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
