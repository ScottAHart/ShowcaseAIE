using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

namespace CUI
{
    [CustomEditor(typeof(CUIButton), true)]
    public class CUIButtonEditor : UnityEditor.UI.ButtonEditor
    {
        public override void OnInspectorGUI()
        {
            //CUIButton button = (CUIButton)target;
            //DrawDefaultInspector();
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_onOver"), EditorStyles.standardFont);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_onExit"), EditorStyles.standardFont);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_onDown"), EditorStyles.standardFont);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_onUp"), EditorStyles.standardFont);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_onEnabled"), EditorStyles.standardFont);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_onDisabled"), EditorStyles.standardFont);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
