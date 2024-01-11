using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CaptionLocation : MonoBehaviour
{
    // Start is called before the first frame update
    public Parameters Params;
    public float offset = 0f;
    Camera mainCamera;
    float dist;
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
        dist = transform.position.z - .0001f;
        rectTransform.sizeDelta = new Vector2(Params.getWidth(dist), rectTransform.sizeDelta.y);
    }  

    // Update is called once per frame
    void Update(){
        UpdatePosition();
    }

    void UpdatePosition() {
        switch (Params.captioningMethod) {
            case 1:
                HandleNonRegCaptions();
                break;
            case 2:
                HandleNonRegCaptions();
                break;
            case 3:
                HandleRegCaptions();
                break;
            case 4:
                HandleRegCaptions();
                break;
        }
    }

    void HandleRegCaptions() {
        Vector3 forwardFromCamera = mainCamera.transform.forward;
        Vector3 pointOnSphere = Params.projectOntoSphere(dist, Params.ReturnCurrentJurorTransform());
        Vector3 newPosition = pointOnSphere;
            
            // Set the object's position
        transform.position = newPosition;
            
        //  make the object look at the camera
        transform.rotation = Quaternion.LookRotation(forwardFromCamera);
    }
    void HandleNonRegCaptions() {
        Vector3 forwardFromCamera = mainCamera.transform.forward;
        Vector3 newPosition = mainCamera.transform.position + forwardFromCamera * dist;
        transform.position = newPosition;
        transform.rotation = Quaternion.LookRotation(forwardFromCamera);
    }
}

