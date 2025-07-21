using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoyStickPanel : MonoBehaviour
{
    public Button attackBtn;
    public Button skillBtn;
    public Button ultiBtn;

    // Start is called before the first frame update
    void Start()
    {
        Player_Attack playerAttack = GameObject.Find(Constants.NAME_Player).GetComponent<Player_Attack>();
        attackBtn.onClick.AddListener(playerAttack.PressAtkBtn);
        skillBtn.onClick.AddListener(playerAttack.PressSkillBtn);
        ultiBtn.onClick.AddListener(playerAttack.PressUltiBtn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
