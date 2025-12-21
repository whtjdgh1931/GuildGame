using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class VictoryPanel : MonoBehaviour
{
		public RectTransform targetUI;   // 버튼이나 이미지
		[SerializeField] private Slider _expSlider;
		[SerializeField] private TextMeshProUGUI _playerLevel;
		[SerializeField] private TextMeshProUGUI _currentExp;
		[SerializeField] private float _goalExp;
		[SerializeField] private float _sliderSpead;

		void Start()
		{
				_playerLevel.text = GameManager.GetInstance().playerLevel.ToString();
				PlayVictoryEffect();

		}

		void PlayVictoryEffect()
		{
				targetUI.localScale = Vector3.zero;

		
				targetUI.DOScale(Vector3.one, 0.6f)
								.SetEase(Ease.OutBack);

				Vector3 originalPos = targetUI.localPosition;
				Vector3 originalScale = targetUI.localScale;

				targetUI.DOShakePosition(0.5f, 20, 10)
								.OnComplete(() =>
								{
										targetUI.localPosition = originalPos;
										targetUI.localScale = originalScale;
										GetExp();
								});

		}

		public void GetExp()
		{
				GameManager.GetInstance().PlusExpByStage();

				_goalExp = GameManager.GetInstance().GetExpRatio();
				_currentExp.text = GameManager.GetInstance().playerExp.ToString() + " / " + GameManager.GetInstance().playerLevelupExp.ToString();
		}

		public void Update()
		{
			_expSlider.value = Mathf.Lerp(_expSlider.value, _goalExp, _sliderSpead*Time.deltaTime);	
		}

}
