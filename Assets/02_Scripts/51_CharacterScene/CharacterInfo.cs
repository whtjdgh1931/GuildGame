using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterInfo : MonoBehaviour
{
    [SerializeField] private Image _characterImage;
    [SerializeField] private TextMeshProUGUI _characterText;

    // Start is called before the first frame update
    void Start()
    {
        _characterImage.sprite = GameManager.GetInstance().PlayerClassScriptableObject.GetClassDataByClassName(GameManager.GetInstance().playerClass).soldierLogoPrefab;
        _characterText.text = GameManager.GetInstance().playerClass;
    }

    
}
