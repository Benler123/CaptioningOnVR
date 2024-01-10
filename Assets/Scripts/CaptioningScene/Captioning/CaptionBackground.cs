using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

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
        rightArrow = transform.GetChild(1).gameObject;

    }  

    // Update is called once per frame
    void Update(){
        currentJuror = Params.ReturnCurrentJurorTransform();
        switch (Params.captioningMethod) {
            case 1:
                transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = true;
                transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = true;
                HandleNonRegCaptions();
                HandleArrows();
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


    void HandleNonRegCaptions() {
        Vector3 forwardFromCamera = mainCamera.transform.forward;
        Vector3 newPosition = mainCamera.transform.position + forwardFromCamera * dist;
            
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

    void HandleArrows() {
        Vector3 pointOnSphere = Params.projectOntoSphere(dist, currentJuror);
        if(transform.position.x + buffer > pointOnSphere.x && transform.position.x - buffer < pointOnSphere.x) {
            //    myGameObject.GetComponent<MeshRenderer>().enabled = false;

            //  transform.GetChild(1).gameObject.SetActive(false);
            // transform.GetChild(2).gameObject.SetActive(false);

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
