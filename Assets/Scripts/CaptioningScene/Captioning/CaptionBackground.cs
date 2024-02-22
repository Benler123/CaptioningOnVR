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
        leftArrow = transform.GetChild(0).GetChild(0).gameObject;
        rightArrow = transform.GetChild(0).GetChild(1).gameObject;
        buffer = Params.getWidth(dist)/2;


    }  

    // Update is called once per frame
    void Update(){

        
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
        Vector3 rotatedForard = rotateYaw(offsetX);
        newPosition = mainCamera.transform.position + rotatedForard * dist;
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
        // transform.rotation = Quaternion.LookRotation(forwardFromCamera);
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
        if(backgroundRect.transform.position.x + buffer > pointOnSphere.x && backgroundRect.transform.position.x - buffer < pointOnSphere.x) {
            leftArrow.GetComponent<MeshRenderer>().enabled = false;
            rightArrow.GetComponent<MeshRenderer>().enabled = false;
        }
        else if (backgroundRect.transform.position.x -pointOnSphere.x > 0){
            leftArrow.GetComponent<MeshRenderer>().enabled = false;
            rightArrow.GetComponent<MeshRenderer>().enabled = true;

        } else{
            leftArrow.GetComponent<MeshRenderer>().enabled = true;
            rightArrow.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    Vector3 rotateYaw(float offsetX){
        // float yawAngle = 10f;

        // float yawAngleRad = Mathf.Deg2Rad * yawAngle;
        // Matrix4x4 yawRotation = Matrix4x4.identity;
        // yawRotation.m00 = Mathf.Cos(yawAngleRad);
        // yawRotation.m02 = Mathf.Sin(yawAngleRad);
        // yawRotation.m20 = -Mathf.Sin(yawAngleRad);
        // yawRotation.m22 = Mathf.Cos(yawAngleRad);

        // Vector3 rotatedForward = yawRotation.MultiplyVector(forwardFromCamera);
        // Debug.Log("Forward Vector: " + forwardFromCamera);
        // Debug.Log("Rotated Forward Vector: " + rotatedForward);

        // return rotatedForward;
        float yawAngle = offsetX;
        Vector3 directionToCaption = mainCamera.transform.forward;
        Quaternion rotation = Quaternion.AngleAxis(yawAngle, mainCamera.transform.up);
        Vector3 rotatedDirection = rotation * directionToCaption;

        return rotatedDirection;
    }

}
