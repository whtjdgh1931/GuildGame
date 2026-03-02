using UnityEngine;
using UnityEngine.AI;


public class SoldierMaker : MonoBehaviour
{
		public ClassScriptableObject classScriptableObject;



		public Soldier soldierPreviewPrefab;

		private Soldier previewInstance;
		private string previewClassName;
		private bool isDrag = false;
		[SerializeField] private float navMeshSampleDistance = 2f;
	
		private Camera mainCam;
		private Plane groundPlane = new(Vector3.up, Vector3.zero);

		public void Start()
		{
				mainCam = Camera.main;
		}

		public void StartDrag(string className)
		{
				if (previewInstance != null) Destroy(previewInstance);
				previewInstance = MakeSoldierPreview(className);
				isDrag = previewInstance != null;
		}

		public void Update()
		{
#if UNITY_STANDALONE_WIN
				
				if (!isDrag) return;

				if (previewInstance == null) return;
				if (TryGetPointerWorldPosition(Input.mousePosition, out Vector3 worldPos) &&
						TryGetNavMeshPosition(worldPos, out Vector3 navMeshPos))
				{
						previewInstance.transform.position = navMeshPos;
				}

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
#elif UNITY_ANDROID
				if (!isDrag || previewInstance == null || Input.touchCount == 0) return;

				Touch touch = Input.GetTouch(0);

				if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            return;
				if (TryGetPointerWorldPosition(touch.position, out Vector3 touchPos) &&
						TryGetNavMeshPosition(touchPos, out Vector3 navMeshPos))
				{
						previewInstance.transform.position = navMeshPos;
				}

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
				if (!TryGetNavMeshPosition(position, out Vector3 navMeshPosition))
				{
						Debug.LogWarning($"Could not place {className}: no NavMesh near {position}.");
						return null;
				}

				ClassData classData = classScriptableObject.GetClassDataByClassName(className);
				Soldier soldier = Instantiate(classData.soldierPrefab, navMeshPosition, Quaternion.identity);
				soldier.classLevelData = ClassManager.Instance().GetLevelData(className);
				soldier.level = Mathf.Min(PlayerPrefs.GetInt(className),Constants.maxLevel);
				soldier.SetLevelData(soldier.level);
				return soldier;



		}

		public Soldier MakeSoldierPreview(string className)
		{
				Vector2 pointerPosition = Input.mousePosition;
#if UNITY_ANDROID
				if (Input.touchCount > 0)
				{
						pointerPosition = Input.GetTouch(0).position;
				}
#endif

				if (!TryGetPointerWorldPosition(pointerPosition, out Vector3 worldPos) ||
						!TryGetNavMeshPosition(worldPos, out Vector3 navMeshPos))
				{
						Debug.LogWarning("Could not create preview: pointer is not over NavMesh.");
						return null;
				}

				Soldier soldierPreview = MakeSoldier(className, navMeshPos);
				if (soldierPreview == null) return null;

				SpriteRenderer[] spriteRenderers = soldierPreview.GetComponentsInChildren<SpriteRenderer>();
				foreach(SpriteRenderer spriteRenderer in spriteRenderers)
				{
				Color tempColor = spriteRenderer.color;
				tempColor.a = 0.4f;
				spriteRenderer.color = tempColor;

				}
				float attackRange = ClassManager.Instance().GetAttackRangeData(className);
				if (soldierPreview.attackRangeObject != null)
				{
						soldierPreview.attackRangeObject.gameObject.SetActive(true);
				}
				previewClassName = className;
				return soldierPreview;
		}

		private bool TryGetPointerWorldPosition(Vector2 screenPosition, out Vector3 worldPosition)
		{
				Ray ray = mainCam.ScreenPointToRay(screenPosition);
				if (groundPlane.Raycast(ray, out float distance))
				{
						worldPosition = ray.GetPoint(distance);
						return true;
				}

				worldPosition = Vector3.zero;
				return false;
		}

		private bool TryGetNavMeshPosition(Vector3 targetPosition, out Vector3 navMeshPosition)
		{
				if (NavMesh.SamplePosition(targetPosition, out NavMeshHit hit, navMeshSampleDistance, NavMesh.AllAreas))
				{
						navMeshPosition = hit.position;
						return true;
				}

				navMeshPosition = Vector3.zero;
				return false;
		}

		#region makeSoldierButton
		public void MakeTankerButton()
		{
				StartDrag(Constants.CLASS_TANKER);
		}

		public void MakeWarriorButton()
		{
				StartDrag(Constants.CLASS_WARRIOR);
		}

		public void MakeArcherButton()
		{
				StartDrag(Constants.CLASS_ARCHER);
		}

		public void MakeAssassinButton()
		{
				StartDrag(Constants.CLASS_ASSASSIN);
		}
		public void MakeHealerButton()
		{
				StartDrag(Constants.CLASS_HEALER);
		}

		public void MakeMagicianButton()
		{
				StartDrag(Constants.CLASS_MAGICIAN);
		}
		#endregion
}
