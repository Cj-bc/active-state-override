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

            if (overridenBy is not null)
            {
                if (overridenBy is UnityEngine.Object)
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.ObjectField("Overriden By: ", (UnityEngine.Object)overridenBy, typeof(UnityEngine.Object));
                    }
                } else
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.TextField($"Overriden By(plain class): {overridenBy}");
                    }
                }
            } else
            {
                EditorGUILayout.HelpBox("Currntly not overriden", MessageType.Info);
            }
        }
    }
}
