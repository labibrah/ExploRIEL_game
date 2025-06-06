using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;
    public bool contextClueActive = false;

    public void ToggleContextClue()
    {
        contextClueActive = !contextClueActive;
        contextClue.SetActive(contextClueActive);
    }
}
