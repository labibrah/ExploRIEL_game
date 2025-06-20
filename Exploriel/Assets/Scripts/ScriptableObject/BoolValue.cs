using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif

[CreateAssetMenu(menuName = "Values/BoolValue")]
public class BoolValue : ScriptableObject
{
    public bool initialValue;
    public bool runtimeValue;

#if UNITY_EDITOR
    // Register the callback once when Unity loads
    [InitializeOnLoadMethod]
    private static void InitOnLoad()
    {
        EditorApplication.playModeStateChanged += ResetAllOnEnterPlayMode;
    }

    private static void ResetAllOnEnterPlayMode(PlayModeStateChange state)
    {
        // Only reset when entering Play Mode (not scene reloads)
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            string[] guids = AssetDatabase.FindAssets("t:BoolValue");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                BoolValue asset = AssetDatabase.LoadAssetAtPath<BoolValue>(path);
                if (asset != null)
                {
                    asset.runtimeValue = asset.initialValue;

                    // Mark dirty so it updates in Inspector
                    EditorUtility.SetDirty(asset);
                }
            }
        }
    }
#endif

    public void SetValue(bool value)
    {
        runtimeValue = value;
    }

    public bool GetValue()
    {
        return runtimeValue;
    }
}
