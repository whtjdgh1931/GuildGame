using Assets.HeroEditor4D.Common.Scripts.CharacterScripts;
using UnityEngine;
using UnityEngine.AI;

public class SoldierAnim : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected FSM FSM;
    [SerializeField] protected Character4D character;
    [SerializeField] protected float horizontal;



    protected void Awake()
    {
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

        if(horizontal>0)
        {
						character.SetDirection(Vector2.right);
				}
        else if(horizontal<0)
        {
						character.SetDirection(Vector2.left);
				}

    }

    public virtual void SetAnimAttack()
    {
        
        anim.SetTrigger("IsAttack");
    }

    public virtual void SetAnimSkill()
    {
        anim.SetTrigger("IsSkill");

    }

    public virtual void SetAnimMove(Vector3 velocity)
    {
        anim.SetBool("IsMove", !Mathf.Approximately(velocity.magnitude, 0));
    }

    public virtual void SetAnimUlti()
    {
        anim.SetTrigger("IsUlti");
    }

    public void SetHorizontal(float horizontal)
    {
        this.horizontal = horizontal;
    }
}
