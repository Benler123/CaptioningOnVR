using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson.IO;
using UnityEngine;

public class PreserveScale : MonoBehaviour {

	
    Vector3 originalScale;

	void Awake() 
	{
            Debug.Log("IN AWAKE");

		originalScale = transform.localScale;
	}
   IEnumerator Start()
    {
        // Wait until the next frame
        Debug.Log("HERE1");
        yield return null;

        // Now the parent's Start method should have been called
        Debug.Log("HERE");

        AdjustScale();
    }

	 void AdjustScale()
    {
        if (transform.parent != null)
        {
            Debug.Log("Parent scale: " + transform.parent.localScale);
            transform.localScale = new Vector3(
                originalScale.x / transform.parent.parent.localScale.x/ transform.parent.localScale.x,
                originalScale.y,
                originalScale.z 
            );
        
        }
    }
	// Update is called once per frame
	void Update () {
        Renderer renderer = GetComponent<Renderer>();
        float width = renderer.bounds.size.x; // This is the width of the object in world units
	}
}