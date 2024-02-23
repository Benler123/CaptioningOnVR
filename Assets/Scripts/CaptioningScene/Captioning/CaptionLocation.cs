using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CaptionLocation : MonoBehaviour
{
    // Start is called before the first frame update
    public Parameters Params;
    public bool update;
    public GameObject background; 
    Camera mainCamera;
    float dist;
    private GameObject textBox;
    
    
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        mainCamera = Camera.main;
        dist = transform.position.z - .0001f;
        rectTransform.sizeDelta = new Vector2(Params.getWidth(dist), rectTransform.sizeDelta.y);
        textBox = transform.GetChild(0).gameObject;
    }  

    // Update is called once per frame
    void Update(){
        if(update){
            PlaceCaptions(Params.captioningMethod != 3 && Params.captioningMethod != 4);
        }
    }

    void PlaceCaptions(bool rotate) {
        transform.position = Params.projectOntoSphere(dist, background.transform) + new Vector3(0.01f, 0, 0);
        if (rotate) {
            Vector3 forwardFromCamera = mainCamera.transform.forward;
            Vector3 rotatedForward = Parameters.rotateYawFromCameraForward(Params.offsetX);
            transform.rotation = Quaternion.LookRotation(rotatedForward);
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
    void HandleNonRegCaptions(float OffsetX, float OffsetY) {
        Vector3 forwardFromCamera = mainCamera.transform.forward;
        Vector3 newPosition = mainCamera.transform.position + forwardFromCamera * dist;
        
        // textBox.transform.localPosition = new Vector3(OffsetX, OffsetY, 0);
        transform.position = newPosition;
        transform.rotation = Quaternion.LookRotation(forwardFromCamera);
    }
}

