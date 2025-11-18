using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class TMPFontChangerAll : EditorWindow
{
    private TMP_FontAsset newFont;

    [MenuItem("Tools/TMP Font Changer (All Scenes & Prefabs)")]
    public static void ShowWindow()
    {
        GetWindow<TMPFontChangerAll>("TMP Font Changer");
    }

    void OnGUI()
    {
        GUILayout.Label("Change TMP Fonts in Project", EditorStyles.boldLabel);

        newFont = (TMP_FontAsset)EditorGUILayout.ObjectField("New TMP Font", newFont, typeof(TMP_FontAsset), false);

        if (GUILayout.Button("Change Fonts in All Scenes & Prefabs"))
        {
            ChangeFontsInProject();
        }
    }

    private void ChangeFontsInProject()
    {
        if (newFont == null)
        {
            Debug.LogWarning("새 TMP 폰트가 지정되지 않았습니다!");
            return;
        }

        // === 씬 처리 ===
        string[] sceneGuids = AssetDatabase.FindAssets("t:Scene");
        foreach (string guid in sceneGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            // 패키지 내부 씬은 pass
            if (path.StartsWith("Packages/"))
            {
                Debug.Log($"패키지 씬 {path} 은 건너뜁니다.");
                continue;
            }

            Scene scene = EditorSceneManager.OpenScene(path, OpenSceneMode.Single);

            TextMeshProUGUI[] tmpTexts = GameObject.FindObjectsOfType<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI tmp in tmpTexts)
            {
                Undo.RecordObject(tmp, "Change TMP Font");
                tmp.font = newFont;
                EditorUtility.SetDirty(tmp);
            }

            EditorSceneManager.SaveScene(scene);
            Debug.Log($"씬 {path} 에서 TMP 폰트 변경 완료 ({tmpTexts.Length}개)");
        }

        // === 프리팹 처리 ===
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);

            // 패키지 내부 프리팹도 pass
            if (path.StartsWith("Packages/"))
            {
                Debug.Log($"패키지 프리팹 {path} 은 건너뜁니다.");
                continue;
            }

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (prefab == null) continue;

            TextMeshProUGUI[] tmpTexts = prefab.GetComponentsInChildren<TextMeshProUGUI>(true);
            foreach (TextMeshProUGUI tmp in tmpTexts)
            {
                Undo.RecordObject(tmp, "Change TMP Font");
                tmp.font = newFont;
                EditorUtility.SetDirty(tmp);
            }

            PrefabUtility.SavePrefabAsset(prefab);
            Debug.Log($"프리팹 {path} 에서 TMP 폰트 변경 완료 ({tmpTexts.Length}개)");
        }

        AssetDatabase.SaveAssets();
        Debug.Log("모든 씬과 프리팹에 TMP 폰트 적용 완료!");
    }
}
