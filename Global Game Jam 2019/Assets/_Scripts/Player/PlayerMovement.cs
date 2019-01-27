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
    public int jumpPower;
    bool onTramp = false;
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

        if (Input.GetKeyDown(KeyCode.Space) && !onTramp)
        {
            Jump(1);
        }
        else if (Input.GetKeyDown(KeyCode.Space) && onTramp)
        {
            Jump(3);
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "T")
        {
            onTramp = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "T")
        {
            onTramp = false;
        }
    }

    private void Jump(float modifier)
    {
        Debug.Log("Jump");
        GetComponent<Rigidbody>().velocity = new Vector3(0f, jumpPower * modifier, 0f);
    }
}
