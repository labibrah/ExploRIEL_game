using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{
    private Animator animator;
    public AudioSource audioSource;
    public AudioClip smashSound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Smash()
    {
        animator.SetBool("smash", true);
        StartCoroutine(SmashCo());
    }

    IEnumerator SmashCo()
    {
        if (audioSource != null && smashSound != null)
        {
            audioSource.PlayOneShot(smashSound);
        }
        yield return new WaitForSeconds(0.5f);
        this.gameObject.SetActive(false);
    }
}
