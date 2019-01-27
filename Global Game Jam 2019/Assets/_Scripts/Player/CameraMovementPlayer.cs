using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovementPlayer : MonoBehaviour
{
    [SerializeField]
    private float speedVertical = 1f;
    [SerializeField]
    private float speedHorizontal = 1f;
    [SerializeField]
    private float crouchBy = 0.05f;
    [SerializeField]
    private float topHeightCamera = 1f;
    [SerializeField]
    private float lowestHeightCamera = 1f;
    [SerializeField]
    private float lowestRotationCamera = 1f;
    [SerializeField]
    private float topRotationCamera = 1f;


    public PlayerMovement playerMovement;

    public float yaw = 77.819f;
    public float pitch = 0.0f;
    private float cameraHeight = 0.0f;

    private CursorLockMode cursorState;

    private void Start()
    {
        cursorState = CursorLockMode.Locked;
        SetCursorState();
    }

    private void Update()
    {
        yaw += speedHorizontal * Input.GetAxis("Mouse X");
        pitch = Mathf.Clamp(pitch - speedVertical * Input.GetAxis("Mouse Y"), lowestRotationCamera, topRotationCamera);

        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorState = CursorLockMode.None;
            SetCursorState();
        }

        if (Input.GetMouseButtonDown(0))
        {
            cursorState = CursorLockMode.Locked;
            SetCursorState();
        }

        if (Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.LeftControl))
        {
            Crouch(true);
        }
        else
        {
            Crouch(false);
        }
    }

    void SetCursorState()
    {
        Cursor.lockState = cursorState;
        // Hide cursor when locking
        Cursor.visible = (CursorLockMode.Locked != cursorState);
    }

    private void Crouch(bool perform)
    {
        cameraHeight = transform.localPosition.y;
        if (perform)
        {
            if (cameraHeight > lowestHeightCamera)
            {
                playerMovement.speed = playerMovement.speedCrouched;
                cameraHeight -= crouchBy;
            }
        }
        else
        {
            if (cameraHeight < topHeightCamera)
            {
                cameraHeight += crouchBy;
                playerMovement.speed = playerMovement.speedNormal;
            }
        }
        transform.localPosition = new Vector3(transform.localPosition.x, cameraHeight, transform.localPosition.z);
    }
}
