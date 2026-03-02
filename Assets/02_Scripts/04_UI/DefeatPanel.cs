using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatPanel : MonoBehaviour
{
		public RectTransform targetUI;   // 버튼이나 이미지


		void Start()
		{
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
								});

		}

}
