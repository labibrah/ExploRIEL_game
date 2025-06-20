using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "ScriptableObjects/FloatValue")]
public class FloatValue : ScriptableObject
{
    public float initialValue;
    public float runtimeValue;
    public float maxValue = 10f;

#if UNITY_EDITOR
    [InitializeOnLoadMethod]
    private static void RegisterFloatResetOnPlayMode()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            string[] guids = AssetDatabase.FindAssets("t:FloatValue");
            foreach (string guid in guids)
            {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                FloatValue asset = AssetDatabase.LoadAssetAtPath<FloatValue>(path);
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
