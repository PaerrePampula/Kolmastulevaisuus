using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretCameraBehaviour : MonoBehaviour
{
    [SerializeField]
    float mouseSens;
    float pitch;
    float yaw;

    CharacterController characterController;
    public Transform playerTransform;
    public Transform playerVisual;

    // Start is called before the first frame update
    void Start()
    {
        characterController = playerTransform.GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        CameraRotate();
        LockStator();
    }

    void LockStator()
    {
        if (Input.GetKeyDown(KeyCode.F1))
            Cursor.lockState = CursorLockMode.None;
        if (Input.GetKeyDown(KeyCode.F2))
            Cursor.lockState = CursorLockMode.Locked;
    }

    void CameraRotate()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotAmountX = mouseX * mouseSens;
        float rotAmountY = mouseY * mouseSens;

        pitch += rotAmountX;
        yaw -= rotAmountY;
        yaw = Mathf.Clamp(yaw, -75, 75);

        playerTransform.transform.rotation = Quaternion.Euler(0, pitch, 0);
        playerVisual.transform.rotation = Quaternion.Euler(yaw, pitch, 0);
    }
}
