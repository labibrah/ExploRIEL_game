using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; // The target to follow
    public float smoothing; // Speed of the camera movement
    public Animator cameraAnimator; // Animator for camera effects
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        cameraAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transform.position != target.position)
        {
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
        }
    }

    public void DoScreenKick()
    {
        StartCoroutine(ScreenKick());
    }

    public IEnumerator ScreenKick()
    {
        cameraAnimator.SetBool("KickActive", true);
        yield return new WaitForSeconds(0.1f);
        cameraAnimator.SetBool("KickActive", false);
    }
}
