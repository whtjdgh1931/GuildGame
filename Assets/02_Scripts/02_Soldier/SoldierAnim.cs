using UnityEngine;
using UnityEngine.AI;

public class SoldierAnim : MonoBehaviour
{
    private NavMeshAgent agent;
    private SpriteRenderer[] spriteRenderers;
    private Animator anim;
    private FSM FSM;
    private bool isFlip;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        anim = GetComponent<Animator>();
        FSM = GetComponentInParent<FSM>();
        isFlip = spriteRenderers[0].flipX;

    }

    private void Update()
    {
        if ((FSM.targetSoldier != null))
        {
            float tempX = FSM.targetSoldier.transform.position.x - transform.position.x;
            foreach (SpriteRenderer animRenderer in spriteRenderers)
            {
                if (tempX < 0)
                    animRenderer.flipX = !isFlip;
                else animRenderer.flipX = isFlip;
                //animRenderer.transform.forward = Camera.main.transform.forward;

            }

        }

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
