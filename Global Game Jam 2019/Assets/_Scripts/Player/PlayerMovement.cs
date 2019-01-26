using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speedNormal;
    public float speed;
    public float speedCrouched;
    public float gravity = 20.0F;
    public bool isFrozen;
    // Drag & Drop the camera in this field, in the inspector
    public Transform cameraTransform;
    private Vector3 moveDirection = Vector3.zero;

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded && !isFrozen)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = cameraTransform.TransformDirection(moveDirection);
            moveDirection *= speed;
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
