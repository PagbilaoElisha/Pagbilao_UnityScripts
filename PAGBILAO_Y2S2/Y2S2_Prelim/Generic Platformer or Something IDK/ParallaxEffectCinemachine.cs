using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffectCinemachine : MonoBehaviour
{
    public Transform virtualCam;
    public float parallaxFactor = 0.5f;

    private Vector3 lastCamPos;

    void Start()
    {
        if (virtualCam == null)
            virtualCam = Camera.main.transform;

        lastCamPos = virtualCam.position;
    }

    void LateUpdate()
    {
        Vector3 deltaMovement = virtualCam.position - lastCamPos;
        transform.position += new Vector3(deltaMovement.x * parallaxFactor, deltaMovement.y * parallaxFactor * 0.5f, 0);
        lastCamPos = virtualCam.position;
    }
}
