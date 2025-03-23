using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActiveStateOverride))]
public class ActiveStateOverrideEditor : Editor
{
    FieldInfo m_OverridenByField = typeof(ActiveStateOverride).GetField("OverridenBy", BindingFlags.NonPublic | BindingFlags.Instance);

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (!Application.isPlaying) return;

        if (target is ActiveStateOverride _target)
        {
            object overridenBy = m_OverridenByField?.GetValue(_target);

            if (overridenBy is not null && (UnityEngine.Object)overridenBy is not null)
            {
                using (new EditorGUI.DisabledScope(true))
                {
                    EditorGUILayout.ObjectField("Overriden By: ", (UnityEngine.Object)overridenBy, typeof(UnityEngine.Object));
                }
            } else
            {
                EditorGUILayout.HelpBox("Currntly not overriden", MessageType.Info);
            }
        }
    }
}
