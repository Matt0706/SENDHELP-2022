using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LockGameManager : MonoBehaviour
{

    public LockGameData GameData;
    public LockGameEvent OnWinEvent;
    public string SceneToLoad;
    public float delayTime = 3f;
    bool isFirstTap = true;
    bool won = false;
    void Start()
    {
        GameData.ResetLevel();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0) && !GameData.isRunning && isFirstTap)
        {
            //GameData.isRunning = true;
            isFirstTap = false;
        }
        CheckGoalsLeft();
    }

    void CheckGoalsLeft()
    {
        if(GameData.GoalsLeft == 0 && !won) {
            won = true;
            Debug.Log("WON BOOL SET");
        }
        if (GameData.GoalsLeft <= 0)
        {
            OnWinEvent.Raise();
            StopLevel();
        }
    }

    public void LoadLevel()
    {
        GameData.ResetLevel();
    }

    public void StopLevel()
    {
        GameData.isRunning = false;
        if(won) {
            Invoke("nextScene", delayTime);
        }
        else {
            Debug.Log(won + " - RELOADING");
            Invoke("reload", 1f);
        }
    }

    public void reload() {
        SceneManager.LoadScene("Lockpicking");
    }
    public void nextScene() {
        SceneManager.LoadScene(SceneToLoad);
    }
}