using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;
using System;

public class CaptionBackground : MonoBehaviour
{
    
    int[] LEFT_ARROW_CHILDREN = {1,4,5,7,9};
    int[] RIGHT_ARROW_CHILDREN = {0,2,3,6,8};
    // Start is called before the first frame update
    private float buffer = .0001f;
    public int test;
    public Transform juror1;
    public Transform juror2;
    public Transform juror3;
    public Transform juror4;

    private Transform currentJuror;

    private GameObject leftArrow;
    private GameObject rightArrow;
    private GameObject backgroundRect; 
    private const float VERTICAL_FOV = 6.86f;
    public Parameters Params;
    Camera mainCamera;
    float dist;
    void Start()
    {
        Params.setJurorPositions(juror1, juror2, juror3, juror4);
        mainCamera = Camera.main;
        dist = transform.position.z;
        transform.localScale = new Vector3(Params.getWidth(dist), transform.localScale.y, transform.localScale.z);
        backgroundRect = transform.GetChild(0).gameObject;
        leftArrow = transform.GetChild(0).GetChild(1).gameObject;
        rightArrow = transform.GetChild(0).GetChild(0).gameObject;
        buffer = Params.getWidth(dist)/2;        
    }  

    // Update is called once per frame
    void Update(){

        setArrows("left", false);
        setArrows("right", false);
        currentJuror = Params.ReturnCurrentJurorTransform();
        float offsetX = Params.offsetX;
        float offsetY = Params.offsetY;
        switch (Params.captioningMethod) {
            case 1:
                HandleNonRegCaptions(offsetX, offsetY);
                HandleNonRegArrows();
                break;
            case 2:
                HandleNonRegCaptions(offsetX, offsetY);
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);
                break;
            case 3:
                HandleRegCaptions();
                leftArrow.SetActive(false);
                rightArrow.SetActive(false);
                break;
            case 4:
                HandleRegCaptions();
                HandleRegArrows(); 
                break;
        }
        
    }


    void HandleNonRegCaptions(float offsetX, float offsetY) {
        Vector3 forwardFromCamera = mainCamera.transform.forward;
        Vector3 newPosition = mainCamera.transform.position + forwardFromCamera * dist;
        Vector3 rotatedForward = Parameters.rotateYawFromCameraForward(offsetX);
        newPosition = mainCamera.transform.position + rotatedForward * dist;
        // Set the Caption Container position
        transform.position = newPosition;

        // backgroundRect.transform.position = new Vector3(backgroundRect.transform.position.x, backgroundRect.transform.position.y,
        //  (float)Math.Sqrt((dist * dist - Math.Pow(backgroundRect.transform.position.x, 2) - Math.Pow(backgroundRect.transform.position.y, 2))));
            
        //  make the Container look at the camera
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
            leftArrow.GetComponent<MeshRenderer>().enabled = true;
            rightArrow.GetComponent<MeshRenderer>().enabled = false;
        } else{
            leftArrow.GetComponent<MeshRenderer>().enabled = false;
            rightArrow.GetComponent<MeshRenderer>().enabled = true;
        }

        leftArrow.transform.rotation = Quaternion.LookRotation(forwardFromCamera);
        rightArrow.transform.rotation = Quaternion.LookRotation(forwardFromCamera);
    }
    void HandleNonRegArrows() {
        leftArrow.SetActive(true);
        rightArrow.SetActive(true);
        Vector3 forwardFromCamera = mainCamera.transform.forward;

        Vector3 newPosition = mainCamera.transform.position + forwardFromCamera * dist;
        Vector3 pointOnSphere = Params.projectOntoSphere(dist, currentJuror);
        // if(backgroundRect.transform.position.x + buffer > pointOnSphere.x && backgroundRect.transform.position.x - buffer < pointOnSphere.x) {
        //     setArrows("left", false);
        //     setArrows("right", false);
        // }
        // else if (backgroundRect.transform.position.x -pointOnSphere.x > 0){
        //     setArrows("left", true);
        //     setArrows("right", false);

        // } else{
        //     setArrows("right", true);
        //     setArrows("left", false);
        // }
        if(newPosition.x + buffer > pointOnSphere.x && newPosition.x - buffer < pointOnSphere.x) {
            setArrows("left", false);
            setArrows("right", false);
        }
        else if (newPosition.x - pointOnSphere.x > 0){
            setArrows("left", true);
            setArrows("right", false);
        } else{
            setArrows("right", true);
            setArrows("left", false);
        }
    }

    void setArrows(String side, bool active) {
        if (side == "left") {
            foreach (int i in LEFT_ARROW_CHILDREN) {
                transform.GetChild(0).GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = active;
            }
        } else {
            foreach (int i in RIGHT_ARROW_CHILDREN) {
                transform.GetChild(0).GetChild(i).gameObject.GetComponent<MeshRenderer>().enabled = active;
            }
        }
    }

}
