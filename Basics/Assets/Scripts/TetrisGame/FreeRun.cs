using UnityEngine;

public class PlayerFreeMovement : MonoBehaviour
{
    public Rigidbody rb;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float rotationSpeed = 50f;
    public float speed = 30f;
    public float jumpForce = 5f;

    private bool isGrounded = true;
    private Animator animator;

    private float turn;
    private float move;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        animator.applyRootMotion = false;
    }

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        turn = Input.GetAxis("Horizontal");
        move = Input.GetAxis("Vertical");

        bool isRunning = Mathf.Abs(move) > 0.1f || Mathf.Abs(turn) > 0.1f;
        animator.SetBool("isRunning", isRunning);
        animator.applyRootMotion = isRunning;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void FixedUpdate()
    {
        // Rotazione fluida
        if (Mathf.Abs(turn) > 0.01f)
        {
            float turnAmount = turn * rotationSpeed * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, turnAmount, 0);
            rb.MoveRotation(rb.rotation * deltaRotation);
        }

        // Movimento avanti/indietro
        if (Mathf.Abs(move) > 0.1f)
        {
            Vector3 direction = rb.rotation * Vector3.forward;
            Vector3 targetVelocity = direction * move * speed;

            Vector3 currentVelocity = rb.linearVelocity;
            targetVelocity.y = currentVelocity.y; // Mantieni la y (gravità/salto)

            rb.linearVelocity = targetVelocity;
        }
        else
        {
            // Ferma il movimento orizzontale mantenendo la y
            Vector3 currentVelocity = rb.linearVelocity;
            currentVelocity.x = 0;
            currentVelocity.z = 0;
            rb.linearVelocity = currentVelocity;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}
