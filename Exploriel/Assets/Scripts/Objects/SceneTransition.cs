using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition;
    public VectorValue playerStorage;
    public GameObject FadeIn;
    public GameObject FadeOut;
    public float fadeDuration = 0.2f;
    public SpriteRenderer portalSpriteRenderer;
    public Sprite portalActiveSprite;
    public Sprite portalInactiveSprite;
    public AudioClip portalSound;
    private AudioSource audioSource;
    public List<BoolValue> activateConditions;
    public Animator portalAnimator;
    public Camera camera;

    private void Awake()
    {
        if (FadeIn != null)
        {
            GameObject fadeInInstance = Instantiate(FadeIn, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(fadeInInstance, 0.3f); // Destroy after 1 second
        }
        audioSource = GetComponent<AudioSource>();
        if (portalSpriteRenderer == null)
        {
            portalSpriteRenderer = GetComponent<SpriteRenderer>();
        }
        if (portalAnimator == null)
        {
            portalAnimator = GetComponent<Animator>();
        }
        if (camera == null)
        {
            camera = FindObjectOfType<Camera>();
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Collision detected with: " + collision.name);
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerStorage.runtimeValue = playerPosition;
            StartCoroutine(FadeOutAndLoadScene());
        }
    }

    public IEnumerator FadeOutAndLoadScene()
    {
        if (FadeOut != null)
        {
            GameObject fadeOutInstance = Instantiate(FadeOut, Vector3.zero, Quaternion.identity) as GameObject;
            yield return new WaitForSeconds(fadeDuration);
        }

        Debug.Log("Loading scene: " + sceneToLoad);
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is fully loaded
        }
    }

    public void Activated()
    {
        StartCoroutine(PortalActivated());
    }

    private IEnumerator PortalActivated()
    {
        if (audioSource != null && portalSound != null)
        {
            audioSource.PlayOneShot(portalSound);
        }

        yield return new WaitForSeconds(0.5f);

        if (portalSpriteRenderer != null)
        {
            portalSpriteRenderer.sprite = portalActiveSprite;
        }
        camera.GetComponent<CameraMovement>().PayAttentionTo(gameObject);
        if (portalAnimator != null)
        {
            portalAnimator.SetBool("isActivated", true);
        }

        // Disable the collider that is NOT a trigger
        Collider2D[] colliders = GetComponents<Collider2D>();
        foreach (var col in colliders)
        {
            if (!col.isTrigger)
            {
                col.enabled = false;
            }
        }
    }

    public void CheckConditions()
    {
        bool allConditionsMet = true;

        foreach (BoolValue condition in activateConditions)
        {
            if (!condition.runtimeValue)
            {
                allConditionsMet = false;
                Debug.Log($"Condition {condition.name} not met.");
                break;
            }
        }

        if (allConditionsMet)
        {
            Activated();
        }
        else
        {
            if (portalSpriteRenderer != null)
            {
                portalSpriteRenderer.sprite = portalInactiveSprite;
            }
        }
    }
}
