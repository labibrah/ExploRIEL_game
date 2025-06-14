using UnityEngine;
using System.Collections;

public class BGMManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip defaultClip;
    public AudioClip hoverClip;

    private Coroutine fadeCoroutine;
    private AudioClip currentClip;

    void Start()
    {
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
            if (audioSource == null)
            {
                Debug.LogError("AudioSource component is missing on BGMManager.");
                return;
            }
        }
        currentClip = defaultClip;
        audioSource.clip = defaultClip;
        audioSource.volume = 1f;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void PlayHoverMusic()
    {
        if (currentClip != hoverClip)
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeToClip(hoverClip));
        }
    }

    public void PlayDefaultMusic()
    {
        if (currentClip != defaultClip)
        {
            if (fadeCoroutine != null)
                StopCoroutine(fadeCoroutine);
            fadeCoroutine = StartCoroutine(FadeToClip(defaultClip));
        }
    }

    private IEnumerator FadeToClip(AudioClip newClip)
    {
        float fadeOutTime = 0.5f;
        float fadeInTime = 0.5f;

        // Fade out
        while (audioSource.volume > 0.01f)
        {
            audioSource.volume -= Time.deltaTime / fadeOutTime;
            yield return null;
        }

        audioSource.Stop();
        audioSource.clip = newClip;
        currentClip = newClip;
        audioSource.Play();

        // Fade in
        while (audioSource.volume < 1f)
        {
            audioSource.volume += Time.deltaTime / fadeInTime;
            yield return null;
        }
    }
}
