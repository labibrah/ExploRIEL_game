using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(fileName = "VectorValue", menuName = "ScriptableObjects/VectorValue", order = 1)]
public class VectorValue : ScriptableObject
{
    public Vector2 initialValue;
    public Vector2 runtimeValue;

#if UNITY_EDITOR
    [InitializeOnLoadMethod]
    private static void RegisterPlayModeReset()
    {
        EditorApplication.playModeStateChanged += ResetOnEnterPlayMode;
    }

    private static void ResetOnEnterPlayMode(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            string[] guids = AssetDatabase.FindAssets("t:VectorValue");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                VectorValue asset = AssetDatabase.LoadAssetAtPath<VectorValue>(path);
                if (asset != null)
                {
                    asset.runtimeValue = asset.initialValue;
                    EditorUtility.SetDirty(asset); // Optional: updates inspector
                }
            }
        }
    }
#endif
}
