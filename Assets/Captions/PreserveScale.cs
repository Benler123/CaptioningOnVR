using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreserveScale : MonoBehaviour {

	Vector3 originalScale;

	public float offset = 0.2f;
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
		Debug.Log(originalScale);
        if (transform.parent != null)
        {
            transform.localScale = new Vector3(
                originalScale.x / transform.parent.localScale.x,
                originalScale.y,
                originalScale.z 
            );
			transform.position =  new Vector3(
                transform.position.x + transform.parent.localScale.x * offset,
                transform.position.y,
                transform.position.z 
            );
        }
    }
	// Update is called once per frame
	void Update () {
	}
}