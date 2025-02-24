using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    PlayerController player = null;
    public float distance;
    private Animator animator;
    private NavMeshAgent agent;
    public float rotationSpeed = 5f; // ȸ�� �ӵ�
    public float speed = 0.5f;
    private bool check = false;             //�÷��̾���� �Ÿ�

    private void Start()
    {
        //�÷��̾� ã��
        player = GameObject.Find("Player Character").GetComponent<PlayerController>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
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
            // �ڿ������� �÷��̾� �ٶ󺸱�
            Vector3 direction = (player.transform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
            agent.SetDestination(player.transform.position);
        }

        else
            check = false;

        Animation();
    }

    void Animation()
    {
        if (animator == null) return;

        if (check == false)
            animator.Play("Z_Attack");
        else
            animator.Play("Z_Walk_InPlace");
    }
}
