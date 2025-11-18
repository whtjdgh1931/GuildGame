using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public Slider volumeSlider;
	[SerializeField] private TextMeshProUGUI _maxVolume;
	[SerializeField] private TextMeshProUGUI _currentVolume;

		public void Start()
		{
				volumeSlider = GetComponent<Slider>();
				volumeSlider.onValueChanged.AddListener(VolumeManager.Instance().SetVolume);
		volumeSlider.onValueChanged.AddListener(SetVolumeText);
		_maxVolume.text = volumeSlider.maxValue.ToString();		
		}

	public void SetVolumeText(float currentVolume)
	{
		_currentVolume.text = currentVolume.ToString();
	}

	

}
