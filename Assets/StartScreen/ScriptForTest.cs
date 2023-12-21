using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using Realms;
using UnityEngine.SceneManagement;

public class ScriptForTest : MonoBehaviour
{
    private TextMeshPro textMeshPro;
    int initialCount;
    public Parameters parameters;
    // Start is called before the first frame update
    void Start()
    {   
        textMeshPro = GetComponent<TextMeshPro>();
        
    }

    // Update is called once per frame
    void Update()
    {
        IQueryable<ParametersDataObject> parametersDataObjects = RealmController.Instance.GetParametersDataObjects();
        if (parametersDataObjects == null){
            // Debug.Log("parametersDataObjects is null");
            return;
        }
        Debug.Log("COUNT " + parametersDataObjects.Count());
        initialCount = parametersDataObjects.Count();
        long sum = 0; 
        foreach (ParametersDataObject parametersDataObject in parametersDataObjects)
        {
            Debug.Log(parametersDataObject.FOV);
            sum += parametersDataObject.FOV;
        }
        textMeshPro.text = sum.ToString();

        if (parametersDataObjects.Count() > initialCount){
            setParams(parametersDataObjects);
            SwitchScene();
        }
    }

    public void setParams(IQueryable<ParametersDataObject> parametersDataObjects) {
        parameters.fov = parametersDataObjects.Last().FOV;
        parameters.video = parametersDataObjects.Last().Video;
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("CaptioningScene");
    }
}
