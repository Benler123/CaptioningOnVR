using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetOverflowSize : MonoBehaviour
{
    // Start is called before the first frame update
    public Parameters Params;

    void Start()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Camera mainCamera = Camera.main;
        float dist = transform.position.z - .0001f;
        rectTransform.sizeDelta = new Vector2(Params.getWidth(dist), rectTransform.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
