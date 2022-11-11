using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class WebsiteManager : MonoBehaviour
{

    public GameObject wireframe1;
    public GameObject wireframe2;
    public GameObject wireframe3;
    public GameObject wireframe4;
    public GameObject wireframe5;
    public GameObject wireframe6;
    public GameObject wireframe7;
    public GameObject wireframe8;
    public GameObject wireframe9;
    public GameObject wireframe10;
    private List<GameObject> wireframes;
    public Button button;
    private Random rnd = new Random();
    private List<bool> usedSites = new List<bool>{false, false, false, false, false, false, false, false, false, false};
    public int currSite = 0;
    private int usedCount = 0;
    private Vector3 background = new Vector3(0, 0, 0);
    private Vector3 foreground = new Vector3(0, 0, -10);
    private int numCorrect;
    private bool gameOver = false;

    void Start() {
        wireframes = new List<GameObject>{wireframe1, wireframe2, wireframe3, wireframe4, wireframe5, wireframe6, wireframe7, wireframe8, wireframe9, wireframe10};
        currSite = rnd.Next(wireframes.Count);
        wireframes[currSite].transform.localPosition = foreground;
    }

    public void changeSite() {

        wireframes[currSite].transform.localPosition = background;

        usedSites[currSite] = true;
        if(usedCount < wireframes.Count - 1)
        while(usedSites[currSite])
            currSite = rnd.Next(wireframes.Count);
        usedCount++;

        wireframes[currSite].transform.localPosition = foreground;
        
    }

    public void onRealClick() {
        if(!gameOver) {
            if(usedCount == wireframes.Count -1)
                if(currSite == 8 || currSite == 9)
                    numCorrect++;
            if(usedCount < wireframes.Count - 1){
                if(currSite == 8 || currSite == 9)
                    numCorrect++;
                changeSite();
            }
            else gameOver = true;
        }
        else endGame();
    }

    public void onFakeClick() {
        if(!gameOver) {
            if(usedCount == wireframes.Count - 1)
                if(currSite != 8 && currSite != 9)
                    numCorrect++;
            if(usedCount < wireframes.Count - 1){
                if(currSite != 8 && currSite != 9)
                    numCorrect++;
                changeSite();
            }
            else gameOver = true;
        }
        else endGame();
    }

    void endGame() {
        Debug.Log("Number Correct: " + numCorrect);
        if(numCorrect >= 7)
            Debug.Log("WINNER");
        else
            Debug.Log("LOSER");
    }
}
