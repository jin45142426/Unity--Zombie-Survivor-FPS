using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("�÷��̾� �̵� �ӵ�")]
    public float moveSpeed = 5f;

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

    // Player Input ������Ʈ�� Send Messages ���� �Է� �̺�Ʈ�� ȣ���� �� ����Ǵ� �޼����

    /// <summary>
    /// "Move" �׼ǿ� ����� �Է� �̺�Ʈ
    /// Input Action Asset���� �׼� �̸��� "Move"���� �ڵ����� ȣ��˴ϴ�.
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
    /// Input Action Asset���� �׼� �̸��� "Look"�̾�� �ڵ����� ȣ��˴ϴ�.
    /// </summary>
    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
            lookInput = context.ReadValue<Vector2>();
        else if (context.canceled)
            lookInput = Vector2.zero;
    }

    // ���� ��� �̵� ó���� FixedUpdate���� �����մϴ�.
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
    /// �Էµ� �̵� ���� ���� Rigidbody.MovePosition�� �̿��Ͽ� �̵� ó��
    /// </summary>
    private void HandleMovement()
    {
        // �÷��̾��� �����ʰ� ���� ���͸� �������� �̵� ���� ���
        Vector3 moveDirection = (transform.right * moveInput.x + transform.forward * moveInput.y).normalized;
        // �̵� ���� ��� (Time.fixedDeltaTime�� ���� ������ �� �̵� �Ÿ� ����)
        Vector3 movement = moveDirection * moveSpeed * Time.fixedDeltaTime;
        // Rigidbody.MovePosition�� ���� �浹 ó���� �Բ� �̵�
        rb.MovePosition(rb.position + movement);
    }

    /// <summary>
    /// �Էµ� ���콺 ���� ���� �÷��̾�� ī�޶� ȸ�� ó��
    /// </summary>
    private void HandleLook()
    {
        // �¿� ȸ��: �÷��̾� GameObject ��ü�� Y�� �������� ȸ��
        transform.Rotate(Vector3.up, lookInput.x * lookSensitivity);

        // ���� ȸ��: ���� ȸ������ ������� ī�޶��� ��ġ(pitch)�� ����
        verticalRotation -= lookInput.y * lookSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -verticalRotationLimit, verticalRotationLimit);

        if (cameraTransform != null)
        {
            cameraTransform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }
}
