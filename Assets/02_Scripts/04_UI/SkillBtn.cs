using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : MonoBehaviour
{
		private Image btnImage;
		private Button btn;
		private Color color;

		void Start()
		{
				btn = GetComponent<Button>();
				btnImage = GetComponent<Image>();
				color = btnImage.color;
		}

		void Update()
		{
				if (btnImage.fillAmount < 1f)
				{
						btn.interactable = false;
						color.a = 0.5f;
						btnImage.color = color;
				}
				else
				{
						btn.interactable = true;
						color.a = 1f;
						btnImage.color = color;
				}

		}
}
