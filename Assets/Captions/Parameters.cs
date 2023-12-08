using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Params")]
public class Parameters : ScriptableObject
{
    public float fov = 10;
    public int video = 1;
    
    private string currentJuror;
    public float getWidth() {
        // This is assuming the camera is 5 away from the captions
        return Mathf.Tan(fov/2 * 0.0174533f) * 5 * 2;
    }

    public string getCurrentJuror(){
        return currentJuror;
    }

    public void setCurrentJuror(string currentJuror){
        // juror-a/Blue juror-b/Pink juror-c/David jury-foreman/Adam
        this.currentJuror = currentJuror;
    }

}
