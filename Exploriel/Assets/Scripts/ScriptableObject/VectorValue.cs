using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VectorValue", menuName = "ScriptableObjects/VectorValue", order = 1)]
public class VectorValue : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 initialValue;
    public Vector2 value;

    private void OnEnable()
    {
        initialValue = value; // Initialize initialValue with the current value
    }

    public void OnBeforeSerialize()
    {
        // Implement any logic before serialization if needed
    }

    public void OnAfterDeserialize()
    {
        // Reset value to initialValue after deserialization
        value = initialValue;
    }
}
