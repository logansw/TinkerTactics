using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Collections.Generic;

[CustomEditor(typeof(WaveHolder))]
public class WaveHolderEditor : Editor
{
    private ReorderableList waveList;

    // Dictionaries to hold nested ReorderableLists
    private Dictionary<int, ReorderableList> laneLists = new Dictionary<int, ReorderableList>();
    private Dictionary<(int WaveIndex, int LaneIndex), ReorderableList> squadLists = new Dictionary<(int, int), ReorderableList>();

    // Dictionary to hold foldout states for Waves
    private Dictionary<int, bool> waveFoldouts = new Dictionary<int, bool>();

    private void OnEnable()
    {
        SerializedProperty wavesProperty = serializedObject.FindProperty("Waves");

        // Initialize the Wave ReorderableList
        waveList = new ReorderableList(serializedObject, wavesProperty, true, true, true, true);

        // Wave List Header
        waveList.drawHeaderCallback = (Rect rect) => {
            EditorGUI.LabelField(rect, "Waves");
        };

        // Wave List Element Height
        waveList.elementHeightCallback = (int index) => {
            float height = EditorGUIUtility.singleLineHeight + 5; // For the foldout
            if (waveFoldouts.ContainsKey(index) && waveFoldouts[index])
            {
                SerializedProperty wave = wavesProperty.GetArrayElementAtIndex(index);
                SerializedProperty lanes = wave.FindPropertyRelative("Lanes");
                height += GetLaneListHeight(lanes, index);
            }
            return height;
        };

        // Wave List Element Drawing
        waveList.drawElementCallback = DrawWaveElement;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        waveList.DoLayoutList();
        serializedObject.ApplyModifiedProperties();
    }

    private void DrawWaveElement(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty wave = waveList.serializedProperty.GetArrayElementAtIndex(index);
        SerializedProperty lanesProp = wave.FindPropertyRelative("Lanes");

        // Define rectangles
        float lineHeight = EditorGUIUtility.singleLineHeight;
        Rect foldoutRect = new Rect(rect.x, rect.y, rect.width, lineHeight);

        // Initialize foldout state if not present
        if (!waveFoldouts.ContainsKey(index))
            waveFoldouts[index] = false;

        // Draw Foldout for Wave
        waveFoldouts[index] = EditorGUI.Foldout(foldoutRect, waveFoldouts[index], $"Wave {index + 1}", true);

        // If foldout is open, draw the nested Lanes list
        if (waveFoldouts[index])
        {
            // Define the rect for the nested list
            Rect lanesRect = new Rect(rect.x, rect.y + lineHeight, rect.width, GetLaneListHeight(lanesProp, index));
            ReorderableList lanesList = GetLanesList(lanesProp, index);
            lanesList.DoList(lanesRect);
        }
    }

    private ReorderableList GetLanesList(SerializedProperty lanesProp, int waveIndex)
    {
        if (!laneLists.ContainsKey(waveIndex))
        {
            ReorderableList lanesList = new ReorderableList(serializedObject, lanesProp, true, true, true, true);

            // Lanes List Header
            lanesList.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, $"Lanes in Wave {waveIndex + 1}");
            };

            // Lanes List Element Height
            lanesList.elementHeightCallback = (int index) => {
                SerializedProperty lane = lanesProp.GetArrayElementAtIndex(index);
                SerializedProperty squads = lane.FindPropertyRelative("Squads");
                float height = GetSquadListHeight(squads, waveIndex, index);
                // Add height for Lane label
                height += EditorGUIUtility.singleLineHeight + 5;
                return height;
            };

            // Lanes List Element Drawing
            lanesList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                SerializedProperty lane = lanesList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty squadsProp = lane.FindPropertyRelative("Squads");

                // Define rectangles
                float lineHeight = EditorGUIUtility.singleLineHeight;
                Rect labelRect = new Rect(rect.x, rect.y, rect.width, lineHeight);

                // Label each Lane
                EditorGUI.LabelField(labelRect, $"Lane {index + 1}");

                // Draw Squads ReorderableList
                Rect squadsRect = new Rect(rect.x, rect.y + lineHeight, rect.width, GetSquadListHeight(squadsProp, waveIndex, index));
                ReorderableList squadsList = GetSquadsList(squadsProp, waveIndex, index);
                squadsList.DoList(squadsRect);
            };

            laneLists.Add(waveIndex, lanesList);
        }

        return laneLists[waveIndex];
    }

    private ReorderableList GetSquadsList(SerializedProperty squadsProp, int waveIndex, int laneIndex)
    {
        var key = (waveIndex, laneIndex);
        if (!squadLists.ContainsKey(key))
        {
            ReorderableList squadsList = new ReorderableList(serializedObject, squadsProp, true, true, true, true);

            // Squads List Header
            squadsList.drawHeaderCallback = (Rect rect) => {
                EditorGUI.LabelField(rect, $"Squads in Lane {laneIndex + 1}, Wave {waveIndex + 1}");
            };

            // Squads List Element Height
            squadsList.elementHeightCallback = (int index) => {
                return EditorGUIUtility.singleLineHeight;
            };

            // Squads List Element Drawing
            squadsList.drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                SerializedProperty squad = squadsList.serializedProperty.GetArrayElementAtIndex(index);
                SerializedProperty enemyTypeProp = squad.FindPropertyRelative("Enemy");
                SerializedProperty countProp = squad.FindPropertyRelative("Count");

                float halfWidth = (rect.width - 5) / 2;
                Rect enemyTypeRect = new Rect(rect.x, rect.y, halfWidth, EditorGUIUtility.singleLineHeight);
                Rect countRect = new Rect(rect.x + halfWidth + 5, rect.y, halfWidth, EditorGUIUtility.singleLineHeight);

                EditorGUI.PropertyField(enemyTypeRect, enemyTypeProp, GUIContent.none);
                EditorGUI.PropertyField(countRect, countProp, GUIContent.none);
            };

            // Ensure minimum height for empty lists
            squadsList.elementHeightCallback = (int index) => {
                return EditorGUIUtility.singleLineHeight + 5;
            };

            squadLists.Add(key, squadsList);
        }

        return squadLists[key];
    }

    private float GetLaneListHeight(SerializedProperty lanesProp, int waveIndex)
    {
        if (lanesProp.arraySize == 0)
        {
            return 70f;
        }
        float totalHeight = 50f;
        for (int i = 0; i < lanesProp.arraySize; i++)
        {
            SerializedProperty lane = lanesProp.GetArrayElementAtIndex(i);
            SerializedProperty squads = lane.FindPropertyRelative("Squads");

            // Each Lane has a label and a Squads list
            float laneHeight = EditorGUIUtility.singleLineHeight + 7.5f + GetSquadListHeight(squads, waveIndex, i);
            totalHeight += laneHeight;
        }
        return totalHeight;
    }

    private float GetSquadListHeight(SerializedProperty squadsProp, int waveIndex, int laneIndex)
    {
        if (squadsProp.arraySize == 0)
        {
            // Allocate height for add button even if list is empty
            return 70f;
        }
        // Each Squad has a fixed height
        return 50f + squadsProp.arraySize * (EditorGUIUtility.singleLineHeight + 7.5f);
    }
}