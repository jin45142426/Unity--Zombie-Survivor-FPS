using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    private PlayerController player = null;
    private CharacterHpBar HpBar = null;
    private Animator animator;
    private NavMeshAgent agent;
    public float rotationSpeed = 5f; // ȸ�� �ӵ�
    public float speed = 0.5f;
    public float distance = 0.0f;
    private bool check = false;             //�÷��̾���� �Ÿ�
    private int attack_Damage = 25;         //���� ���� ������

    private void Start()
    {
        //�÷��̾� ã��
        player = GameObject.Find("Player Character").GetComponent<PlayerController>();
        HpBar = player.GetComponent<CharacterHpBar>();
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // ����� ������ �������� ����
        if (player == null) return;
        if (agent == null) return; 
        if (HpBar == null) return;

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
        {
            check = false;
        }
            
        Animation();
    }

    public void Attack()           // ���� �Լ�
    {
        if (HpBar == null)
            return;

        HpBar.Damage(attack_Damage);
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
