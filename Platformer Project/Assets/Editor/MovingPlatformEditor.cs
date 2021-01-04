using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

public class MovingPlatformEditor : MonoBehaviour
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
[CustomEditor(typeof(MovingFloor))]
[CanEditMultipleObjects]
public class MovingFloorEditor : Editor
{

    // This is Data
    SerializedProperty nodes;
    private MovingFloor movingFloor;
    bool foldoutFlag;

    ReorderableList list;

    private void OnEnable()
    {

        movingFloor = target as MovingFloor;

        // Get the <wave> array from WaveManager, in SerializedProperty form.
        nodes = serializedObject.FindProperty("nodes");

        list = new ReorderableList(serializedObject, nodes, true, true, true, true);


        list.drawElementCallback = DrawListItems;
        list.drawHeaderCallback = DrawHeader;

    }

    void DrawListItems(Rect rect, int index, bool isActive, bool isFocused)
    {
        SerializedProperty element = list.serializedProperty.GetArrayElementAtIndex(index); // The element in the list
        SerializedProperty element2 = element.FindPropertyRelative("location");
        SerializedProperty element3 = element.FindPropertyRelative("index");

        
        EditorGUI.PropertyField
            (
            new Rect(rect.x + 140, rect.y, 160, EditorGUIUtility.singleLineHeight),
            element2,
            GUIContent.none
            );

        EditorGUI.PropertyField
            (
            new Rect(rect.x + 35, rect.y, 30, EditorGUIUtility.singleLineHeight),
            element3,
            GUIContent.none
            );

        EditorGUI.LabelField(new Rect(rect.x, rect.y, 130, EditorGUIUtility.singleLineHeight),
          ("Index: "));

        EditorGUI.LabelField(new Rect(rect.x + 75, rect.y, 130, EditorGUIUtility.singleLineHeight),
          ("Location: "));

        //Set Preset Data
        element3.intValue = index;

        if(index == 0 && !Application.isPlaying)
        {
            element2.vector2Value = movingFloor.transform.position;
        }
    }

    

    void DrawHeader(Rect rect)
    {
        string name = "Path Node List";
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