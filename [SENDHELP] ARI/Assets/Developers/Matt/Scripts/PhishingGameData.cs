using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;
public class PhishingGameData : MonoBehaviour
{
    // VARIABLES
    public string SceneToLoad;
    public Animator anim;
    public int delayTime;
    int currMessage = 0;
    bool onStartScreen = true;
    List<string> emailAddresses = new List<string>() { "noreply@aamazon.com", "microsoftbilling@c2asdhj398.com", "ITsupport@xyz.net", "trustedbank@trustedbank.com", "HR@fakecompany.com"};
    List<string> messages = new List<string>() { "Hello ARI,\nYou have been chosen as a recipient of a 100 \ndollars gift card! Click the link below to claim it:\namazom.com/giftcard",
                                                "Hello, it is required for you to update your \ncredit card information. Please use the link \nbelow to enter your credit card number.\nbilling.micros0ft.com", 
                                                "Hello ARI, there seems to be an issue with your \naccount login credentials. Please reply to this \nmessage with your username and password to this \nemail to rectify this issue.\nThank you, IT", 
                                                "Hello valued customer,\nWe regret to inform you that your online banking access has been restricted. Please use the link \nbelow to sign in and fix this issue.\ntrustedbanklogin.scamsite.net", 
                                                "Dear employee,\nYou are required to immediately review and \nelectronically sign our updated employee code of conduct. CLICK HERE to open the document.\nThank you, HR, FakeCompany"};

    static List<string> choices = new List<string>() {
        "sender address",       //0
        "suspicious link",      //1
        "poor writing",         //2
        "generic greeting",     //3
        "personal information", //4
        "urgency",              //5
        "nothing"               //6
    };
    
    /*
    0, 1, 2
    0, 1, 4
    0, 4, 5
    0, 1
    0, 1, 3, 4
    */

    List<bool> usedMessages = new List<bool> {false, false, false, false, false, false, false, false, false, false};
    int numCorrect = 0;
    static Random rnd = new Random();
    int usedCount = 0;
    void Start()
    {
        PasswordTerminalTerminal.ClearScreen();
        PasswordTerminalTerminal.WriteLine("ARI, it seems we have scammers trying to \ninfiltrate our systems.");
        PasswordTerminalTerminal.WriteLine("\nCheck your inbox and identify each thing wrong \nwith the emails.");
        PasswordTerminalTerminal.WriteLine("\nPossible choices include \"sender address\", \n\"suspicious link\", \"spelling errors\", \nand \"nothing\"");
        PasswordTerminalTerminal.WriteLine("\nType every correct choice then hit enter.\nFor example: \"sender address, suspicious link andspelling errors\" or \"nothing\"");
        PasswordTerminalTerminal.WriteLine("\nType open to get started");
        currMessage = rnd.Next(messages.Count);
        while(this.usedMessages[currMessage] == true) {
            currMessage = rnd.Next(messages.Count);
        }
    }   

    void UpdateScreen() {
        this.usedMessages[currMessage] = true;
        PasswordTerminalTerminal.ClearScreen();
        if(usedCount < messages.Count) {
            PasswordTerminalTerminal.WriteLine("You have " + (messages.Count - usedCount) + " new messages");
            PasswordTerminalTerminal.WriteLine("Phishing signs: sender address | suspicious link poor writing | generic greeting | \npersonal information | urgency");
            PasswordTerminalTerminal.WriteLine("\nFrom: " + emailAddresses[currMessage]);
            PasswordTerminalTerminal.WriteLine(messages[currMessage]);
            PasswordTerminalTerminal.WriteLine("\nType every sign you see, then hit enter");
            PasswordTerminalTerminal.WriteLine("Otherwise type \"nothing\"");
        }
        else endScreen();
    }

    void endScreen(){
        PasswordTerminalTerminal.WriteLine("You got " + numCorrect + " out of " + messages.Count + " completely correct.");
        double percentCorrect = numCorrect / (double)messages.Count;
        if(percentCorrect > .65) {
            PasswordTerminalTerminal.WriteLine("Great work ARI!");
            SceneToLoad = "Level2PhishingSuccess";
        }
        else{
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
        if(onStartScreen) {
            if(Input.ToLower() == "open") {
                onStartScreen = false;
                UpdateScreen();
            }
            else Start();
        }
        else {
            Input = Input.ToLower();
            switch(currMessage) {
                case(0):
                    if(Input.Contains(choices[0]) && Input.Contains(choices[1]) && Input.Contains(choices[2]))
                        if(!Input.Contains(choices[3]) && !Input.Contains(choices[4]) && !Input.Contains(choices[5]) && !Input.Contains(choices[6]))
                            numCorrect++;
                    break;
                case(1):
                    if(Input.Contains(choices[0]) && Input.Contains(choices[1]) && Input.Contains(choices[4]))
                        if(!Input.Contains(choices[2]) && !Input.Contains(choices[3]) && !Input.Contains(choices[5]) && !Input.Contains(choices[6]))
                            numCorrect++;
                    break;
                case(2):
                    if(Input.Contains(choices[0]) && Input.Contains(choices[4]) && Input.Contains(choices[5]))
                        if(!Input.Contains(choices[1]) && !Input.Contains(choices[2]) && !Input.Contains(choices[3]) && !Input.Contains(choices[6]))
                            numCorrect++;
                    break;
                case(3):
                    if(Input.Contains(choices[0]) && Input.Contains(choices[1]))
                        if(!Input.Contains(choices[2]) && !Input.Contains(choices[3]) && !Input.Contains(choices[4]) && !Input.Contains(choices[5]) && !Input.Contains(choices[6]))
                            numCorrect++;
                    break;
                case(4):
                    if(Input.Contains(choices[0]) && Input.Contains(choices[1]) && Input.Contains(choices[3]) && Input.Contains(choices[4]))
                        if(!Input.Contains(choices[2]) && !Input.Contains(choices[5]) && !Input.Contains(choices[6]))
                            numCorrect++;
                    break;
                default:
                    break;
            }
            usedMessages[currMessage] = true;
            if(this.usedCount < (messages.Count - 1))
                while(this.usedMessages[currMessage]) {
                    currMessage = rnd.Next(messages.Count);
                }
            usedCount++;
            UpdateScreen();
        }
    }
    

    void DelayedAction()
    {
        Debug.Log("Waiting for " + delayTime + " Seconds till next task.");
        SceneManager.LoadScene(SceneToLoad);
    }

}
