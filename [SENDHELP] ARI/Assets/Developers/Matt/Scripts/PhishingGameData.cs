using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhishingGameData : MonoBehaviour
{
    // VARIABLES
    public string SceneToLoad;
    public Animator anim;
    public int delayTime;
    int messageNum = 0;
    List<string> emailAddresses = new List<string>() { "marzheir@sand.net", "friend@palebluedot.com", "phishingexample@a1b2c3.net", "phishingexample2@website.com", "anotherfriend@website.com"};
    List<string> messages = new List<string>() { "Hi this is the Martian Prince. \nPls send money.",
                                                "Hey ARI! I hope you're having fun up there!", 
                                                "placeholder phishing scam", 
                                                "placeholder phishing scam", 
                                                "placeholder non-scam"};

    List<bool> isScam = new List<bool>() {true, false, true, true, false};
    int scamsRemaining = 0;

    void Start()
    {
        for(int i = 0; i < isScam.Count; i++)
            if (isScam[i]) scamsRemaining++;
        UpdateScreen();
    }

    void UpdateScreen() {
        PasswordTerminalTerminal.ClearScreen();
        if(messageNum < messages.Count) {
            PasswordTerminalTerminal.WriteLine("You have " + (messages.Count - messageNum) + " new messages");
            PasswordTerminalTerminal.WriteLine("Please delete any phishing scams");
            PasswordTerminalTerminal.WriteLine("\nFrom: " + emailAddresses[messageNum]);
            PasswordTerminalTerminal.WriteLine(messages[messageNum]);
            PasswordTerminalTerminal.WriteLine("\nType delete if this is a phishing scam");
            PasswordTerminalTerminal.WriteLine("Otherwise type next");
        }
        else endScreen();
    }

    void endScreen(){
        if(scamsRemaining == 1) {
            PasswordTerminalTerminal.WriteLine("You left 1 scam in your inbox.");
        }
        else PasswordTerminalTerminal.WriteLine("You left " + scamsRemaining + " scams in your inbox.");
        if(scamsRemaining == 0){
            PasswordTerminalTerminal.WriteLine("Great work ARI!");
            SceneToLoad = "Level2PhishingSuccess";
        }
        if(scamsRemaining > 0) {
            PasswordTerminalTerminal.WriteLine("Warning! Detecting intruders in the network.");
        }
        Invoke("end", 5f);
    }

    void end() {
        // WIN CONDITION!
        anim.SetBool("MinigameWon", true);
        Invoke("DelayedAction", delayTime);
    }
    void OnUserInput(string Input)
    {
        if (Input.ToLower() == "delete") {
            if(isScam[messageNum]){
                scamsRemaining--;
            }
            messageNum++;
        }
        if(Input.ToLower() == "next") {
            messageNum++;
        }
        UpdateScreen();
    }
    

    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }

}
