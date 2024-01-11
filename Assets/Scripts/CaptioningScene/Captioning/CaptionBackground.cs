using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using System;
public class CaptionBackground : MonoBehaviour
{
    // Start is called before the first frame update
    private float buffer = .5f;
    public Transform juror1;
    public Transform juror2;
    public Transform juror3;
    public Transform juror4;

    private Transform currentJuror;

    private GameObject leftArrow;
    private GameObject rightArrow;
    public Parameters Params;
    Camera mainCamera;
    float dist;
    void Start()
    {
        Params.setJurorPositions(juror1, juror2, juror3, juror4);
        mainCamera = Camera.main;
        dist = transform.position.z;
        transform.localScale = new Vector3(Params.getWidth(dist), transform.localScale.y, transform.localScale.z);
        leftArrow = transform.GetChild(1).gameObject;
        rightArrow = transform.GetChild(2).gameObject;


    }  

    // Update is called once per frame
    void Update(){

   
        currentJuror = Params.ReturnCurrentJurorTransform();
        float offset = 10;
        switch (Params.captioningMethod) {
            case 1:
                HandleNonRegCaptions(offset);
                HandleNonRegArrows();
                break;
            case 2:
                HandleNonRegCaptions(offset);
                break;
            case 3:
                HandleRegCaptions();
                break;
            case 4:
                HandleRegCaptions();
                HandleRegArrows(); 
                break;
        }


    }


    void HandleNonRegCaptions(float offset) {
        Vector3 forwardFromCamera = mainCamera.transform.forward;
        Vector3 newPosition = mainCamera.transform.position + forwardFromCamera * dist;;
            // Set the object's position
        transform.position = newPosition;
            
        //  make the object look at the camera
        transform.rotation = Quaternion.LookRotation(forwardFromCamera);
    }

    void HandleRegCaptions() {
        Vector3 forwardFromCamera = mainCamera.transform.forward;
        Vector3 pointOnSphere = Params.projectOntoSphere(dist, currentJuror);
        
        Vector3 newPosition = pointOnSphere;
            
            // Set the object's position
        transform.position = newPosition;
            
        //  make the object look at the camera
        transform.rotation = Quaternion.LookRotation(forwardFromCamera);
    }

    void HandleRegArrows(){
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);

        Vector3 forwardFromCamera = mainCamera.transform.forward;
        Vector3 newPosition = mainCamera.transform.position + forwardFromCamera * dist;
        leftArrow.transform.position = newPosition;
        rightArrow.transform.position = newPosition;

        Vector3 pointOnSphere = Params.projectOntoSphere(dist, currentJuror);

        if(newPosition.x + buffer > pointOnSphere.x && newPosition.x - buffer < pointOnSphere.x) {
            leftArrow.GetComponent<MeshRenderer>().enabled = false;
            rightArrow.GetComponent<MeshRenderer>().enabled = false;
        }
        else if (newPosition.x - pointOnSphere.x > 0){
            leftArrow.GetComponent<MeshRenderer>().enabled = false;
            rightArrow.GetComponent<MeshRenderer>().enabled = true;
        } else{
            leftArrow.GetComponent<MeshRenderer>().enabled = true;
            rightArrow.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    void HandleNonRegArrows() {
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);

        Vector3 pointOnSphere = Params.projectOntoSphere(dist, currentJuror);
        if(transform.position.x + buffer > pointOnSphere.x && transform.position.x - buffer < pointOnSphere.x) {
            leftArrow.GetComponent<MeshRenderer>().enabled = false;
            rightArrow.GetComponent<MeshRenderer>().enabled = false;
        }
        else if (transform.position.x -pointOnSphere.x > 0){
            leftArrow.GetComponent<MeshRenderer>().enabled = false;
            rightArrow.GetComponent<MeshRenderer>().enabled = true;

        } else{
            leftArrow.GetComponent<MeshRenderer>().enabled = true;
            rightArrow.GetComponent<MeshRenderer>().enabled = false;
        }
    }


}
