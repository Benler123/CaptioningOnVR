using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Realms;
using UnityEngine.SceneManagement;

public class CaptioningRealmListener : MonoBehaviour
{
    int initialCount;
    void Start()
    {   
    }

    // Update is called once per frame
    void Update()
    {
        IQueryable<QuitDataObject> quitDataObjects = CaptioningRealmController.Instance.GetQuitDataObjects();
        if (quitDataObjects == null){
            Debug.Log("quitDataObjects is null");
            return;
        }
        if(initialCount == 0) {
            initialCount = quitDataObjects.Count();
        }

        if (quitDataObjects.Count() > initialCount){
            SwitchScene();
        }
    }

    public void SwitchScene()
    {
        GameManager.Instance.SetState(GameManager.GameStates.PreScene);
    }
}
