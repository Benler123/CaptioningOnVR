using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class UpdateCaptions : MonoBehaviour
{
    // Start is called before the first frame update
    Camera mainCamera;
    float dist;
    void Start()
    {
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
    }
}
