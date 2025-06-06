using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FloatValue : ScriptableObject, ISerializationCallbackReceiver
{
    public float initialValue;
    public float runtimeValue;
    public float maxValue = 10f;

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        runtimeValue = initialValue; // Reset runtime value to initial value after deserialization
    }
}
