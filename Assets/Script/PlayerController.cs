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
    public float mouseSenesitivity = 2.0f;

    public float yMinLimit = -90;
    public float yMaxLimit = 90;

    private float theta = 0.0f;
    private float phi = 0.0f;
    private float targetVericalRotation = 0;
    private float verticalRotationSpeed = 240f;


    private bool isGrounded;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    
    void HandleJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce,ForceMode. Impulse);
            isGrounded = false;
        }
        
    }

    void HandleMovement()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveHorizontal + transform.forward * moveVertical;
        rb.MovePosition(rb.position + movement * moveSpeed * Time.deltaTime);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleJump();
        
    }

    void SetupCameras()
    {
        firstPersonCamera.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
        firstPersonCamera.transform.localRotation = Quaternion.identity;
    }

   void HandleRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSenesitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSenesitivity;

        theta += mouseX;
        theta = Mathf.Repeat(theta, 360.0f);

        targetVericalRotation -= mouseY;
        targetVericalRotation = Mathf.Clamp(targetVericalRotation, yMinLimit, yMaxLimit);
        phi = Mathf.MoveTowards(phi, target)


    }
}
