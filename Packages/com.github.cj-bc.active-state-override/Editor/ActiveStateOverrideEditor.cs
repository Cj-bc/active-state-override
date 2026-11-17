using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ActiveStateOverride))]
public class ActiveStateOverrideEditor : Editor
{
    FieldInfo m_OverridenByField = typeof(ActiveStateOverride).GetField("OverridenBy", BindingFlags.NonPublic | BindingFlags.Instance);

    private Dictionary<Type, MonoScript> _monoScriptsCache = new();

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
                } else if (findMonoScript(overridenBy.GetType()) is MonoScript scriptFile)
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.ObjectField("Overriden By(pure class): ", scriptFile, typeof(MonoScript));
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

    private static MonoScript findMonoScript(Type type)
    {
        if (_monoScriptsCache.TryGetValue(type, out var script))
        {
            return script;
        }

        string[] guids = AssetDatabase.FindAssets($"{type.Name} t:MonoScript");
        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            MonoScript script = AssetDatabase.LoadAssetAtPath<MonoScript>(path);

            // 同名クラスが別名前空間にある場合に備え、GetClass()で型が完全一致するか確認
            if (script != null && script.GetClass() == type)
            {
                _monoScriptsCache.Add(type, script);
                return script;
            }
        }
        return null;

    }
}
