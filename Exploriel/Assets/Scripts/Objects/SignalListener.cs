using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SignalListener : MonoBehaviour
{
    public Signal signal;
    public UnityEngine.Events.UnityEvent response;

    private void OnEnable()
    {
        signal.RegisterListener(this);
    }

    private void OnDisable()
    {
        signal.UnregisterListener(this);
    }

    public void OnSignalRaised()
    {
        response.Invoke();
    }
}
