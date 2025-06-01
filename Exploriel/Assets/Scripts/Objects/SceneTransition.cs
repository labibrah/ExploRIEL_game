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

    private void Awake()
    {
        if (FadeIn != null)
        {
            GameObject fadeInInstance = Instantiate(FadeIn, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(fadeInInstance, 0.3f); // Destroy after 1 second
        }

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
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

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);

        while (!asyncLoad.isDone)
        {
            yield return null; // Wait until the scene is fully loaded
        }
    }
}
