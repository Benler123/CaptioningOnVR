using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Realms;
using UnityEngine.SceneManagement;

public class RealmListener : MonoBehaviour
{
    private TextMeshPro textMeshPro;
    int initialCount;
    public Parameters parameters;
    void Start()
    {   
        textMeshPro = GetComponent<TextMeshPro>();
        
    }

    // Update is called once per frame
    void Update()
    {
        IQueryable<ParametersDataObject> parametersDataObjects = RealmController.Instance.GetParametersDataObjects();
        if (parametersDataObjects == null){
            Debug.Log("parametersDataObjects is null");
            return;
        }
        if(initialCount == 0) {
            initialCount = parametersDataObjects.Count();
        }

        if (parametersDataObjects.Count() > initialCount){
            textMeshPro.text = "LOADING";
            setParams(parametersDataObjects);
            SwitchScene();
        }
    }

    public void setParams(IQueryable<ParametersDataObject> parametersDataObjects) {
        parameters.fov = parametersDataObjects.Last().FOV;
        parameters.video = parametersDataObjects.Last().Video;
        parameters.offsetX = parametersDataObjects.Last().OffsetX;
        parameters.offsetY = parametersDataObjects.Last().OffsetY;
        parameters.captioningMethod = parametersDataObjects.Last().CaptioningMethod;
        parameters.alpha = (float)parametersDataObjects.Last().alpha;
    }

    public void SwitchScene()
    {
        GameManager.Instance.SetState(GameManager.GameStates.Captioning);
    }
}
