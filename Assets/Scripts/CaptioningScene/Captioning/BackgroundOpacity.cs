using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundOpacity : MonoBehaviour
{
    // Start is called before the first frame update
    public Parameters parameters;
    void Start()
    {
        float alpha = parameters.alpha;
        this.GetComponent<MeshRenderer>().material.color = new Color(0f, 0f, 255.0f, alpha);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
