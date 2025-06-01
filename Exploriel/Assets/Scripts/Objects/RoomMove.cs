using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public bool needText;
    public string placeName;
    public GameObject text;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (needText)
            {
                StartCoroutine(placeNameCoroutine());
            }
        }
    }

    private IEnumerator placeNameCoroutine()
    {
        text.SetActive(true);
        yield return new WaitForSeconds(2);
        text.SetActive(false);
    }
}
