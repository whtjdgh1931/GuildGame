using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickPanel : MonoBehaviour
{
    public Button attackBtn;
    public Button skillBtn;
    public Button ultiBtn;

    
    private Player_Attack playerAttack;

    // Start is called before the first frame update
    void Start()
    {
				playerAttack = GameObject.Find(Constants.NAME_Player).GetComponent<Player_Attack>();
        attackBtn.onClick.AddListener(playerAttack.PressAtkBtn);
        skillBtn.onClick.AddListener(playerAttack.PressSkillBtn);
        ultiBtn.onClick.AddListener(playerAttack.PressUltiBtn);
    }

    void Update()
    {
        attackBtn.image.fillAmount = playerAttack.GetAttackCoolTime();
        skillBtn.image.fillAmount = playerAttack.GetSkillCoolTime();
        ultiBtn.image.fillAmount = playerAttack.GetUltiCoolTime();

        
    }
}
