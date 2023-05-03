using UnityEngine;

namespace Player
{
    public class PlayerController : MonoBehaviour
    {
        // Movement variables
        public float moveSpeed = 5f;
        public float jumpForce = 5f;
        private bool _isGrounded;

        // Camera variables
        public float mouseSensitivity = 100f;
        public Transform playerBody;

        private float _xRotation = 0f;
        private Rigidbody _rigidbody;

        // Start is called before the first frame update
        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Update is called once per frame
        private void Update()
        {
            // Camera rotation
            var mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            var mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            _xRotation -= mouseY;
            _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);

            // Movement
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            var transform1 = transform;
            var moveDirection = transform1.right * horizontal + transform1.forward * vertical;
            moveDirection = moveDirection.normalized * (moveSpeed * Time.deltaTime);

            transform1.position += moveDirection;

            // Jumping
            Debug.Log(_isGrounded);
            if (Input.GetButtonDown("Jump") && _isGrounded)
            {
                GetComponent<Rigidbody>().AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                _isGrounded = false;
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _isGrounded = true;
            }
        }
    }
}
