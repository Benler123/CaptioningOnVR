using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class CaptionLocation : MonoBehaviour
{
    // Start is called before the first frame update
    public Parameters Params;
    Camera mainCamera;
    float dist;
    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.sizeDelta = new Vector2(Params.getWidth(), rectTransform.sizeDelta.y);
        mainCamera = Camera.main;
        dist = transform.position.z - .0001f;
    }  

    // Update is called once per frame
    void Update(){
        UpdatePosition();
    }

    void UpdatePosition() {
        Vector3 forwardFromCamera = mainCamera.transform.forward;
        // Debug.Log(forwardFromCamera);
        Vector3 newPosition = mainCamera.transform.position + forwardFromCamera * dist;
        transform.position = newPosition;
        transform.rotation = Quaternion.LookRotation(forwardFromCamera);
    }
}
