using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager {
    private static GameManager instance = new GameManager();
    private GameStates currentState;
   
    // make sure the constructor is private, so it can only be instantiated here
    private GameManager() {
    }

    public void SetState(GameStates state) {
        currentState = state;
        switch(state) {
            case GameStates.PreScene:
                SceneManager.LoadScene("StartScene");
                break;
            case GameStates.Captioning:
                SceneManager.LoadScene("CaptioningScene");
                break;
            case GameStates.HTTPTesting:
                SceneManager.LoadScene("TestScene");
                break;
        }
    }

    public GameStates GetCurrentState() {
        return currentState;
    }
 
    public static GameManager Instance {
        get { return instance; }
    }


    public enum GameStates{
        PreScene,
        Captioning,
        HTTPTesting,
    }
}