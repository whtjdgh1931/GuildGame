using UnityEngine;
using UnityEngine.AI;

public class SoldierAnim : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private FSM FSM;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        FSM = GetComponentInParent<FSM>();

    }

    
    public void SetAnimAttack()
    {
        anim.SetTrigger("IsAttack");
    }

    public void SetAnimSkill()
    {
        anim.SetTrigger("IsSkill");

    }

    public void SetAnimMove(Vector3 velocity)
    {
        anim.SetBool("IsMove", !Mathf.Approximately(velocity.magnitude, 0));
    }

    public void SetAnimUlti()
    {
        anim.SetTrigger("IsUlti");
    }
}
