using UnityEditor;
using UnityEngine;

public class WaveEditorWindow : EditorWindow
{
    private SubWaveSO subWave;
    private SerializedObject serializedSubWave;
    private SerializedProperty enemiesProp;
    private SerializedProperty enemyCountsProp;

    [MenuItem("Window/SubWave Editor")]
    public static void ShowWindow()
    {
        GetWindow<WaveEditorWindow>("SubWave Editor");
    }

    private void OnEnable()
    {
        subWave = CreateInstance<SubWaveSO>();
        serializedSubWave = new SerializedObject(subWave);
        enemiesProp = serializedSubWave.FindProperty("enemies");
        enemyCountsProp = serializedSubWave.FindProperty("enemyCounts");
    }

    private void OnGUI()
    {
        serializedSubWave.Update();

        EditorGUILayout.PropertyField(serializedSubWave.FindProperty("waveName"), new GUIContent("Wave Name"));

        EditorGUILayout.PropertyField(enemiesProp, new GUIContent("Enemies"), true);
        EditorGUILayout.PropertyField(enemyCountsProp, new GUIContent("Enemy Counts"), true);

        serializedSubWave.ApplyModifiedProperties();

        if (GUILayout.Button("Save Wave"))
        {
            SaveWave();
        }
    }

    private void SaveWave()
    {
        string path = EditorUtility.SaveFilePanelInProject("Save SubWave", subWave.subWaveName, "asset", "Save SubWave as ScriptableObject");
        if (path == "")
            return;
        
        AssetDatabase.CreateAsset(subWave, path);
        AssetDatabase.SaveAssets();
    }
}
