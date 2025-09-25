using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAnim : MonoBehaviour
{
    private NavMeshAgent agent;
    private Animator anim;
    private FSM FSM;
    private Character4D character;

    private void Awake()
    {
        agent = GetComponentInParent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        character = GetComponent<Character4D>();
        FSM = GetComponentInParent<FSM>();

    }

    private void Update()
    {
        if ((FSM.targetSoldier != null))
        {
            float tempX = FSM.targetSoldier.transform.position.x - transform.position.x;
            if (tempX > 0)
                character.SetDirection(Vector2.right);
            else character.SetDirection(Vector2.left);

        }

    }

    public void SetAnimAttack()
    {
        Debug.Log("Attack");
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
