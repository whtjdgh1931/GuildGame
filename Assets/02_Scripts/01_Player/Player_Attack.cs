using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    private Player_Skill playerSkill;
    private Player_Soldier player;
    private SoldierAnim soldier_Anim;



    private float curTime;
    private float attackCoolTime;
    private float curSkillTime;
    private float skillCoolTime;
    private float curUltiTime;
    private float UltiCoolTime;

    public void ReadyAttack()
    {
        player = GetComponent<Player_Soldier>();
        playerSkill = GetComponentInChildren<Player_Skill>();
        soldier_Anim = GetComponentInChildren<SoldierAnim>();



        curTime = float.MaxValue;
        attackCoolTime = Constants.AttackTime / player.attackSpeed;

        curSkillTime = float.MaxValue;
        skillCoolTime = Constants.skillCoolTime;

        curUltiTime = float.MaxValue;
        UltiCoolTime = Constants.ultiCoolTime;
    }

    public void SetAttackSpeed(float multi)
    {
        attackCoolTime *= multi;
    }

    public void Update()
    {
        if (!SoldierManager.Instance().isBattle) return;

        curTime += Time.deltaTime;
        curSkillTime += Time.deltaTime;
        curUltiTime += Time.deltaTime;

        if (player == null) return;
        if (player.isAuto) return;



        if (Input.GetMouseButton(0))
        {
            if (curTime >= attackCoolTime)
            {
                Soldier target = playerSkill.SearchEnemyTarget(player.attackRange);
                if (target == null) return;
                if (soldier_Anim != null) soldier_Anim.SetAnimAttack();
                playerSkill.DoAttack(target);
                
                curTime = 0;
            }
        }
        else if (Input.GetMouseButton(1))
        {
            if (curSkillTime >= skillCoolTime)
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                targetPosition.y = 0;
                if (soldier_Anim != null) soldier_Anim.SetAnimSkill();

                playerSkill.DoSkill(targetPosition);
                curSkillTime = 0;
                curTime = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (curUltiTime >= UltiCoolTime)
            {
                Vector3 mousePosition = Input.mousePosition;
                Vector3 targetPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                targetPosition.y = 0;
                if (soldier_Anim != null) soldier_Anim.SetAnimUlti();

                playerSkill.DoUlti(targetPosition);
                curSkillTime = 0;
                curSkillTime = Mathf.Min(curSkillTime, skillCoolTime - 2f);

                curTime = 0;
            }
        }


    }

    public void DoAutoAttack()
    {
        if (curUltiTime >= UltiCoolTime)
        {
            Soldier target = playerSkill.SearchEnemyTarget(Mathf.Max(player.attackRange, player.skillRange));
            if (target == null) return;
            if (soldier_Anim != null) soldier_Anim.SetAnimUlti();

            playerSkill.DoUlti(target);
            curUltiTime = 0;
            curSkillTime = Mathf.Min(curSkillTime,skillCoolTime - 2f);

            curTime = 0;
            return;
        }

        if (curSkillTime >= skillCoolTime)
        {
            Soldier target = playerSkill.SearchEnemyTarget(Mathf.Max(player.attackRange, player.skillRange));
            if (target == null) return;
            if (soldier_Anim != null) soldier_Anim.SetAnimSkill();

            playerSkill.DoSkill(target);
            curSkillTime = 0;
            curTime = 0;
            return;
        }

        if (curTime >= attackCoolTime)
        {
            Soldier target = playerSkill.SearchEnemyTarget(Mathf.Max(player.attackRange, player.skillRange));
            if (target == null) return;
            if (soldier_Anim != null) soldier_Anim.SetAnimAttack();

            playerSkill.DoAttack(target);
            curTime = 0;
            return;
        }
    }

    public void PressAtkBtn()
    {
        if (curTime >= attackCoolTime)
        {
            Soldier target = playerSkill.SearchEnemyTarget(Mathf.Max(player.attackRange, player.skillRange));
            if (target == null) return;
            if (soldier_Anim != null) soldier_Anim.SetAnimAttack();

            playerSkill.DoAttack(target);
            curTime = 0;
        }
    }

    public void PressSkillBtn()
    {
        if (curSkillTime >= skillCoolTime)
        {
            Soldier target = playerSkill.SearchEnemyTarget(Mathf.Max(player.attackRange, player.skillRange));
            if (target == null) return;
            if (soldier_Anim != null) soldier_Anim.SetAnimSkill();

            playerSkill.DoSkill(target);
            curSkillTime = 0;
            curTime = 0;
        }
    }

    public void PressUltiBtn()
    {
        if (curUltiTime >= UltiCoolTime)
        {
            Soldier target = playerSkill.SearchEnemyTarget(Mathf.Max(player.attackRange, player.skillRange));
            if (target == null) return;
            if (soldier_Anim != null) soldier_Anim.SetAnimUlti();

            playerSkill.DoUlti(target);
            curUltiTime = 0;
            curSkillTime = Mathf.Min(curSkillTime, skillCoolTime - 2f);

            curTime = 0;
        }
    }


    public float GetAttackCoolTime()
    {
        return curTime / attackCoolTime;
    }

    public float GetUltiCoolTime()
    {
        return curUltiTime / UltiCoolTime;
    }

    public float GetSkillCoolTime()
    {
        return curSkillTime / skillCoolTime;

    }

}
