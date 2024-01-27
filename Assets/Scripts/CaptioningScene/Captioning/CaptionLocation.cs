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
            PlaceCaptions();
        }
    }

    void PlaceCaptions() {
        transform.position = Params.projectOntoSphere(dist, background.transform) + new Vector3(0.01f, 0, 0);
        Vector3 forwardFromCamera = mainCamera.transform.forward;
        transform.rotation = Quaternion.LookRotation(forwardFromCamera);
      }
}

