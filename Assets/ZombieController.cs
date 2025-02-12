using UnityEngine;

public class ZombieController : MonoBehaviour
{
    PlayerController player = null;
    float distance;
    Animator animator;
    public float rotationSpeed = 5f; // ȸ�� �ӵ�
    float speed = 0.5f;
    bool check = false;

    private void Start()
    {
        //�÷��̾� ã��
        player = GameObject.Find("Player Character").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // ����� ������ �������� ����
        if (player == null) return;

        // �÷��̾���� �Ÿ� ���
        distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance > 2.0f)
        {
            check = true;
            // �÷��̾� �������� �̵�
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

            // �ڿ������� �÷��̾� �ٶ󺸱�
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

        }

        else
            check = false;

        Animation();
    }

    void Animation()
    {
        if(animator == null) return;

        if (check == false)
            animator.Play("Z_Attack");
        else
            animator.Play("Z_Walk_InPlace");
    }
}
