using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class SoldierMaker : MonoBehaviour
{
		public ClassScriptableObject classScriptableObject;



		public Soldier soldierPreviewPrefab;

		private Soldier previewInstance;
		private string previewClassName;
		private bool isDrag = false;
		private Camera mainCam;

		public void Start()
		{
				mainCam = Camera.main;
		}

		public void StartDrag(string className)
		{
				if (previewInstance != null) Destroy(previewInstance);
				previewInstance = MakeSoldierPreview(className);
				isDrag = true;
		}

		public void Update()
		{
#if UNITY_STANDALONE_WIN
				
				if (!isDrag) return;

				Vector3 mousePos = Input.mousePosition;
				Vector3 worldPos = mainCam.ScreenToWorldPoint(mousePos);
				worldPos.y = 0f;

				previewInstance.transform.position = worldPos;

				if (Input.GetMouseButtonUp(0))
				{
						MakeSoldier(previewClassName, previewInstance.transform.position);
						Destroy(previewInstance.gameObject);
						isDrag = false;
				}

				if (Input.GetMouseButtonDown(1))
				{
						Destroy(previewInstance.gameObject);
						isDrag = false;
				}
#elif UNITY_EDITOR || UNITY_ANDROID
				if (!isDrag || !Input.GetMouseButton(0)) return;



				//if (EventSystem.current.IsPointerOverGameObject())
				//		return;
				Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
				mousePos.y = 0;

				if (previewInstance != null)
						previewInstance.transform.position = mousePos;

				if (Input.GetMouseButtonUp(0))
				{
						Debug.Log("up");
						MakeSoldier(previewClassName, previewInstance.transform.position);
						Destroy(previewInstance.gameObject);
						isDrag = false;
				}

#elif UNITY_ANDROID
				if (!isDrag||Input.touchCount == 0) return;

				Touch touch = Input.GetTouch(0);

				if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return;
						Vector3 touchPos = mainCam.ScreenToWorldPoint(touch.position);
						  touchPos.y = 0;

				if (previewInstance != null)
						previewInstance.transform.position = touchPos;

				switch (touch.phase)
				{
						case TouchPhase.Ended:
								MakeSoldier(previewClassName, previewInstance.transform.position);
								Destroy(previewInstance.gameObject);
								isDrag = false;
								break;
				}

#endif
		}


		public Soldier MakeSoldier(string className, Vector3 position)
		{
				ClassData classData = classScriptableObject.GetClassDataByClassName(className);
				Soldier soldier = Instantiate(classData.soldierPrefab, position, Quaternion.identity);
				soldier.classLevelData = ClassManager.Instance().GetLevelData(className);
				soldier.level = PlayerPrefs.GetInt(className);
				soldier.SetLevelData(soldier.level);

				return soldier;



		}

		public Soldier MakeSoldierPreview(string className)
		{
				Soldier soldierPreview = MakeSoldier(className,Input.mousePosition);
				SoldierRange range = soldierPreview.GetComponentInChildren<SoldierRange>();
				SpriteRenderer spriteRenderer = soldierPreview.GetComponentInChildren<SpriteRenderer>();
				Debug.Log(spriteRenderer == null);
				Color tempColor = spriteRenderer.color;
				tempColor.a = 0.4f;
				spriteRenderer.color = tempColor;
				float attackRange = ClassManager.Instance().GetAttackRangeData(className);
				if (range != null)
				{
						Vector3 rangeScale = new(attackRange, 0.1f,attackRange);
						range.transform.localScale = rangeScale;
				}
				else Debug.Log("range = null");
				previewClassName = className;
				return soldierPreview;
		}

		#region makeSoldierButton
		public void MakeTankerButton()
		{
				StartDrag(Constants.CLASS_TANKER);
#if UNITY_ANDROID || UNITY_EDITOR
				isDrag = true;
#endif
		}

		public void MakeWarriorButton()
		{
				StartDrag(Constants.CLASS_WARRIOR);
#if UNITY_ANDROID || UNITY_EDITOR
				isDrag = true;
#endif
		}

		public void MakeArcherButton()
		{
				StartDrag(Constants.CLASS_ARCHER);
#if UNITY_ANDROID || UNITY_EDITOR
				isDrag = true;
#endif
		}

		public void MakeAssassinButton()
		{
				StartDrag(Constants.CLASS_ASSASSIN);
#if UNITY_ANDROID || UNITY_EDITOR
				isDrag = true;
#endif
		}
		public void MakeHealerButton()
		{
				StartDrag(Constants.CLASS_HEALER);
#if UNITY_ANDROID || UNITY_EDITOR
				isDrag = true;
#endif
		}

		public void MakeMagicianButton()
		{
				StartDrag(Constants.CLASS_MAGICIAN);
#if UNITY_ANDROID || UNITY_EDITOR
				isDrag = true;
#endif
		}
		#endregion
}
