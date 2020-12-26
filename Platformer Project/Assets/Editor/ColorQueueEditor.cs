using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class ColorQueueEditor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


// Tells Unity to use this Editor class with the WaveManager script component.
[CustomEditor(typeof(ColorQueue))]
[CanEditMultipleObjects]
public class ScheduleEditor : Editor
{

    // This is Data
    SerializedProperty colorSlots;
    bool foldoutFlag;

    ReorderableList list;

    private void OnEnable()
    {
        // Get the <wave> array from WaveManager, in SerializedProperty form.
        colorSlots = serializedObject.FindProperty("colorSlots");

        list = new ReorderableList(serializedObject, colorSlots, true, true, true, true);


        list.drawElementCallback = DrawListItems;
        list.drawHeaderCallback = DrawHeader;

    }

    void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index); // The element in the list
        SerializedProperty element2 = element.FindPropertyRelative("currentColor");



        EditorGUI.PropertyField
            (
            new Rect(rect.x + 130, rect.y, 160, EditorGUIUtility.singleLineHeight),
            element2,
            GUIContent.none
            );


        EditorGUI.LabelField(new Rect(rect.x, rect.y, 130, EditorGUIUtility.singleLineHeight),
          ("Color: "));
    }

    void DrawHeader(Rect rect)
    {
        string name = "Color Queue List";
        EditorGUI.LabelField(rect, name);
    }

    //This is the function that makes the custom editor work
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        serializedObject.Update(); // Update the array property's representation in the inspector


        //EditorGUILayout.PropertyField(colorSlots);

        list.DoLayoutList(); // Have the ReorderableList do its work

        #region FoldoutMethodNotUsed
        /*foldoutFlag = EditorGUILayout.BeginFoldoutHeaderGroup(foldoutFlag, "Color Queue Foldout");

        if (foldoutFlag)
        {
            if (Selection.activeTransform)
            {
                EditorGUILayout.PropertyField(colorSlots);

                list.DoLayoutList(); // Have the ReorderableList do its work

            }
        }

        if (!Selection.activeTransform)
        {
            foldoutFlag = false;
        }
        EditorGUILayout.EndFoldoutHeaderGroup();
        */
        #endregion

        // We need to call this so that changes on the Inspector are saved by Unity.
        serializedObject.ApplyModifiedProperties();

    }
}