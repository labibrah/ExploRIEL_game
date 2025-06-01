using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject contextClue;
    // Start is called before the first frame update
    public void Enable()
    {
        contextClue.SetActive(true);
    }

    public void Disable()
    {
        contextClue.SetActive(false);
    }
}
