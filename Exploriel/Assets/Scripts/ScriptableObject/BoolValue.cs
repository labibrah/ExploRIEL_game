using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class BoolValue : ScriptableObject, ISerializationCallbackReceiver
{
    public bool initialValue;
    public bool runtimeValue;

    // Method to reset the runtime value to the initial value
    public void Reset()
    {
        runtimeValue = initialValue;
    }

    // Method to set the runtime value
    public void SetValue(bool value)
    {
        runtimeValue = value;
    }

    // Method to get the current runtime value
    public bool GetValue()
    {
        return runtimeValue;
    }

    public void OnBeforeSerialize()
    {
        // This method is called before serialization, can be used to prepare data if needed
    }
    public void OnAfterDeserialize()
    {
        // This method is called after deserialization, can be used to reset or validate data
        runtimeValue = initialValue; // Reset runtime value to initial value after deserialization
    }
}
