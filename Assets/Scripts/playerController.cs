using UnityEngine;

public class playerController : MonoBehaviour
{
    public float movementSpeed = 5f;
    public float jumpForce = 5f;
    public float mouseSens = 100f;

    private Rigidbody playerRigidbody;
    private bool isJumping;
    private float xRotation;
    public Transform playerHead;

    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();


    }

    private void Update()
    {
        CameraMovement();
        PlayerMovement();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed;
        movement = gameObject.transform.TransformDirection(movement);

        playerRigidbody.velocity = new Vector3(movement.x, playerRigidbody.velocity.y, movement.z);

        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = true;
        }
    }

    private void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerHead.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);

    }
}
