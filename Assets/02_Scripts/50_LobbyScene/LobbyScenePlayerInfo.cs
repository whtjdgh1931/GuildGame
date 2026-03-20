using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyScenePlayerInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _playerLevel;
    [SerializeField] private Slider _playerExpSlider;
    [SerializeField] private TextMeshProUGUI _playerExp;
    

    public void Start()
    {
        SetPlayerExpText();
    }

    public void SetPlayerExpText()
    {
        _playerLevel.text = GameManager.GetInstance().playerLevel.ToString();
        _playerExpSlider.value = GameManager.GetInstance().GetExpRatio();
        _playerExp.text = GameManager.GetInstance().playerExp.ToString() + " / " + GameManager.GetInstance().playerLevelupExp.ToString();
    }
}
