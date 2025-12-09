using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnUI : MonoBehaviour
{
		[SerializeField] protected Button button;


		protected virtual void Start()
    {
				button = GetComponent<Button>();

		}


}
