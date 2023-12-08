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
        rectTransform.sizeDelta = new Vector2(Params.getWidth(), rectTransform.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
