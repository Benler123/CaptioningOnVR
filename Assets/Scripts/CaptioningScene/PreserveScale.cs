using System.Collections;
using System.Collections.Generic;
using MongoDB.Bson.IO;
using UnityEngine;

public class PreserveScale : MonoBehaviour {

	
    public bool isLeft;
    Vector3 originalScale;

	void Awake() 
	{
		originalScale = transform.localScale;
	}
   IEnumerator Start()
    {
        // Wait until the next frame
        yield return null;

        // Now the parent's Start method should have been called

        AdjustScale();
    }

	 void AdjustScale()
    {
        if (transform.parent != null)
        {
            transform.localScale = new Vector3(
                originalScale.x / transform.parent.parent.localScale.x/ transform.parent.localScale.x,
                originalScale.y,
                originalScale.z 
            );
            if (isLeft)
            {
                transform.localPosition = new Vector3(
                    -0.5f - transform.localScale.x / 2f,
                    transform.localPosition.y,
                    transform.localPosition.z
                );
            }
            else
            {
                transform.localPosition = new Vector3(
                    0.5f + transform.localScale.x / 2f,
                    transform.localPosition.y,
                    transform.localPosition.z
                );
            }
        }
    }
	// Update is called once per frame
	void Update () {
        Renderer renderer = GetComponent<Renderer>();
        float width = renderer.bounds.size.x; // This is the width of the object in world units
	}
}