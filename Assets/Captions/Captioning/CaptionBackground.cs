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
    public Parameters Params;
    Camera mainCamera;
    float dist;
    void Start()
    {
        transform.localScale = new Vector3(Params.getWidth(), transform.localScale.y, transform.localScale.z);
        mainCamera = Camera.main;
        dist = transform.position.z;
    }  

    // Update is called once per frame
    void Update(){
        // float angle = 0.0f;
        // Vector3 axis = Vector3.zero;
        // mainCamera.transform.rotation.ToAngleAxis(out angle, out axis);
        // Debug.Log(angle);
        // Debug.Log(axis);
        // transform.position = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2, angle * axis.x + angle * axis.y));
        Vector3 forwardFromCamera = mainCamera.transform.forward;
        Vector3 newPosition = mainCamera.transform.position + forwardFromCamera * dist;
            
            // Set the object's position
        transform.position = newPosition;
            
            // Optionally, make the object look at the camera or away from the camera
            // Remove or comment out the line below if you don't want the object to change its rotation
        transform.rotation = Quaternion.LookRotation(forwardFromCamera);

        setCurrentJuror();
        HandleArrows();
    }

    void HandleArrows() {
        Vector3 pointOnSphere = projectOntoSphere(currentJuror);
        if(transform.position.x + buffer > pointOnSphere.x && transform.position.x - buffer < pointOnSphere.x) {
            //    myGameObject.GetComponent<MeshRenderer>().enabled = false;

            //  transform.GetChild(1).gameObject.SetActive(false);
            // transform.GetChild(2).gameObject.SetActive(false);

            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;;
            transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = false;;
        }
        else if (transform.position.x -pointOnSphere.x > 0){
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = true;

        } else{
            transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().enabled = true;
            transform.GetChild(2).gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
    }
    void setCurrentJuror() {
        if(Params.getCurrentJuror() == "juror-a") {
            currentJuror = juror3;
        } else if (Params.getCurrentJuror() == "juror-b"){
            currentJuror = juror2;
        } else if(Params.getCurrentJuror() == "juror-c") {
            currentJuror = juror1;
        } else if(Params.getCurrentJuror() == "jury-foreman") {
            currentJuror = juror4;
        } else {
            throw new InvalidDataException("Not a valid juror");
        }
    }
    
    Vector3 projectOntoSphere(Transform point, int radius=5){
        float magnitude = currentJuror.transform.position.magnitude;
        float scalar = radius/magnitude;
        return new Vector3(
            scalar * currentJuror.transform.position.x,
            scalar * currentJuror.transform.position.y,
            scalar * currentJuror.transform.position.z
        );
    }
}
