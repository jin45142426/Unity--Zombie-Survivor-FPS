using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("�÷��̾� �̵� �ӵ�")]
    public float moveSpeed = 5f;

    [Header("Jump Settings")]
    [Tooltip("���� �� (��� ��)")]
    public float jumpForce = 50f;
    [Tooltip("�ٴ� ������ ���� ����ĳ��Ʈ �Ÿ�")]
    public float groundCheckDistance = 0.2f;

    [Header("Look Settings")]
    [Tooltip("���콺 ����")]
    public float lookSensitivity = 1f;
    [Tooltip("ī�޶��� ���� ȸ�� ���� (����)")]
    public float verticalRotationLimit = 80f;

    [Header("Camera Reference")]
    [Tooltip("�÷��̾� �ڽĿ� ��ġ�� FPS ī�޶��� Transform")]
    public Transform cameraTransform;

    // ���ο��� ������ �Է� ��
    private Vector2 moveInput;
    private Vector2 lookInput;
    private float verticalRotation = 0f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        // Rigidbody�� �ܺ� ���� ���� ȸ���� �ڵ����� �������� �ʵ��� ȸ�� ���� �����մϴ�.
        rb.freezeRotation = true;

        // ���� ���� �� Ŀ�� ���� �� ���
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Player Input ������Ʈ�� Send Messages Ȥ�� Unity Events�� ȣ���� �� ����Ǵ� �޼����

    /// <summary>
    /// "Move" �׼ǿ� ����� �Է� �̺�Ʈ
    /// </summary>
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
            moveInput = context.ReadValue<Vector2>();
        else if (context.canceled)
            moveInput = Vector2.zero;
    }

    /// <summary>
    /// "Look" �׼ǿ� ����� �Է� �̺�Ʈ
    /// </summary>
    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
            lookInput = context.ReadValue<Vector2>();
        else if (context.canceled)
            lookInput = Vector2.zero;
    }

    /// <summary>
    /// "Jump" �׼ǿ� ����� �Է� �̺�Ʈ
    /// </summary>
    public void OnJump(InputAction.CallbackContext context)
    {
        // ���� �Է��� performed �����̰�, �ٴڿ� ��� ���� ���� ���� ó��
        if (context.performed && IsGrounded())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // ���� ��� �̵� �� �߷� ó���� FixedUpdate���� �����մϴ�.
    private void FixedUpdate()
    {
        HandleMovement();
    }

    // ȸ��(����) ó���� Update���� �����մϴ�.
    private void Update()
    {
        HandleLook();
    }

    /// <summary>
    /// �Էµ� �̵� ���� ���� �ӵ��� �ݿ��Ͽ� �̵� ó��
    /// </summary>
    private void HandleMovement()
    {
        // �÷��̾��� �����ʰ� ���� ���͸� �������� ���� �̵� ���� ���
        Vector3 moveDirection = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        Vector3 horizontalMovement = moveDirection * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + horizontalMovement);
    }

    /// <summary>
    /// �Էµ� ���콺 ���� ���� �÷��̾�� ī�޶� ȸ�� ó��
    /// </summary>
    private void HandleLook()
    {
        // �¿� ȸ��: �÷��̾� GameObject ��ü�� Y�� �������� ȸ��
        transform.Rotate(Vector3.up, lookInput.x * lookSensitivity);

        // ���� ȸ��: ���� ȸ������ ������� ī�޶��� ��ġ(pitch) ����
        verticalRotation -= lookInput.y * lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }

    /// <summary>
    /// �ٴ� ������ ���� �÷��̾� �Ʒ��� ����ĳ��Ʈ�� ����
    /// </summary>
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance);
    }
}
