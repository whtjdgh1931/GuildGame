using UnityEngine;
using UnityEngine.UI;

public class SkillBtn : BtnUI
{
		private Image btnImage;
		private Color color;

		new void  Start()
		{
				base.Start();
				btnImage = GetComponent<Image>();
				color = btnImage.color;
		}

		void Update()
		{
				if (btnImage.fillAmount < 1f)
				{
						button.interactable = false;
						color.a = 0.5f;
						btnImage.color = color;
				}
				else
				{
						button.interactable = true;
						color.a = 1f;
						btnImage.color = color;
				}

		}
}
