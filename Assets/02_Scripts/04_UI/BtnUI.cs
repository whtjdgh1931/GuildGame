using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class BtnUI : MonoBehaviour
{
		[SerializeField] protected Button button;

		protected UnityEngine.Events.UnityEvent inspectorEvents;
		protected Image buttonImage;
		protected Sequence currentSequence; // 실행 중인 시퀀스를 저장
		protected Color originalColor;

		protected virtual void Start()
		{
				DOTween.Init();
				if (button == null)
				{
						button = GetComponent<Button>();
				}
				if (buttonImage == null)
				{
						buttonImage = GetComponent<Image>();
				}
				else
				{
						originalColor = buttonImage.color;
				}

		}

		public void AddBtnAnim()
		{
				inspectorEvents = button.onClick;

				button.onClick = new Button.ButtonClickedEvent();
				button.onClick.AddListener(PlayEffect);
		}

		protected void PlayEffect()
		{
				// 이전 시퀀스가 살아있으면 먼저 Kill
				currentSequence?.Kill();

				if (Time.timeScale == 0)
				{
						Time.timeScale = 1.0f;
				}
				currentSequence = DOTween.Sequence();
				currentSequence.Append(button.transform.DOScale(1.2f, 0.1f));
				currentSequence.Append(button.transform.DOScale(1f, 0.1f));
				currentSequence.Join(button.transform.DOShakeScale(
						0.25f,
						strength: new Vector3(0.2f, 0.2f, 0),
						vibrato: 30,
						randomness: 90,
						fadeOut: true
				));
				currentSequence.Append(button.transform.DOScale(1f, 0.05f));

				currentSequence.OnComplete(() =>
				{
						inspectorEvents?.Invoke();
				});
		}

		public void OnDisable()
		{
				// 버튼 비활성화 시 애니메이션 종료
				currentSequence?.Kill();
				currentSequence = null;
		}
}
