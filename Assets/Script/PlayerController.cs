using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [Header("Player Movement")]
    public float moveSpeed = 5.0f;
    public float jumpForce = 5.0f;

    [Header("Camera Settings")]
    public Camera firstPersonCamera;
    public Camera thirdPersonCamera;
    public float mouseSenesitivity = 2.0f;

    public float radius = 5.0f;
    public float minRadius = 1.0f;
    public float maxRadius = 10.0f;

    public float yMinLimit = -90;
    public float yMaxLimit = 90;

    private float theta = 0.0f;
    private float phi = 0.0f;
    private float targetVericalRotation = 0;
    private float verticalRotationSpeed = 240f;

    private bool isFirstPerson = true;
    private bool isGrounded;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        SetupCameras();
        SetActiveCamera();
    }

    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        firstPersonCamera.transform.localRotation = Quaternion.identity;
    }

    void SetActiveCamera()
    {
        firstPersonCamera.gameObject.SetActive(isFirstPerson);
        thirdPersonCamera.gameObject.SetActive(!isFirstPerson);
    }

    void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }

    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        if (!isFirstPerson)
        {
            Vector3 cameraForward = thirdPersonCamera.transform.forward;
            cameraForward.y = 0.0f;
            cameraForward.Normalize();

            Vector3 cameraRight = thirdPersonCamera.transform.right;
            cameraRight.y = 0.0f;
            cameraRight.Normalize();

            Vector3 movement = cameraRight * moveHorizontal + cameraForward * moveVertical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }
        else
        {

            Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
            rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
        }

        // Update is called once per frame
        void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleJump();
            HandleCameraToggle();

        }


        void HandleRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;

            theta += mouseX;
            theta = Mathf.Repeat(theta, 360.0f);

            targetVericalRotation -= mouseY;
            targetVericalRotation = Mathf.Clamp(targetVericalRotation, yMinLimit, yMaxLimit);
            phi = Mathf.MoveTowards(phi, targetVericalRotation, verticalRotationSpeed * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0.0f, theta, 0.0f);

            if (isFirstPerson)
            {

                float x = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Cos(Mathf.Deg2Rad * theta);
                float y = radius * Mathf.Cos(Mathf.Deg2Rad * phi);
                float z = radius * Mathf.Sin(Mathf.Deg2Rad * phi) * Mathf.Sin(Mathf.Deg2Rad * theta);

                thirdPersonCamera.transform.position = transform.position + new Vector3(x, y, z);
                thirdPersonCamera.transform.LookAt(transform);

                radius = Mathf.Clamp(radius - Input.GetAxis("Mouse ScrollWheel") * 5, minRadius, maxRadius);
            }
        }

        void HandleCameraToggle()
        {
            if(Input.GetKeyDown(KeyCode.C))
            {
                isFirstPerson = !isFirstPerson;
                SetActiveCamera();

            }
        }

            firstPersonCamera.transform.localRotation = Quaternion.Euler(phi, 0.0f, 0.0f);


        }
    }
